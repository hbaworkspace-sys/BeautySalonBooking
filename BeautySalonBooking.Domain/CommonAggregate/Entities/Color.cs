using BeautySalonBooking.Domain.Base.Entities;

namespace BeautySalonBooking.Domain.CommonAggregate.Entities;

public class Color : AuditableSoftDeleteEntity<int>
{
    public string Name { get; private set; } = null!;
    public string Code { get; private set; } = null!;
    public string? HexCode { get; private set; } = null!;
}
