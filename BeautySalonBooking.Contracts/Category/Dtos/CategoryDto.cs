namespace BeautySalonBooking.Contracts.Category.Dtos;

public class CategoryDto
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string Title { get; set; } = null!;
    public string Code { get; set; } = null!;
    public byte Level { get; set; }
    public int Type { get; set; }
    public string? Description { get; set; }
    public int DisplayOrder { get; set; }
    public string? Glyph { get; set; }
    public bool HasChildren { get; set; } // 👈 برای UI (نمایش فلش)
    public List<CategoryMediaDto> Media { get; set; } = new();
}

