using BeautySalonBooking.Contracts.Branch.BranchService.Dtos;

namespace BeautySalonBooking.Contracts.Branch.BranchService.Responses;

public sealed class GetBranchesByServiceResponse
{
    public IReadOnlyList<BranchServiceOrganizationDto> Organizations { get; init; } = [];
}