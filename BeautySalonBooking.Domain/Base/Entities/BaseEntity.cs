namespace BeautySalonBooking.Domain.Base.Entities;

public abstract class BaseEntity<TId>
{
    public TId Id { get; protected set; } = default!;
    public bool IsActive { get; private set; } = true;

    protected BaseEntity()
    {
    }

    public virtual void Activate()
    {
        IsActive = true;
    }

    public virtual void Deactivate()
    {
        IsActive = false;
    }
}