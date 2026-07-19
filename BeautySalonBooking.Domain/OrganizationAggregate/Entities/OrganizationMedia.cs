using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.OrganizationAggregate.Enums;

namespace BeautySalonBooking.Domain.OrganizationAggregate.Entities;

public class OrganizationMedia : AuditableEntity<long>
{
    public long OrganizationId { get; private set; }
    public Organization Organization { get; private set; } = null!;

    public string FileName { get; private set; } = null!;
    public string ContentType { get; private set; } = null!;
    public long FileSize { get; private set; }
    public byte[] Content { get; private set; } = null!;
    public OrganizationMediaType Type { get; private set; }
    public int DisplayOrder { get; private set; }
}