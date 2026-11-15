using BeautySalonBooking.Contracts.Category.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Contracts.Category.Requests
{
    public class CreateCategoryRequest
    {
        [Required] public string Title { get; set; } = null!;
        [Required] public string Code { get; set; } = null!;
        [Required] public CategoryType Type { get; set; }
        public int? ParentId { get; set; } // 👈 اگر null باشد، ریشه است
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }
        public string? Glyph { get; set; }

        // 👇 رسانه‌های اختیاری
        public List<CreateCategoryMediaRequest> Media { get; set; } = new();
    }
    public class CreateCategoryMediaRequest
    {
        [Required] public string FileName { get; set; } = null!;
        [Required] public string ContentType { get; set; } = null!;
        [Required] public string Base64Content { get; set; } = null!;
        public int DisplayOrder { get; set; }
    }
}
