using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.BranchAggregate.Entities;

namespace BeautySalonBooking.Domain.SchedulingAggregate.Entities;

public class BranchMemberSchedule : AuditableSoftDeleteEntity<long>
{
    public long BranchMemberId { get; private set; }
    public BranchMember BranchMember { get; private set; } = null!;


    private readonly List<WorkingShift> _workingShifts = new();
    public IReadOnlyCollection<WorkingShift> WorkingShifts => _workingShifts;


    private readonly List<ScheduleException> _exceptions = new();
    public IReadOnlyCollection<ScheduleException> Exceptions => _exceptions;


    private readonly List<TimeOff> _timeOffs = new();
    public IReadOnlyCollection<TimeOff> TimeOffs => _timeOffs;
}