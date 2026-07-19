using BeautySalonBooking.Domain.Base.Entities;

namespace BeautySalonBooking.Domain.RequestAggregate.Entities;

public class DocumentType : AuditableSoftDeleteEntity<int>
{
    public string Title { get; private set; } = null!;
    public string Code { get; private set; } = null!;
    public string? Description { get; private set; }


    private readonly List<RequestDocument> _requestDocuments = new();
    public IReadOnlyCollection<RequestDocument> RequestDocuments => _requestDocuments;


    private readonly List<RequestRequiredDocument> _requiredDocuments = new();
    public IReadOnlyCollection<RequestRequiredDocument> RequiredDocuments => _requiredDocuments;

}