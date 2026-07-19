namespace BeautySalonBooking.Domain.Base.Entities;

public abstract class AuditableSoftDeleteEntity<TId> : AuditableEntity<TId>
{
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public long? DeletedBy { get; private set; }

    protected AuditableSoftDeleteEntity()
    {
    }

    protected AuditableSoftDeleteEntity(long createdBy)
        : base(createdBy)
    {
    }

    public virtual void Delete(long userId)
    {
        if (IsDeleted)
            return;

        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        DeletedBy = userId;

        Deactivate();
        UpdateAudit(userId);
    }

    public virtual void Restore(long userId)
    {
        if (!IsDeleted)
            return;

        IsDeleted = false;
        DeletedAt = null;
        DeletedBy = null;

        Activate();
        UpdateAudit(userId);
    }
}
