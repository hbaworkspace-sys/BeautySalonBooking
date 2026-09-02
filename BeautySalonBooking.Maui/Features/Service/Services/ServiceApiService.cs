using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Contracts.Service.Responses;
using BeautySalonBooking.Maui.Common.Services;
using BeautySalonBooking.Maui.Features.Service.Constants;
using Microsoft.Extensions.Logging;

namespace BeautySalonBooking.Maui.Features.Service.Services;

public sealed class ServiceApiService : BaseApiService, IServiceApiService
{
    public ServiceApiService(
        HttpClient httpClient,
        ILogger<ServiceApiService> logger)
        : base(httpClient, logger)
    {
    }

    public Task<ApiResponse_New<GetServicesByCategoryResponse>> GetByCategoryAsync(
        int categoryId,
        CancellationToken cancellationToken = default)
        => GetAsync<GetServicesByCategoryResponse>(
            $"{ServiceRoutes.ByCategory}?categoryId={categoryId}",
            cancellationToken);
}