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

    // / سازنده خالی برای EF Core
    private RequestRequiredDocument() { }

    // سازنده برای استفاده در کد
    public RequestRequiredDocument(int requestTypeId, int documentTypeId, bool isRequired, int displayOrder)
    {
        RequestTypeId = requestTypeId;
        DocumentTypeId = documentTypeId;
        IsRequired = isRequired;
        DisplayOrder = displayOrder;
    }

    public void Update(bool isRequired, int displayOrder)
    {
        IsRequired = isRequired;
        DisplayOrder = displayOrder;
    }

}