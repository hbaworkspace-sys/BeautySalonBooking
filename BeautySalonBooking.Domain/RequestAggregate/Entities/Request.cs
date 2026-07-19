using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;
using BeautySalonBooking.Domain.RequestAggregate.Enums;

namespace BeautySalonBooking.Domain.RequestAggregate.Entities;

public class Request : AuditableEntity<long>
{
    public long UserId { get; private set; }
    public User User { get; private set; } = null!;

    public int RequestTypeId { get; private set; }
    public RequestType RequestType { get; private set; } = null!;

    public string TrackingCode { get; private set; } = null!;
    public RequestStatus Status { get; private set; }
    public byte CurrentStep { get; private set; }
    public DateTime SubmittedAt { get; private set; }
    public long? ReviewedBy { get; private set; }
    public User? Reviewer { get; private set; }
    public string? ReviewDescription { get; private set; }


    public OrganizationRequestDetail? OrganizationRequestData { get; private set; }
    public PersonRequestDetail? PersonRequestData { get; private set; }


    private readonly List<RequestDocument> _documents = new();
    public IReadOnlyCollection<RequestDocument> Documents => _documents;


    private readonly List<RequestHistory> _histories = new();
    public IReadOnlyCollection<RequestHistory> Histories => _histories;
}