using BeautySalonBooking.Contracts.Branch.BranchMemberService.Requests;
using BeautySalonBooking.Contracts.Branch.BranchMemberService.Responses;
using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.Application.Branch.BranchMemberService.Interfaces;

public interface IBranchMemberServiceService
{
    /// <summary>
    /// دریافت اعضای فعال یک شعبه که سرویس مشخص‌شده را ارائه می‌دهند.
    /// </summary>
    Task<ApiResponse_New<GetMembersByBranchAndServiceResponse>>
        GetMembersByBranchAndServiceAsync(
            GetMembersByBranchAndServiceRequest request,
            CancellationToken cancellationToken = default);
}