using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Request.DTOs
{
    // ================= Organization Upgrade Request =================
    public class CreateOrganizationUpgradeRequestDto
    {
        [Required] public string OrganizationName { get; set; } = null!;
        public string? OrganizationDescription { get; set; }
        public string? PhoneNumber { get; set; }
        public string? MobileNumber { get; set; }
        public string? Website { get; set; }
        public string? Instagram { get; set; }
        public string? Email { get; set; }
        public string? Whatsapp { get; set; }

        public string? BranchName { get; set; }
        public string? BranchDescription { get; set; }
        public int? RegionId { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }

        [Required] public string LicenseNumber { get; set; } = null!;
        public DateTime? LicenseIssuedDate { get; set; }
        public DateTime? LicenseExpireDate { get; set; }

        public string? Slogan { get; set; }
        public string? AdditionalInfo { get; set; }

        public List<RequestDocumentUploadDto> Documents { get; set; } = new();
    }

    public class ApproveOrganizationUpgradeRequestDto
    {
        [Required] public long RequestId { get; set; }
        public string? ReviewDescription { get; set; }
    }
}
