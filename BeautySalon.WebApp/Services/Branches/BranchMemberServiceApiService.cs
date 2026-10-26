using BeautySalonBooking.Contracts.Branch.BranchMemberService.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.WebApp.Interfaces.Branches;
using BeautySalonBooking.WebApp.Interfaces.Common;

namespace BeautySalonBooking.WebApp.Services.Branches;

public class BranchMemberServiceApiService : IBranchMemberServiceApiService
{
    private readonly IApiClient _apiClient;
    private readonly ILogger<BranchMemberServiceApiService> _logger;

    public BranchMemberServiceApiService(IApiClient apiClient, ILogger<BranchMemberServiceApiService> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    public async Task<ApiResponse_New<GetMembersByBranchAndServiceResponse>> GetAsync(
        long branchId,
        long serviceId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var endpoint = $"api/BranchMemberService/by-branch-and-service?branchId={branchId}&serviceId={serviceId}";

            return await _apiClient.GetPublicAsync<ApiResponse_New<GetMembersByBranchAndServiceResponse>>(
                endpoint, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting members for branchId: {BranchId}, serviceId: {ServiceId}", branchId, serviceId);
            return new ApiResponse_New<GetMembersByBranchAndServiceResponse>
            {
                IsSuccess = false,
                Message = "خطا در دریافت لیست متخصصین"
            };
        }
    }
}