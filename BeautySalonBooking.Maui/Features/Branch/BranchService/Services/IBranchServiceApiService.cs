using BeautySalonBooking.Contracts.Branch.BranchService.Responses;
using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.Maui.Features.Branch.BranchService.Services;

public interface IBranchServiceApiService
{
    Task<ApiResponse_New<GetBranchesByServiceResponse>>
        GetOrganizationsByServiceAsync(
            long serviceId,
            CancellationToken cancellationToken = default);
}