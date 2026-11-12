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

    private Request() { }

    public Request(long userId, int requestTypeId, string trackingCode, RequestStatus status, DateTime submittedAt)
    {
        UserId = userId;
        RequestTypeId = requestTypeId;
        TrackingCode = trackingCode;
        Status = status;
        SubmittedAt = submittedAt;
        CurrentStep = 1;
    }

    public void SetPersonRequestData(PersonRequestDetail detail) => PersonRequestData = detail;
    public void SetOrganizationRequestData(OrganizationRequestDetail detail) => OrganizationRequestData = detail;
    public void AddDocument(RequestDocument document) => _documents.Add(document);

    // 👇 متد جدید برای تغییر وضعیت و اضافه کردن تاریخچه
    public void ChangeStatus(RequestStatus newStatus, long changedByUserId, RequestHistoryAction action, string? description = null)
    {
        Status = newStatus;
        var history = new RequestHistory(this.Id, newStatus, action, changedByUserId, description);
        _histories.Add(history);
    }

    // 👇 متد برای اضافه کردن تاریخچه اولیه (هنگام ایجاد)
    public void AddInitialHistory(long changedByUserId, string? description = null)
    {
        var history = new RequestHistory(this.Id, this.Status, RequestHistoryAction.Created, changedByUserId, description ?? "درخواست ایجاد شد.");
        _histories.Add(history);
    }



    // و برای تایید نهایی:
    public void Approve(long reviewedByUserId, string? reviewDescription = null)
    {
        Status = RequestStatus.Approved;
        ReviewedBy = reviewedByUserId;
        ReviewDescription = reviewDescription;
        var history = new RequestHistory(this.Id, RequestStatus.Approved, RequestHistoryAction.Approved, reviewedByUserId, reviewDescription ?? "درخواست تایید شد.");
        _histories.Add(history);
    }

}