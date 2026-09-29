using BeautySalonBooking.Contracts.Branch.Branch.Responses;
using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.Application.Branch.Branch.Interfaces;

public interface IBranchService
{
    Task<ApiResponse_New<GetBranchesResponse>> GetAllAsync(
    CancellationToken cancellationToken);
}