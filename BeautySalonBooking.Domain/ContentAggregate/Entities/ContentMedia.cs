
using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Base.Enums;

namespace BeautySalonBooking.Domain.ContentAggregate.Entities;

public class ContentMedia : AuditableEntity<long>
{
    public long ContentItemId { get; private set; }
    public ContentItem ContentItem { get; private set; } = null!;

    public string FileName { get; private set; } = null!;
    public string ContentType { get; private set; } = null!;
    public long FileSize { get; private set; }
    public byte[] Content { get; private set; } = null!;

    public MediaType Type { get; private set; }
    public int DisplayOrder { get; private set; }

    private ContentMedia()
    {
    }

    internal ContentMedia(
        ContentItem contentItem,
        string fileName,
        string contentType,
        long fileSize,
        byte[] content,
        MediaType type,
        int displayOrder,
        long createdBy)
        : base(createdBy)
    {
        ArgumentNullException.ThrowIfNull(contentItem);
        ArgumentNullException.ThrowIfNull(content);

        if (createdBy <= 0)
            throw new ArgumentOutOfRangeException(nameof(createdBy));

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

        if (content.Length == 0)
            throw new ArgumentException(
                "Media content cannot be empty.",
                nameof(content));

        if (fileSize != content.LongLength)
            throw new ArgumentException(
                "File size does not match the content length.",
                nameof(fileSize));

        if (!Enum.IsDefined(typeof(MediaType), type))
            throw new ArgumentOutOfRangeException(nameof(type));

        if (displayOrder < 0)
            throw new ArgumentOutOfRangeException(nameof(displayOrder));

        ContentItem = contentItem;
        FileName = fileName.Trim();
        ContentType = contentType.Trim();
        FileSize = fileSize;
        Content = content.ToArray();
        Type = type;
        DisplayOrder = displayOrder;
    }
}
