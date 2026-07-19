using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.GeographyAggregate.Enums;

namespace BeautySalonBooking.Domain.GeographyAggregate.Entities;

public class Address : AuditableSoftDeleteEntity<long>
{
    public long RelatedId { get; private set; }
    public AddressOwnerType RelatedType { get; private set; }
    public int RegionId { get; private set; }
    public Region Region { get; private set; } = null!;
    public string FullAddress { get; private set; } = null!;
    public string? PostalCode { get; private set; }
}