using BeautySalonBooking.Domain.Base.Entities;

namespace BeautySalonBooking.Domain.ContentAggregate.Entities;

public class ContentLike : AuditableEntity<long>
{
    public long ContentItemId { get; private set; }
    public ContentItem ContentItem { get; private set; } = null!;

    public long UserId { get; private set; }

    private ContentLike()
    {
    }

    public ContentLike(
        long contentItemId,
        long userId,
        long createdBy)
        : base(createdBy)
    {
        if (contentItemId <= 0)
            throw new ArgumentOutOfRangeException(nameof(contentItemId));

        if (userId <= 0)
            throw new ArgumentOutOfRangeException(nameof(userId));

        ContentItemId = contentItemId;
        UserId = userId;
    }
}
