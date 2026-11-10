using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Contracts.Branch.Branch.Dtos
{
    public class CreateBranchMemberDto
    {
        [Required] public long BranchId { get; set; }
        [Required] public long PersonId { get; set; }
        [Required] public int BranchRoleId { get; set; }
        [Required] public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        // 👇 سرویس‌هایی که این عضو ارائه می‌دهد
        public List<CreateBranchMemberServiceDto> Services { get; set; } = new();
    }
}
