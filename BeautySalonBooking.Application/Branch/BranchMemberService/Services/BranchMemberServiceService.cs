using BeautySalonBooking.Application.Branch.BranchMemberService.Interfaces;
using BeautySalonBooking.Contracts.Branch.BranchMemberService.Dtos;
using BeautySalonBooking.Contracts.Branch.BranchMemberService.Requests;
using BeautySalonBooking.Contracts.Branch.BranchMemberService.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Domain.BranchAggregate.Repositories;

namespace BeautySalonBooking.Application.Branch.BranchMemberService.Services;

public sealed class BranchMemberServiceService
    : IBranchMemberServiceService
{
    private readonly IBranchMemberRepository _branchMemberRepository;

    public BranchMemberServiceService(
        IBranchMemberRepository branchMemberRepository)
    {
        _branchMemberRepository = branchMemberRepository;
    }

    public async Task<ApiResponse_New<GetMembersByBranchAndServiceResponse>>
        GetMembersByBranchAndServiceAsync(
            GetMembersByBranchAndServiceRequest request,
            CancellationToken cancellationToken = default)
    {
        if (request.BranchId <= 0 ||
            request.ServiceId <= 0)
        {
            return new ApiResponse_New<GetMembersByBranchAndServiceResponse>
            {
                IsSuccess = false,
                Code = 400,
                Message = "شناسه شعبه و سرویس معتبر نیست."
            };
        }

        var members =
            await _branchMemberRepository.GetByBranchAndServiceAsync(
                request.BranchId,
                request.ServiceId,
                cancellationToken);

        var response = new GetMembersByBranchAndServiceResponse
        {
            Members = members
                .Select(member => new BranchMemberServiceDto
                {
                    BranchMemberId = member.BranchMemberId,
                    BranchMemberServiceId = member.BranchMemberServiceId,
                    PersonId = member.PersonId,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    Price = member.Price,
                    Duration = member.Duration
                })
                .ToList()
        };

        return new ApiResponse_New<GetMembersByBranchAndServiceResponse>
        {
            IsSuccess = true,
            Code = 200,
            Payload = response
        };
    }
}