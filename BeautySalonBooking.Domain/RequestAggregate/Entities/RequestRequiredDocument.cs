using BeautySalonBooking.Domain.Base.Entities;

namespace BeautySalonBooking.Domain.RequestAggregate.Entities;

public class RequestRequiredDocument : AuditableSoftDeleteEntity<int>
{
    public int RequestTypeId { get; private set; }
    public RequestType RequestType { get; private set; } = null!;

    public int DocumentTypeId { get; private set; }
    public DocumentType DocumentType { get; private set; } = null!;

    public bool IsRequired { get; private set; }
    public int DisplayOrder { get; private set; }
}