using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;
using BeautySalonBooking.Domain.RequestAggregate.Enums;

namespace BeautySalonBooking.Domain.RequestAggregate.Entities;

public class RequestDocument : AuditableEntity<long>
{
    public long RequestId { get; private set; }
    public Request Request { get; private set; } = null!;

    public int DocumentTypeId { get; private set; }
    public DocumentType DocumentType { get; private set; } = null!;

    public string FileName { get; private set; } = null!;
    public string ContentType { get; private set; } = null!;
    public long FileSize { get; private set; }
    public byte[] Content { get; private set; } = null!;
    public RequestDocumentStatus Status { get; private set; }
    public long? ReviewedByUserId { get; private set; }
    public User? ReviewedByUser { get; private set; }

    public DateTime? ReviewedAt { get; private set; }
    public string? ReviewDescription { get; private set; }

    private RequestDocument() { }

    // سازنده برای استفاده در کد
    public RequestDocument(int documentTypeId, string fileName, string contentType, byte[] content)
    {
        DocumentTypeId = documentTypeId;
        FileName = fileName;
        ContentType = contentType;
        Content = content;
        FileSize = content.Length;
        Status = RequestDocumentStatus.Pending; // یا هر وضعیت اولیه‌ای که دارید
    }
}