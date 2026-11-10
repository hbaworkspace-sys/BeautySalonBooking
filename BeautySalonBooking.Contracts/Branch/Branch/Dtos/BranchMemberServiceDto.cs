using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Contracts.Branch.Branch.Dtos
{
    public class BranchMemberServiceDto
    {
        public long Id { get; set; }
        public long BranchMemberId { get; set; }
        public long BranchServiceId { get; set; }
        public string? BranchServiceTitle { get; set; }
        public decimal? Price { get; set; }
        public TimeSpan? Duration { get; set; }
    }
}
