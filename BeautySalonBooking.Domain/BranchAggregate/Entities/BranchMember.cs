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

    private BranchMember() { }

    public BranchMember(long branchId, long personId, int branchRoleId, DateOnly startDate)
    {
        BranchId = branchId;
        PersonId = personId;
        BranchRoleId = branchRoleId;
        StartDate = startDate;
    }

    // 👇 متد Update
    public void Update(int branchRoleId, DateOnly startDate, DateOnly? endDate)
    {
        BranchRoleId = branchRoleId;
        StartDate = startDate;
        EndDate = endDate;
    }

    // 👇 متدهای مدیریت سرویس‌ها
    public void AddService(BranchMemberService service) => _services.Add(service);
    public void RemoveService(BranchMemberService service) => _services.Remove(service);
}