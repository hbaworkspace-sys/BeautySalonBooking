using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.PersonAggregate.Entities;

namespace BeautySalonBooking.Domain.BranchAggregate.Entities;

public class BranchMember : AuditableSoftDeleteEntity<long>
{
    public long BranchId { get; private set; }
    public Branch Branch { get; private set; } = null!;

    public long PersonId { get; private set; }
    public Person Person { get; private set; } = null!;

    public int BranchRoleId { get; private set; }
    public BranchRole BranchRole { get; private set; } = null!;

    public DateOnly StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }


    private readonly List<BranchMemberService> _services = new();
    public IReadOnlyCollection<BranchMemberService> Services => _services;
}