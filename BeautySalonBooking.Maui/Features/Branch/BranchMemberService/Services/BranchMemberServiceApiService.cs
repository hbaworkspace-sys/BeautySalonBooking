using BeautySalonBooking.Contracts.Branch.BranchMemberService.Requests;
using BeautySalonBooking.Contracts.Branch.BranchMemberService.Responses;
using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.Maui.Features.Branch.BranchMemberService.Services;

public interface IBranchMemberServiceApiService
{
    Task<ApiResponse_New<GetMembersByBranchAndServiceResponse>> GetAsync(
        GetMembersByBranchAndServiceRequest request,
        CancellationToken cancellationToken = default);
}