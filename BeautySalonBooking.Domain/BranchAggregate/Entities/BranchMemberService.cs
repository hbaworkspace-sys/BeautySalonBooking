using BeautySalonBooking.Domain.Base.Entities;

namespace BeautySalonBooking.Domain.BranchAggregate.Entities;

public class BranchMemberService : AuditableSoftDeleteEntity<long>
{
    public long BranchMemberId { get; private set; }
    public BranchMember BranchMember { get; private set; } = null!;

    public long BranchServiceId { get; private set; }
    public BranchService BranchService { get; private set; } = null!;

    public decimal? Price { get; private set; }
    public TimeSpan? Duration { get; private set; }
}