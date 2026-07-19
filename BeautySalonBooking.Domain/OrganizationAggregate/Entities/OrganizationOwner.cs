using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;

namespace BeautySalonBooking.Domain.OrganizationAggregate.Entities;

public class OrganizationOwner : AuditableEntity<long>
{
    public long OrganizationId { get; private set; }
    public Organization Organization { get; private set; } = null!;

    public long OwnerUserId { get; private set; }
    public User OwnerUser { get; private set; } = null!;

    public bool IsPrimary { get; private set; }
}