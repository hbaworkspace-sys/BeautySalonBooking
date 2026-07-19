using BeautySalonBooking.Domain.Base.Entities;

namespace BeautySalonBooking.Domain.SchedulingAggregate.Entities;

public class TimeOff : AuditableSoftDeleteEntity<long>
{
    public long BranchMemberScheduleId { get; private set; }
    public BranchMemberSchedule BranchMemberSchedule { get; private set; } = null!;

    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public TimeOnly? StartTime { get; private set; }
    public TimeOnly? EndTime { get; private set; }
    public string? Reason { get; private set; }
}