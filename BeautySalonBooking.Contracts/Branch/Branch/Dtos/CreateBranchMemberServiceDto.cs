using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Contracts.Branch.Branch.Dtos
{
    public class CreateBranchMemberServiceDto
    {
        [Required] public long BranchServiceId { get; set; }
        public decimal? Price { get; set; }
        public TimeSpan? Duration { get; set; }
    }
}
