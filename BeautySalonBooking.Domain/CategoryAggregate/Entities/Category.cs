using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.CategoryAggregate.Enums;
using BeautySalonBooking.Domain.ServiceAggregate.Entities;

namespace BeautySalonBooking.Domain.CategoryAggregate.Entities;

public class Category : AuditableSoftDeleteEntity<int>
{
    public int? ParentId { get; private set; }
    public Category? Parent { get; private set; }

    public string Title { get; private set; } = null!;
    public string Code { get; private set; } = null!;
    public byte Level { get; private set; }
    public CategoryType Type { get; private set; }
    public string? Description { get; private set; }
    public int DisplayOrder { get; private set; }


    private readonly List<Category> _children = new();
    public IReadOnlyCollection<Category> Children => _children;

    private readonly List<Service> _services = new();
    public IReadOnlyCollection<Service> Services => _services;
}