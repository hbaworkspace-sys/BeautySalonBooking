using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Contracts.Branch.Branch.Dtos
{
    public class UpdateBranchMemberDto
    {
        [Required] public long Id { get; set; }
        [Required] public int BranchRoleId { get; set; }
        [Required] public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }

}
