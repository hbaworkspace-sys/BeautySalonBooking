using BeautySalonBooking.Domain.Base.Entities;

namespace BeautySalonBooking.Domain.RequestAggregate.Entities;

public class PersonRequestDetail : AuditableEntity<long>
{
    public long RequestId { get; private set; }
    public Request Request { get; private set; } = null!;
    public string? Address { get; private set; }
    public string? CodePosti { get; private set; }
    public string? LicenseNo { get; private set; }
    public string? Biography { get; private set; }
    public int? ExperienceYears { get; private set; }
    public string? Skills { get; private set; }
    public string? Certifications { get; private set; }
    public string? Instagram { get; private set; }
    public string? Website { get; private set; }
    public string? AdditionalInfo { get; private set; }



    private PersonRequestDetail() { }


    public PersonRequestDetail(
        string? address, string? codePosti, string? licenseNo,
        string? biography, int? experienceYears, string? skills,
        string? certifications, string? instagram, string? website, string? additionalInfo)
    {
        Address = address;
        CodePosti = codePosti;
        LicenseNo = licenseNo;
        Biography = biography;
        ExperienceYears = experienceYears;
        Skills = skills;
        Certifications = certifications;
        Instagram = instagram;
        Website = website;
        AdditionalInfo = additionalInfo;
    }

}