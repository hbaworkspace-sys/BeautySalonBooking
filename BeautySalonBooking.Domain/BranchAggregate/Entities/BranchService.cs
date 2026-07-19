using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.ServiceAggregate.Entities;

namespace BeautySalonBooking.Domain.BranchAggregate.Entities;

public class BranchService : AuditableSoftDeleteEntity<long>
{
    public long BranchId { get; private set; }
    public Branch Branch { get; private set; } = null!;

    public long ServiceId { get; private set; }
    public Service Service { get; private set; } = null!;

    public decimal Price { get; private set; }
    public TimeSpan Duration { get; private set; }
    public string? Description { get; private set; }
    public int DisplayOrder { get; private set; }
    public DateOnly FromDate { get; private set; }
    public DateOnly ToDate { get; private set; }


    private readonly List<BranchMemberService> _branchMemberServices = new();
    public IReadOnlyCollection<BranchMemberService> BranchMemberServices => _branchMemberServices;
}