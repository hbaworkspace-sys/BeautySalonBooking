using BeautySalonBooking.Domain.Base.Entities;

namespace BeautySalonBooking.Domain.SchedulingAggregate.Entities;

public class BranchWorkingHour : AuditableSoftDeleteEntity<long>
{
    public long BranchScheduleId { get; private set; }
    public BranchSchedule BranchSchedule { get; private set; } = null!;

    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
}