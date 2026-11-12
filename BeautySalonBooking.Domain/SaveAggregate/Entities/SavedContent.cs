using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.ContentAggregate.Entities;

namespace BeautySalonBooking.Domain.SaveAggregate.Entities;

public class SavedContent : AuditableEntity<long>
{
    public long? OwnerUserId { get; private set; }
    public long? OwnerOrganizationId { get; private set; }

    public long ContentItemId { get; private set; }
    public ContentItem ContentItem { get; private set; } = null!;

    private SavedContent()
    {
    }

    public SavedContent(
        long? ownerUserId,
        long? ownerOrganizationId,
        long contentItemId,
        long createdBy)
        : base(createdBy)
    {
        ValidateOwner(ownerUserId, ownerOrganizationId);

        if (contentItemId <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(contentItemId),
                "شناسه محتوا معتبر نیست.");

        if (createdBy <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(createdBy),
                "شناسه کاربر ایجادکننده معتبر نیست.");

        OwnerUserId = ownerUserId;
        OwnerOrganizationId = ownerOrganizationId;
        ContentItemId = contentItemId;
    }

    private static void ValidateOwner(
        long? ownerUserId,
        long? ownerOrganizationId)
    {
        bool hasUser = ownerUserId.HasValue;
        bool hasOrganization = ownerOrganizationId.HasValue;

        if (hasUser == hasOrganization)
            throw new ArgumentException(
                "دقیقاً یکی از شناسه‌های کاربر یا سازمان باید مشخص شود.");

        if (ownerUserId.HasValue && ownerUserId.Value <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(ownerUserId),
                "شناسه کاربر معتبر نیست.");

        if (ownerOrganizationId.HasValue &&
            ownerOrganizationId.Value <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(ownerOrganizationId),
                "شناسه سازمان معتبر نیست.");
    }
}
