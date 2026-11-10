using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Contracts.Branch.Branch.Dtos
{
    public class UpdateBranchMemberServiceDto
    {
        [Required] public long Id { get; set; }
        public decimal? Price { get; set; }
        public TimeSpan? Duration { get; set; }
    }

}
