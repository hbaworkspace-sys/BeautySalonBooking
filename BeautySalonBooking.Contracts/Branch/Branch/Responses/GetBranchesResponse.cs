using BeautySalonBooking.Contracts.Branch.Branch.Dtos;

namespace BeautySalonBooking.Contracts.Branch.Branch.Responses;

public class GetBranchesResponse
{
    public IReadOnlyList<BranchDto> Branches { get; init; } = [];

}