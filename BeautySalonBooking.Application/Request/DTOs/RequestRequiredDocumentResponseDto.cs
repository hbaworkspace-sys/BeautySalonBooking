using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Request.DTOs
{
    public class RequestRequiredDocumentResponseDto
    {
        public int Id { get; set; }
        public int RequestTypeId { get; set; }
        public string RequestTypeTitle { get; set; } = null!;
        public int DocumentTypeId { get; set; }
        public string DocumentTypeTitle { get; set; } = null!;
        public bool IsRequired { get; set; }
        public int DisplayOrder { get; set; }
    }
}
