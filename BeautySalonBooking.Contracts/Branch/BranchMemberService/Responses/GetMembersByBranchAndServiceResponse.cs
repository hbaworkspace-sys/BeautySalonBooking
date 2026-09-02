using BeautySalonBooking.Contracts.Branch.BranchMemberService.Dtos;

namespace BeautySalonBooking.Contracts.Branch.BranchMemberService.Responses;

public sealed class GetMembersByBranchAndServiceResponse
{
    public IReadOnlyList<BranchMemberServiceDto> Members { get; init; } = [];
}