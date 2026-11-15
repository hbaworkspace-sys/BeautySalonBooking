namespace BeautySalonBooking.WebApp.Models.Category
{
    public class UpdateCategoryRequest
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int Type { get; set; }
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }
        public string? Glyph { get; set; }
        public List<long> MediaIdsToKeep { get; set; } = new(); // 👈 شناسه تصاویری که باید بمانند
        public List<CreateCategoryMediaRequest> Media { get; set; } = new(); // 👈 تصاویر جدید

    }
}
