using BeautySalonBooking.Domain.Base.Entities;

namespace BeautySalonBooking.Domain.ContentAggregate.Entities;

public class ContentView : AuditableEntity<long>
{
    public long ContentItemId { get; private set; }
    public ContentItem ContentItem { get; private set; } = null!;

    public long? UserId { get; private set; }

    public DateTime ViewedAt { get; private set; }

    private ContentView()
    {
    }
    public ContentView(
        long contentItemId,
        long? userId)
    {
        if (contentItemId <= 0)
            throw new ArgumentOutOfRangeException(nameof(contentItemId));

        if (userId.HasValue && userId.Value <= 0)
            throw new ArgumentOutOfRangeException(nameof(userId));

        ContentItemId = contentItemId;
        UserId = userId;
        ViewedAt = DateTime.UtcNow;
    }
}