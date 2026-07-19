using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.ContactAggregate.Enums;

namespace BeautySalonBooking.Domain.ContactAggregate.Entities;

public class PhoneNumber : AuditableSoftDeleteEntity<long>
{
    public long RelatedId { get; private set; }
    public PhoneNumberOwnerType RelatedType { get; private set; }
    public PhoneNumberType Type { get; private set; }
    public string Number { get; private set; } = null!;
    public bool IsDefault { get; private set; }
    public bool IsVerified { get; private set; }
}