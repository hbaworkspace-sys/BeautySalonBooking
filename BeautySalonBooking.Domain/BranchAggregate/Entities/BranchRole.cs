using BeautySalonBooking.Domain.Base.Entities;

namespace BeautySalonBooking.Domain.BranchAggregate.Entities;

public class BranchRole : AuditableSoftDeleteEntity<int>
{
    public string Title { get; private set; } = null!;
    public string Code { get; private set; } = null!;
    public string? Description { get; private set; }

    private readonly List<BranchMember> _members = new();
    public IReadOnlyCollection<BranchMember> Members => _members;
}
