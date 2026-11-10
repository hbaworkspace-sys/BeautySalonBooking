using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Contracts.Branch.BranchService.Dtos
{
    public class GetBranchServicesRequest
    {
        public long? BranchId { get; set; }        // فیلتر بر اساس شعبه
        public long? ServiceId { get; set; }       // فیلتر بر اساس سرویس
        public decimal? MinPrice { get; set; }     // حداقل قیمت
        public decimal? MaxPrice { get; set; }     // حداکثر قیمت
        public string? SearchTerm { get; set; }    // جستجو در توضیحات
        public bool? IsActive { get; set; }        // فقط فعال‌ها
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; } = "DisplayOrder"; // یا "Price", "CreatedAt"
        public bool SortDescending { get; set; } = false;
    }
}
