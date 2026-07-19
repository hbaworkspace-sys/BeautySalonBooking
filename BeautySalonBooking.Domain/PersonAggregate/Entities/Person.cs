using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.GeographyAggregate.Entities;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;
using BeautySalonBooking.Domain.PersonAggregate.Eunms;

namespace BeautySalonBooking.Domain.PersonAggregate.Entities;

public class Person : AuditableSoftDeleteEntity<long>
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string? FatherName { get; private set; }
    public string? NationalCode { get; private set; }
    public Gender Gender { get; private set; }
    public DateOnly? BirthDate { get; private set; }
    public int? BirthRegionId { get; private set; }
    public Region? BirthRegion { get; private set; }
    public string? BirthCertificateNumber { get; private set; }
    public string? BirthCertificateSerial { get; private set; }


    private readonly List<User> _users = new();
    public IReadOnlyCollection<User> Users => _users;
}