using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Contracts.Service.Responses;
using BeautySalonBooking.WebApp.Interfaces.Common;
using BeautySalonBooking.WebApp.Interfaces.Services;

namespace BeautySalonBooking.WebApp.Services.Services;

public class ServiceApiService : IServiceApiService
{
    private readonly IApiClient _apiClient;
    private readonly ILogger<ServiceApiService> _logger;

    public ServiceApiService(IApiClient apiClient, ILogger<ServiceApiService> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    public async Task<ApiResponse_New<GetServicesByCategoryResponse>> GetByCategoryAsync(
    int categoryId,
    CancellationToken cancellationToken = default)
    {
        try
        {
            var endpoint = $"api/Service/by-category?categoryId={categoryId}";

            // ✅ اندپوینت public — نیازی به لاگین ندارد
            return await _apiClient.GetPublicAsync<ApiResponse_New<GetServicesByCategoryResponse>>(
                endpoint, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting services for categoryId: {CategoryId}", categoryId);
            return new ApiResponse_New<GetServicesByCategoryResponse>
            {
                IsSuccess = false,
                Message = "خطا در دریافت سرویس‌ها"
            };
        }
    }
}