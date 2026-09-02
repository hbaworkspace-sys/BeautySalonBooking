using BeautySalonBooking.Contracts.Branch.BranchService.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Maui.Common.Services;
using BeautySalonBooking.Maui.Features.Branch.BranchService.Constants;
using Microsoft.Extensions.Logging;

namespace BeautySalonBooking.Maui.Features.Branch.BranchService.Services;

public sealed class BranchServiceApiService
    : BaseApiService,
      IBranchServiceApiService
{
    public BranchServiceApiService(
        HttpClient httpClient,
        ILogger<BranchServiceApiService> logger)
        : base(httpClient, logger)
    {
    }

    public Task<ApiResponse_New<GetBranchesByServiceResponse>>
        GetOrganizationsByServiceAsync(
            long serviceId,
            CancellationToken cancellationToken = default)
        => GetAsync<GetBranchesByServiceResponse>(
            $"{BranchServiceRoutes.BranchesByService}?serviceId={serviceId}",
            cancellationToken);
}