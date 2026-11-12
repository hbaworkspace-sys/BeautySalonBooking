

using BeautySalonBooking.Contracts.Branch.BranchService.Enums;
using BeautySalonBooking.Contracts.Organization.Enums;

namespace BeautySalonBooking.Contracts.Organization.Dtos;

// ================= خروجی کامل سازمان =================
public class OrganizationFullDetailDto
{
    // اطلاعات پایه سازمان
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string LicenseNumber { get; set; } = null!;
    public OrganizationType Type { get; set; }
    public string? Slogan { get; set; }
    public string? Signature { get; set; }
    public string? Glyph { get; set; }
    public DateTime CreatedAt { get; set; }

    // جزئیات سازمان (یک به یک)
    public OrganizationDetailDto? Detail { get; set; }

    // مالکان
    public List<OrganizationOwnerDto> Owners { get; set; } = new();

    // رسانه‌ها (تصاویر و مدارک)
    public List<OrganMediaDto> Media { get; set; } = new();

    // شعبه‌ها
    public List<BranchSummaryDto> Branches { get; set; } = new();

    // آمار کلی
    public OrganizationStatisticsDto Statistics { get; set; } = new();
}

// ================= جزئیات سازمان =================
public class OrganizationDetailDto
{
    public string? Description { get; set; }
    public string? Website { get; set; }
    public string? Instagram { get; set; }
    public string? Email { get; set; }
    public string? Whatsapp { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}

// ================= مالک =================
public class OrganizationOwnerDto
{
    public long OwnerUserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsPrimary { get; set; }
}

// ================= رسانه =================
public class OrganMediaDto
{
    public long Id { get; set; }
    public string FileName { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public long FileSize { get; set; }
    public string Base64Content { get; set; } = null!; // 👈 تبدیل byte[] به Base64
    public MediaType Type { get; set; }
    public int DisplayOrder { get; set; }
}

// ================= خلاصه شعبه =================
public class BranchSummaryDto
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? Glyph { get; set; }
    public int MemberCount { get; set; }
    public int ServiceCount { get; set; }
    public int MediaCount { get; set; }
    public List<BranchMemberSummaryDto> Members { get; set; } = new();
}

// ================= خلاصه عضو شعبه =================
public class BranchMemberSummaryDto
{
    public long Id { get; set; }
    public long PersonId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? BranchRoleTitle { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public int ServiceCount { get; set; }
}

// ================= آمار =================
public class OrganizationStatisticsDto
{
    public int TotalBranches { get; set; }
    public int TotalMembers { get; set; }
    public int TotalServices { get; set; }
    public int TotalMedia { get; set; }
    public int TotalRequests { get; set; }
    public int PendingRequests { get; set; }
    public int ApprovedRequests { get; set; }
    public int RejectedRequests { get; set; }
    public int TotalOrganizationDocuments { get; set; }
}