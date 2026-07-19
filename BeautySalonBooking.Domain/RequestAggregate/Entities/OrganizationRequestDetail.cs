using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.GeographyAggregate.Entities;

namespace BeautySalonBooking.Domain.RequestAggregate.Entities;

public class OrganizationRequestDetail : AuditableEntity<long>
{
    public long RequestId { get; private set; }
    public Request Request { get; private set; } = null!;

    public string? OrganizationName { get; private set; }
    public string? OrganizationDescription { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? MobileNumber { get; private set; }
    public string? Website { get; private set; }
    public string? Instagram { get; private set; }
    public string? Email { get; private set; }
    public string? Whatsapp { get; private set; }
    public string? BranchName { get; private set; }
    public string? BranchDescription { get; private set; }
    public int? RegionId { get; private set; }
    public Region? Region { get; private set; }
    public string? Address { get; private set; }
    public string? PostalCode { get; private set; }
    public string? LicenseNumber { get; private set; }
    public DateTime? LicenseIssuedDate { get; private set; }
    public DateTime? LicenseExpireDate { get; private set; }
    public string? Slogan { get; private set; }
    public string? AdditionalInfo { get; private set; }
}