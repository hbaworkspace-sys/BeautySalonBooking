using BeautySalonBooking.Domain.Base.Entities;

namespace BeautySalonBooking.Domain.SchedulingAggregate.Entities;

public class WorkingShift : AuditableSoftDeleteEntity<long>
{
    public long BranchMemberScheduleId { get; private set; }
    public BranchMemberSchedule BranchMemberSchedule { get; private set; } = null!;

    public DayOfWeek DayOfWeek { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public void SetBranchMemberScheduleId(long id) => BranchMemberScheduleId = id;
    private WorkingShift() { }
    public WorkingShift(DayOfWeek dayOfWeek, TimeOnly startTime, TimeOnly endTime)
    {
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        EndTime = endTime;
    }

}