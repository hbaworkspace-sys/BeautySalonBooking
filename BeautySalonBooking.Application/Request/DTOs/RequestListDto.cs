using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Request.DTOs
{
    public class RequestListDto
    {
        public long Id { get; set; }
        public string TrackingCode { get; set; } = null!;
        public string RequestTypeTitle { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime SubmittedAt { get; set; }
        public int DocumentsCount { get; set; }
    }
}
