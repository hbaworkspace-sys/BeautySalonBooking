using BeautySalonBooking.Domain.Base.Entities;

namespace BeautySalonBooking.Domain.RequestAggregate.Entities;

public class PersonRequestDetail : AuditableEntity<long>
{
    public long RequestId { get; private set; }
    public Request Request { get; private set; } = null!;

    public string? Biography { get; private set; }
    public int? ExperienceYears { get; private set; }
    public string? Skills { get; private set; }
    public string? Certifications { get; private set; }
    public string? Instagram { get; private set; }
    public string? Website { get; private set; }
    public string? AdditionalInfo { get; private set; }
}