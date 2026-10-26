using BeautySalonBooking.Contracts.Branch.BranchMemberService.Responses;
using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.WebApp.Interfaces.Branches;

public interface IBranchMemberServiceApiService
{
    Task<ApiResponse_New<GetMembersByBranchAndServiceResponse>> GetAsync(
        long branchId,
        long serviceId,
        CancellationToken cancellationToken = default);
}