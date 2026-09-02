using BeautySalonBooking.Contracts.Branch.BranchMemberService.Requests;
using BeautySalonBooking.Contracts.Branch.BranchMemberService.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Maui.Common.Services;
using Microsoft.Extensions.Logging;

namespace BeautySalonBooking.Maui.Features.Branch.BranchMemberService.Services;

public sealed class BranchMemberServiceApiService
    : BaseApiService,
      IBranchMemberServiceApiService
{
    public BranchMemberServiceApiService(
        HttpClient httpClient,
        ILogger<BranchMemberServiceApiService> logger)
        : base(httpClient, logger)
    {
    }

    public Task<ApiResponse_New<GetMembersByBranchAndServiceResponse>> GetAsync(
        GetMembersByBranchAndServiceRequest request,
        CancellationToken cancellationToken = default)
    {
        var url =
            $"{BranchMemberServiceRoutes.MembersByBranchAndService}" +
            $"?branchId={request.BranchId}" +
            $"&serviceId={request.ServiceId}";

        return GetAsync<GetMembersByBranchAndServiceResponse>(
            url,
            cancellationToken);
    }
}