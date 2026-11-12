using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.BranchAggregate.Entities;

namespace BeautySalonBooking.Domain.SchedulingAggregate.Entities;

public class BranchHoliday : AuditableSoftDeleteEntity<long>
{
    public long BranchId { get; private set; }
    public Branch Branch { get; private set; } = null!;

    public DateOnly Date { get; private set; }
    public string? Title { get; private set; }
    public string? Description { get; private set; }
    public bool IsNationalHoliday { get; private set; }

    private BranchHoliday() { }

    public BranchHoliday(long branchId, DateOnly date, string? title, string? description, bool isNationalHoliday)
    {
        BranchId = branchId;
        Date = date;
        Title = title;
        Description = description;
        IsNationalHoliday = isNationalHoliday;
    }

    public void Update(string? title, string? description, bool isNationalHoliday)
    {
        Title = title;
        Description = description;
        IsNationalHoliday = isNationalHoliday;
    }

}