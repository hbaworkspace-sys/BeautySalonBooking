using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Contracts.Branch.BranchService.Dtos
{
    public class CreateBranchServiceRequest
    {
        [Required] public long BranchId { get; set; }
        [Required] public long ServiceId { get; set; }
        [Required] public decimal Price { get; set; }
        [Required] public TimeSpan Duration { get; set; }
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }
        [Required] public DateOnly FromDate { get; set; }
        [Required] public DateOnly ToDate { get; set; }
    }
}
