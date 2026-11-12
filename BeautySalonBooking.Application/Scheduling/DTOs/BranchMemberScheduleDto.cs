using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Scheduling.DTOs
{
    public class BranchMemberScheduleDto
    {
        public long Id { get; set; }
        public long BranchMemberId { get; set; }
        public string? PersonFullName { get; set; }
        public string? BranchTitle { get; set; }
        public string? BranchRoleTitle { get; set; }
        public List<WorkingShiftDto> WorkingShifts { get; set; } = new();
        public List<ScheduleExceptionDto> Exceptions { get; set; } = new();
        public List<TimeOffDto> TimeOffs { get; set; } = new();
    }
}
