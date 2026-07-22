namespace BeautySalonBooking.Domain.Base.Entities;

public abstract class AuditableEntity<TId> : BaseEntity<TId>
{
    public DateTime CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public long? UpdatedBy { get; private set; }

    protected AuditableEntity()
    {
    }

    protected AuditableEntity(long createdBy)
    {
        MarkCreated(createdBy);
    }

    public void MarkCreated(long? userId)
    {
        CreatedAt = DateTime.UtcNow;
        CreatedBy = userId;
    }

    public void MarkUpdated(long? userId)
    {
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = userId;
    }
}