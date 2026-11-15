namespace BeautySalonBooking.WebApp.Models.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public byte Level { get; set; }
        public int Type { get; set; }
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }
        public string? Glyph { get; set; }
        public bool HasChildren { get; set; }
        public List<CategoryMediaDto> Media { get; set; } = new();
    }

    public class CategoryMediaDto
    {
        public long Id { get; set; } // 👈 این خط را اضافه کنید
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public byte[] Content { get; set; } = Array.Empty<byte>();
        public int DisplayOrder { get; set; }
    }
}
