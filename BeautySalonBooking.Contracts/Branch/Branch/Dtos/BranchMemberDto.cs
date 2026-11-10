using BeautySalonBooking.Contracts.Branch.BranchMemberService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Contracts.Branch.Branch.Dtos
{
    public class BranchMemberDto
    {
        public long Id { get; set; }
        public long BranchId { get; set; }
        public string? BranchTitle { get; set; }
        public long PersonId { get; set; }
        public string? PersonFullName { get; set; }
        public string? PersonPhoneNumber { get; set; }
        public int BranchRoleId { get; set; }
        public string? BranchRoleTitle { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public List<BranchMemberServiceDto> Services { get; set; } = new();
    }
}
