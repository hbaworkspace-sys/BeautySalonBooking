using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Contracts.Branch.Branch.Dtos
{
    public class GetBranchMembersRequest
    {
        public long? BranchId { get; set; }
        public int? BranchRoleId { get; set; }
        public string? SearchTerm { get; set; }
        public bool? IsActive { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
