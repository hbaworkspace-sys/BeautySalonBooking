namespace BeautySalonBooking.Domain.Base.Entities;

public abstract class BaseEntity<TId>
{
    public TId Id { get; protected set; } = default!;
    public bool IsActive { get; private set; } = true;

    protected BaseEntity()
    {
    }

    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
    }
}