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

    private OrganizationDetail() { }
    public void SetOrganizationId(long organizationId) => OrganizationId = organizationId;
    public OrganizationDetail(string? description, string? website, string? instagram,
        string? email, string? whatsapp, DateTime fromDate, DateTime toDate)
    {
        Description = description;
        Website = website;
        Instagram = instagram;
        Email = email;
        Whatsapp = whatsapp;
        FromDate = fromDate;
        ToDate = toDate;
    }
}