using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.GeographyAggregate.Enums;

namespace BeautySalonBooking.Domain.GeographyAggregate.Entities;

public class Region : AuditableSoftDeleteEntity<int>
{
    public int? ParentId { get; private set; }
    public Region? Parent { get; private set; }

    public string Name { get; private set; } = null!;
    public string Code { get; private set; } = null!;
    public RegionType Type { get; private set; }


    private readonly List<Region> _children = new();
    public IReadOnlyCollection<Region> Children => _children;
}