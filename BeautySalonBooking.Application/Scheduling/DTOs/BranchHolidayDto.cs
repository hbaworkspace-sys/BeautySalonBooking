using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Scheduling.DTOs
{
    public class BranchHolidayDto
    {
        public long Id { get; set; }
        public long BranchId { get; set; }
        public DateOnly Date { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsNationalHoliday { get; set; }
    }
}
