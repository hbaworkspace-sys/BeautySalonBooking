using BeautySalonBooking.Domain.Base.Entities;

namespace BeautySalonBooking.Domain.RequestAggregate.Entities;

public class RequestType : AuditableSoftDeleteEntity<int>
{
    public string Title { get; private set; } = null!;
    public string Code { get; private set; } = null!;
    public string? Description { get; private set; }
    public bool IsPaymentRequired { get; private set; }


    private readonly List<Request> _requests = new();
    public IReadOnlyCollection<Request> Requests => _requests;


    private readonly List<RequestField> _requestFields = new();
    public IReadOnlyCollection<RequestField> RequestFields => _requestFields;


    private readonly List<RequestRequiredDocument> _requiredDocuments = new();
    public IReadOnlyCollection<RequestRequiredDocument> RequiredDocuments => _requiredDocuments;
}