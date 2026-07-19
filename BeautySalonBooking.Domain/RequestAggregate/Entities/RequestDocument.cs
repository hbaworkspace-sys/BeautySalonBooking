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
}