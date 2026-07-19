using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.BranchAggregate.Entities;
using BeautySalonBooking.Domain.CategoryAggregate.Entities;

namespace BeautySalonBooking.Domain.ServiceAggregate.Entities;

public class Service : AuditableSoftDeleteEntity<long>
{
    public int CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;

    public string Title { get; private set; } = null!;
    public string Code { get; private set; } = null!;
    public string? Description { get; private set; }
    public decimal BasePrice { get; private set; }
    public TimeSpan BaseDuration { get; private set; }


    private readonly List<BranchService> _branchServices = new();
    public IReadOnlyCollection<BranchService> BranchServices => _branchServices;
}