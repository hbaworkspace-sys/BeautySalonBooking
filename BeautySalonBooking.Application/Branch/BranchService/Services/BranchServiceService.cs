using BeautySalonBooking.Application.Branch.BranchService.Interfaces;
using BeautySalonBooking.Contracts.Branch.BranchService.Dtos;
using BeautySalonBooking.Contracts.Branch.BranchService.Enums;
using BeautySalonBooking.Contracts.Branch.BranchService.Requests;
using BeautySalonBooking.Contracts.Branch.BranchService.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Domain.BranchAggregate.Repositories;

namespace BeautySalonBooking.Application.Branch.BranchService.Services;

public sealed class BranchServiceService : IBranchServiceService
{
    private readonly IBranchServiceRepository _branchServiceRepository;

    public BranchServiceService(
        IBranchServiceRepository branchServiceRepository)
    {
        _branchServiceRepository = branchServiceRepository;
    }

    public async Task<ApiResponse_New<GetBranchesByServiceResponse>>
        GetBranchesByServiceAsync(
            GetBranchesByServiceRequest request,
            CancellationToken cancellationToken = default)
    {
        var results =
            await _branchServiceRepository
                .GetBranchesByServiceIdAsync(
                    request.ServiceId,
                    cancellationToken);

        var response = new GetBranchesByServiceResponse
        {
            Organizations = results
                .Select(x => new BranchServiceOrganizationDto
                {
                    BranchServiceId = x.BranchServiceId,

                    BranchId = x.BranchId,
                    BranchTitle = x.BranchTitle,
                    BranchDescription = x.BranchDescription,

                    OrganizationId = x.OrganizationId,
                    OrganizationTitle = x.OrganizationTitle,
                    OrganizationType = (OrganizationType)x.OrganizationType,

                    Price = x.Price,
                    Duration = x.Duration,
                    DisplayOrder = x.DisplayOrder
                })
                .ToList()
        };

        return new ApiResponse_New<GetBranchesByServiceResponse>
        {
            IsSuccess = true,
            Code = 200,
            Payload = response
        };
    }
}