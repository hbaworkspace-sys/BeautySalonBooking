using BeautySalonBooking.Contracts.Branch.BranchService.Responses;

using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.WebApp.Interfaces.Branches;
using BeautySalonBooking.WebApp.Interfaces.Common;

namespace BeautySalonBooking.WebApp.Services.Branches;

public class BranchServiceApiService : IBranchServiceApiService
{
    private readonly IApiClient _apiClient;
    private readonly ILogger<BranchServiceApiService> _logger;

    public BranchServiceApiService(IApiClient apiClient, ILogger<BranchServiceApiService> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    public async Task<ApiResponse_New<GetBranchesByServiceResponse>> GetOrganizationsByServiceAsync(
        long serviceId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // استفاده از متد عمومی برای درخواست‌های بدون لاگین
            var endpoint = $"api/BranchService/by-service?serviceId={serviceId}";

            return await _apiClient.GetPublicAsync<ApiResponse_New<GetBranchesByServiceResponse>>(
                endpoint, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting branches for serviceId: {ServiceId}", serviceId);
            return new ApiResponse_New<GetBranchesByServiceResponse>
            {
                IsSuccess = false,
                Message = "خطا در دریافت لیست سالن‌ها"
            };
        }
    }
}