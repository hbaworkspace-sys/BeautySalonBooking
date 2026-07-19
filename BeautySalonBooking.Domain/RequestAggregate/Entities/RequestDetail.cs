using BeautySalonBooking.Domain.Base.Entities;

namespace BeautySalonBooking.Domain.RequestAggregate.Entities;

public class RequestDetail : AuditableEntity<long>
{
    public long RequestId { get; private set; }
    public Request Request { get; private set; } = null!;

    public int RequestFieldId { get; private set; }
    public RequestField RequestField { get; private set; } = null!;

    public string? Value { get; private set; }
}