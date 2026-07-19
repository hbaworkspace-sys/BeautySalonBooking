using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.OrganizationAggregate.Entities;

namespace BeautySalonBooking.Domain.BranchAggregate.Entities;

public class Branch : AuditableSoftDeleteEntity<long>
{
    public long OrganizationId { get; private set; }
    public Organization Organization { get; private set; } = null!;

    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }


    private readonly List<BranchMember> _members = new();
    public IReadOnlyCollection<BranchMember> Members => _members;


    private readonly List<BranchService> _services = new();
    public IReadOnlyCollection<BranchService> Services => _services;
}