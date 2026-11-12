using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Base.Enums;
using BeautySalonBooking.Domain.BranchAggregate.Entities;
using BeautySalonBooking.Domain.ContentAggregate.Enums;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;
using BeautySalonBooking.Domain.OrganizationAggregate.Entities;

namespace BeautySalonBooking.Domain.ContentAggregate.Entities;

public class ContentItem : AuditableSoftDeleteEntity<long>
{
    // مالک محتوا: سازمان
    public long? OrganizationId { get; private set; }
    public Organization? Organization { get; private set; }

    // مالک محتوا: شعبه
    public long? BranchId { get; private set; }
    public Branch? Branch { get; private set; }

    // مالک محتوا: کاربر (آرایشگر)
    public long? PublisherUserId { get; private set; }
    public User? PublisherUser { get; private set; }

    public ContentType Type { get; private set; }
    public ContentStatus Status { get; private set; }

    public string? Caption { get; private set; }

    public DateTime? PublishedAt { get; private set; }
    public DateTime? ExpiresAt { get; private set; }

    private readonly List<ContentMedia> _media = new();

    public IReadOnlyCollection<ContentMedia> Media => _media.AsReadOnly();

    // مورد استفاده EF Core
    private ContentItem()
    {
    }

    // سازنده مشترک برای ایجاد محتوا با مالک مشخص
    private ContentItem(
        long? organizationId,
        long? branchId,
        long? publisherUserId,
        ContentType type,
        long createdBy,
        string? caption,
        DateTime? expiresAt)
        : base(createdBy)
    {
        ValidateOwner(organizationId, branchId, publisherUserId);

        if (createdBy <= 0)
            throw new ArgumentOutOfRangeException(nameof(createdBy));

        ValidateType(type);
        ValidateCaption(caption);
        ValidateExpiration(expiresAt);

        if (expiresAt.HasValue && expiresAt.Value <= DateTime.UtcNow)
        {
            throw new ArgumentOutOfRangeException(
                nameof(expiresAt),
                "Expiration must be in the future.");
        }

        OrganizationId = organizationId;
        BranchId = branchId;
        PublisherUserId = publisherUserId;

        Type = type;
        Status = ContentStatus.Draft;

        Caption = NormalizeCaption(caption);
        ExpiresAt = expiresAt;
    }

    // ایجاد محتوا در پروفایل سازمان
    public static ContentItem CreateForOrganization(
        long organizationId,
        ContentType type,
        long createdBy,
        string? caption = null,
        DateTime? expiresAt = null)
    {
        return new ContentItem(
            organizationId,
            null,
            null,
            type,
            createdBy,
            caption,
            expiresAt);
    }

    // ایجاد محتوا در پروفایل شعبه
    public static ContentItem CreateForBranch(
        long branchId,
        ContentType type,
        long createdBy,
        string? caption = null,
        DateTime? expiresAt = null)
    {
        return new ContentItem(
            null,
            branchId,
            null,
            type,
            createdBy,
            caption,
            expiresAt);
    }

    // ایجاد محتوا در پروفایل شخصی آرایشگر
    public static ContentItem CreateForStylist(
        long stylistUserId,
        ContentType type,
        long createdBy,
        string? caption = null,
        DateTime? expiresAt = null)
    {
        return new ContentItem(
            null,
            null,
            stylistUserId,
            type,
            createdBy,
            caption,
            expiresAt);
    }

    public void UpdateCaption(string? caption, long? userId)
    {
        EnsureEditable();
        ValidateCaption(caption);
        ValidateUserId(userId);

        Caption = NormalizeCaption(caption);

        MarkUpdated(userId);
    }

    public void SetExpiration(DateTime? expiresAt, long? userId)
    {
        EnsureNotDeleted();
        ValidateUserId(userId);
        ValidateExpiration(expiresAt);

        if (expiresAt.HasValue)
        {
            if (expiresAt.Value <= DateTime.UtcNow)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(expiresAt),
                    "Expiration must be in the future.");
            }

            if (PublishedAt.HasValue &&
                expiresAt.Value <= PublishedAt.Value)
            {
                throw new ArgumentException(
                    "Expiration must be later than publication.",
                    nameof(expiresAt));
            }
        }

        ExpiresAt = expiresAt;

        MarkUpdated(userId);
    }

    public void Publish(long? userId)
    {
        EnsureNotDeleted();
        ValidateUserId(userId);

        if (Status != ContentStatus.Draft)
        {
            throw new InvalidOperationException(
                "Only draft content can be published.");
        }

        var now = DateTime.UtcNow;

        if (ExpiresAt.HasValue && ExpiresAt.Value <= now)
        {
            throw new InvalidOperationException(
                "Expired content cannot be published.");
        }

        PublishedAt = now;
        Status = ContentStatus.Published;

        MarkUpdated(userId);
    }

    public void Archive(long? userId)
    {
        EnsureNotDeleted();
        ValidateUserId(userId);

        if (Status == ContentStatus.Archived)
            return;

        Status = ContentStatus.Archived;

        MarkUpdated(userId);
    }

    public void AddMedia(
        string fileName,
        string contentType,
        long fileSize,
        byte[] content,
        MediaType mediaType,
        int displayOrder,
        long createdBy)
    {
        EnsureEditable();

        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException(
                "File name is required.",
                nameof(fileName));

        if (string.IsNullOrWhiteSpace(contentType))
            throw new ArgumentException(
                "Content type is required.",
                nameof(contentType));

        if (fileSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(fileSize));

        ArgumentNullException.ThrowIfNull(content);

        if (content.Length == 0)
            throw new ArgumentException(
                "File content cannot be empty.",
                nameof(content));

        if (fileSize != content.LongLength)
            throw new ArgumentException(
                "File size does not match the content length.",
                nameof(fileSize));

        if (displayOrder < 0)
            throw new ArgumentOutOfRangeException(nameof(displayOrder));

        if (createdBy <= 0)
            throw new ArgumentOutOfRangeException(nameof(createdBy));

        if (!Enum.IsDefined(typeof(MediaType), mediaType))
            throw new ArgumentOutOfRangeException(nameof(mediaType));

        var media = new ContentMedia(
            this,
            fileName.Trim(),
            contentType.Trim(),
            fileSize,
            content,
            mediaType,
            displayOrder,
            createdBy);

        _media.Add(media);

        MarkUpdated(createdBy);
    }

    private void EnsureNotDeleted()
    {
        if (IsDeleted)
        {
            throw new InvalidOperationException(
                "Deleted content cannot be modified.");
        }
    }

    private void EnsureEditable()
    {
        EnsureNotDeleted();

        if (Status != ContentStatus.Draft)
        {
            throw new InvalidOperationException(
                "Only draft content can be modified.");
        }
    }

    private static void ValidateOwner(
        long? organizationId,
        long? branchId,
        long? publisherUserId)
    {
        var ownerCount =
            (organizationId.HasValue ? 1 : 0) +
            (branchId.HasValue ? 1 : 0) +
            (publisherUserId.HasValue ? 1 : 0);

        if (ownerCount != 1)
        {
            throw new ArgumentException(
                "Exactly one content owner must be specified.");
        }

        if (organizationId.HasValue && organizationId.Value <= 0)
            throw new ArgumentOutOfRangeException(nameof(organizationId));

        if (branchId.HasValue && branchId.Value <= 0)
            throw new ArgumentOutOfRangeException(nameof(branchId));

        if (publisherUserId.HasValue && publisherUserId.Value <= 0)
            throw new ArgumentOutOfRangeException(nameof(publisherUserId));
    }

    private static void ValidateType(ContentType type)
    {
        if (!Enum.IsDefined(typeof(ContentType), type))
            throw new ArgumentOutOfRangeException(nameof(type));
    }

    private static void ValidateCaption(string? caption)
    {
        if (caption is { Length: > 1000 })
        {
            throw new ArgumentException(
                "Caption cannot exceed 1000 characters.",
                nameof(caption));
        }
    }

    private static string? NormalizeCaption(string? caption)
    {
        return string.IsNullOrWhiteSpace(caption)
            ? null
            : caption.Trim();
    }

    private static void ValidateExpiration(DateTime? expiresAt)
    {
        if (expiresAt.HasValue &&
            expiresAt.Value.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException(
                "Expiration must be expressed in UTC.",
                nameof(expiresAt));
        }
    }

    private static void ValidateUserId(long? userId)
    {
        if (userId.HasValue && userId.Value <= 0)
            throw new ArgumentOutOfRangeException(nameof(userId));
    }
}
