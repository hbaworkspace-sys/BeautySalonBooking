using BeautySalonBooking.Contracts.Branch.BranchService.Requests;
using BeautySalonBooking.Contracts.Branch.BranchService.Responses;
using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.Application.Branch.BranchService.Interfaces;

public interface IBranchServiceService
{
    Task<ApiResponse_New<GetBranchesByServiceResponse>>
        GetBranchesByServiceAsync(
            GetBranchesByServiceRequest request,
            CancellationToken cancellationToken = default);
}