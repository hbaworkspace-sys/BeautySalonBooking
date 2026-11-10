using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Scheduling.DTOs
{
    public class CreateScheduleExceptionDto
    {
        public long BranchMemberScheduleId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool IsWorkingDay { get; set; }
        public string? Description { get; set; }
    }

}
