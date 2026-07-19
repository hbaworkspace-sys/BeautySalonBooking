using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;
using BeautySalonBooking.Domain.RequestAggregate.Enums;

namespace BeautySalonBooking.Domain.RequestAggregate.Entities;

public class RequestHistory : AuditableEntity<long>
{
    public long RequestId { get; private set; }
    public Request Request { get; private set; } = null!;

    public RequestStatus Status { get; private set; }
    public RequestHistoryAction Action { get; private set; }

    public long? ChangedByUserId { get; private set; }
    public User? ChangedByUser { get; private set; }

    public string? Description { get; private set; }
}