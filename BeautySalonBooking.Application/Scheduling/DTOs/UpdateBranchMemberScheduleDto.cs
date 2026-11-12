using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Scheduling.DTOs
{
    public class UpdateBranchMemberScheduleDto
    {
        [Required] public long Id { get; set; }
        public List<WorkingShiftDto> WorkingShifts { get; set; } = new();
        public List<ScheduleExceptionDto> Exceptions { get; set; } = new();
        public List<TimeOffDto> TimeOffs { get; set; } = new();
    }

}
