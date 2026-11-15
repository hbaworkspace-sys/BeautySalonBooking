namespace BeautySalonBooking.WebApp.Models.Category
{
    public class CreateCategoryRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int Type { get; set; }
        public int? ParentId { get; set; }
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }
        public string? Glyph { get; set; }
        public List<CreateCategoryMediaRequest> Media { get; set; } = new();
    }

    public class CreateCategoryMediaRequest
    {
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public string Base64Content { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
    }
}
