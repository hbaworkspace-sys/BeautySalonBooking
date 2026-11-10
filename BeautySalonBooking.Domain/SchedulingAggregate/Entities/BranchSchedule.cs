using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.BranchAggregate.Entities;

namespace BeautySalonBooking.Domain.SchedulingAggregate.Entities;

public class BranchSchedule : AuditableSoftDeleteEntity<long>
{
    public long BranchId { get; private set; }
    public Branch Branch { get; private set; } = null!;

    public DayOfWeek DayOfWeek { get; private set; }
    public bool IsWorkingDay { get; private set; }


    private readonly List<BranchWorkingHour> _workingHours = new();
    public IReadOnlyCollection<BranchWorkingHour> WorkingHours => _workingHours;


    private BranchSchedule() { }

    public BranchSchedule(long branchId, DayOfWeek dayOfWeek, bool isWorkingDay)
    {
        BranchId = branchId;
        DayOfWeek = dayOfWeek;
        IsWorkingDay = isWorkingDay;
    }

    public void Update(DayOfWeek dayOfWeek, bool isWorkingDay)
    {
        DayOfWeek = dayOfWeek;
        IsWorkingDay = isWorkingDay;
    }

    public void AddWorkingHour(BranchWorkingHour hour) => _workingHours.Add(hour);
    public void ClearWorkingHours() => _workingHours.Clear();
}