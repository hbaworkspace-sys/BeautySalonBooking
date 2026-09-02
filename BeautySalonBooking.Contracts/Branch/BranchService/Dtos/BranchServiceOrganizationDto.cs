using BeautySalonBooking.Contracts.Branch.BranchService.Enums;

namespace BeautySalonBooking.Contracts.Branch.BranchService.Dtos;

public sealed class BranchServiceOrganizationDto
{
    public long BranchServiceId { get; init; }

    public long BranchId { get; init; }
    public string BranchTitle { get; init; } = string.Empty;
    public string? BranchDescription { get; init; }

    public long OrganizationId { get; init; }
    public string OrganizationTitle { get; init; } = string.Empty;
    public OrganizationType OrganizationType { get; init; }

    public decimal Price { get; init; }
    public TimeSpan Duration { get; init; }
    public int DisplayOrder { get; init; }
}