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
    public string? Glyph { get; private set; }

    private readonly List<Category> _children = new();
    public IReadOnlyCollection<Category> Children => _children;

    private readonly List<Service> _services = new();
    public IReadOnlyCollection<Service> Services => _services;

    public ICollection<CategoryMedia> Media { get; private set; } = new List<CategoryMedia>();

    // 👇 سازنده خالی برای EF Core
    private Category() { }

    // 👇 سازنده برای ریشه (بدون والد)
    public Category(
        string title,
        string code,
        CategoryType type,
        string? description = null,
        int displayOrder = 0,
        string? glyph = null)
    {
        Title = title;
        Code = code;
        Type = type;
        Description = description;
        DisplayOrder = displayOrder;
        Glyph = glyph;
        Level = 1;
        ParentId = null;
    }

    // 👇 سازنده برای فرزند (با والد)
    public Category(
        string title,
        string code,
        CategoryType type,
        int parentId,
        byte parentLevel,
        string? description = null,
        int displayOrder = 0,
        string? glyph = null)
    {
        Title = title;
        Code = code;
        Type = type;
        Description = description;
        DisplayOrder = displayOrder;
        Glyph = glyph;
        ParentId = parentId;
        Level = (byte)(parentLevel + 1);
    }

    // 👇 متد Update
    public void Update(
        string title,
        string code,
        CategoryType type,
        string? description,
        int displayOrder,
        string? glyph)
    {
        Title = title;
        Code = code;
        Type = type;
        Description = description;
        DisplayOrder = displayOrder;
        Glyph = glyph;
    }

    // 👇 متدهای مدیریت Media
    public void AddMedia(CategoryMedia media) => Media.Add(media);
    public void RemoveMedia(CategoryMedia media) => Media.Remove(media);
}