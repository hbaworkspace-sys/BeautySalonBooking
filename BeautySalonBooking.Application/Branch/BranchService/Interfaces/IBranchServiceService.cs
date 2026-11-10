using BeautySalonBooking.Contracts.Branch.BranchService.Dtos;
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

    Task<ApiResponse_New<GetBranchServicesResponse>> GetFilteredAsync(
       GetBranchServicesRequest request, CancellationToken ct = default);

    Task<ApiResponse_New<long>> CreateAsync(
        CreateBranchServiceRequest request, CancellationToken ct = default);

    Task<ApiResponse_New<bool>> UpdateAsync(
        UpdateBranchServiceRequest request, CancellationToken ct = default);

    Task<ApiResponse_New<bool>> DeleteAsync(long id, CancellationToken ct = default);
}