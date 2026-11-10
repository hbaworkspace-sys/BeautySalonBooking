using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Scheduling.DTOs
{
    public class UpdateBranchScheduleDto
    {
        public long Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public bool IsWorkingDay { get; set; }
        public List<WorkingHourDto> WorkingHours { get; set; } = new();
    }
}
