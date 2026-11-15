using BeautySalonBooking.Contracts.Category.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Contracts.Category.Requests
{
    public class UpdateCategoryRequest
    {
        [Required] public int Id { get; set; }
        [Required] public string Title { get; set; } = null!;
        [Required] public string Code { get; set; } = null!;
        [Required] public CategoryType Type { get; set; }
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }
        public string? Glyph { get; set; }
        public List<long> MediaIdsToKeep { get; set; } = new(); // 👈 شناسه تصاویری که باید بمانند
        public List<CreateCategoryMediaRequest> Media { get; set; } = new();
    }

    public class GetRootCategoriesRequest
    {
        public int? Type { get; set; } // فیلتر بر اساس نوع (Service, Specialty, Product)
    }
}
