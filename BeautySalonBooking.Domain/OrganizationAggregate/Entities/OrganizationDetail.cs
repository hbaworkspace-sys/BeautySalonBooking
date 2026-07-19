using BeautySalonBooking.Domain.Base.Entities;

namespace BeautySalonBooking.Domain.OrganizationAggregate.Entities;

public class OrganizationDetail : AuditableEntity<long>
{
    public long OrganizationId { get; private set; }
    public Organization Organization { get; private set; } = null!;

    public string? Description { get; private set; }
    public string? Website { get; private set; }
    public string? Instagram { get; private set; }
    public string? Email { get; private set; }
    public string? Whatsapp { get; private set; }
    public DateTime FromDate { get; private set; }
    public DateTime ToDate { get; private set; }
}