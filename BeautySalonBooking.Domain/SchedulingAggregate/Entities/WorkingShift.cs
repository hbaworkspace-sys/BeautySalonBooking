using BeautySalonBooking.Domain.Base.Entities;

namespace BeautySalonBooking.Domain.SchedulingAggregate.Entities;

public class WorkingShift : AuditableSoftDeleteEntity<long>
{
    public long BranchMemberScheduleId { get; private set; }
    public BranchMemberSchedule BranchMemberSchedule { get; private set; } = null!;

    public DayOfWeek DayOfWeek { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
}