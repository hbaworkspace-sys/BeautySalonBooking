using BeautySalonBooking.Domain.Base.Entities;

namespace BeautySalonBooking.Domain.SchedulingAggregate.Entities;

public class ScheduleException : AuditableSoftDeleteEntity<long>
{
    public long BranchMemberScheduleId { get; private set; }
    public BranchMemberSchedule BranchMemberSchedule { get; private set; } = null!;

    public DateOnly Date { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public bool IsWorkingDay { get; private set; }
    public string? Description { get; private set; }
}