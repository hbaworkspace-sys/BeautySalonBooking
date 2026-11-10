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

    private BranchMemberService() { }

    public BranchMemberService(long branchServiceId, decimal? price = null, TimeSpan? duration = null)
    {
        BranchServiceId = branchServiceId;
        Price = price;
        Duration = duration;
    }

    public void SetBranchMemberId(long branchMemberId) => BranchMemberId = branchMemberId;

    public void Update(decimal? price, TimeSpan? duration)
    {
        Price = price;
        Duration = duration;
    }
}
