using BeautySalonBooking.Domain.RequestAggregate.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Request.DTOs
{

    public class CreateFullRequestDto
    {
        // 👈 UserId حذف شد، از توکن گرفته می‌شود
        [Required] public int RequestTypeId { get; set; }
        [Required] public RequestStatus InitialStatus { get; set; } // 👈 وضعیت اولیه از ورودی

        // Person Detail
        public string? Address { get; set; }
        public string? CodePosti { get; set; }
        public string? LicenseNo { get; set; }
        public string? Biography { get; set; }
        public int? ExperienceYears { get; set; }

        public List<RequestDocumentUploadDto> Documents { get; set; } = new();
        public string? Description { get; set; } // برای تاریخچه اولیه
    }

    public class ChangeRequestStatusDto
    {
        [Required] public long RequestId { get; set; }
        [Required] public RequestStatus NewStatus { get; set; }
        [Required] public RequestHistoryAction Action { get; set; } // مثلا Approved, Rejected
        public string? Description { get; set; }
    }
}
