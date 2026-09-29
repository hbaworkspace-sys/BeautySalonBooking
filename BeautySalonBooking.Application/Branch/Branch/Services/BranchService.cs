using BeautySalonBooking.Application.Branch.Branch.Interfaces;
using BeautySalonBooking.Contracts.Branch.Branch.Dtos;
using BeautySalonBooking.Contracts.Branch.Branch.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Domain.BranchAggregate.Repositories;

namespace BeautySalonBooking.Application.Branch.Branch.Services;

public sealed class BranchService : IBranchService
{
    private readonly IBranchRepository _branchRepository;

    public BranchService(
        IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<ApiResponse_New<GetBranchesResponse>>
        GetAllAsync(
            CancellationToken cancellationToken)
    {
        var branches =
            await _branchRepository.GetAllAsync(
                cancellationToken);

        var response = new GetBranchesResponse
        {
            Branches = branches
                .Select(branch => new BranchDto
                {
                    Id = branch.Id,
                    Title = branch.Title,
                    Description = branch.Description,
                    Glyph = branch.Glyph,
                    Media = branch.Media
                        .Where(media => media.DisplayOrder == 1)
                        .Select(media => new BranchMediaDto
                        {
                            FileName = media.FileName,
                            ContentType = media.ContentType,
                            FileSize = media.FileSize,
                            Content = media.Content,
                            DisplayOrder = media.DisplayOrder
                        })
                        .ToList()
                })
                .ToList()
        };

        return new ApiResponse_New<GetBranchesResponse>
        {
            IsSuccess = true,
            Code = 200,
            Payload = response
        };
    }

}
