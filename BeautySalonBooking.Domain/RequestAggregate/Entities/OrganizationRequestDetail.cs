using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.GeographyAggregate.Entities;

namespace BeautySalonBooking.Domain.RequestAggregate.Entities;

public class OrganizationRequestDetail : AuditableEntity<long>
{
    public long RequestId { get; private set; }
    public Request Request { get; private set; } = null!;

    public string? OrganizationName { get; set; } // 👈 private set نیست، public set است
    public string? OrganizationDescription { get; set; } // 👈 private set نیست
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

    // 👇 سازنده خالی برای EF Core
    private OrganizationRequestDetail() { }

    // 👇 سازنده کامل (باید نام پارامترها دقیقاً مطابق کد RequestService باشد)
    public OrganizationRequestDetail(
        string? organizationName,
        string? organizationDescription,
        string? phoneNumber,
        string? mobileNumber,
        string? website,
        string? instagram,
        string? email,
        string? whatsapp,
        string? branchName,
        string? branchDescription,
        int? regionId,
        string? address,
        string? postalCode,
        string? licenseNumber,
        DateTime? licenseIssuedDate,
        DateTime? licenseExpireDate,
        string? slogan,
        string? additionalInfo)
    {
        OrganizationName = organizationName;
        OrganizationDescription = organizationDescription;
        PhoneNumber = phoneNumber;
        MobileNumber = mobileNumber;
        Website = website;
        Instagram = instagram;
        Email = email;
        Whatsapp = whatsapp;
        BranchName = branchName;
        BranchDescription = branchDescription;
        RegionId = regionId;
        Address = address;
        PostalCode = postalCode;
        LicenseNumber = licenseNumber;
        LicenseIssuedDate = licenseIssuedDate;
        LicenseExpireDate = licenseExpireDate;
        Slogan = slogan;
        AdditionalInfo = additionalInfo;
    }
}