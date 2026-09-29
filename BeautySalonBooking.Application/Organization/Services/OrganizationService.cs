using BeautySalonBooking.Application.Organization.Interfaces;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Contracts.Organization.Dtos;
using BeautySalonBooking.Contracts.Organization.Responses;
using BeautySalonBooking.Domain.OrganizationAggregate.Repositories;

namespace BeautySalonBooking.Application.Organization.Services;

public sealed class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepository _organizationRepository;

    public OrganizationService(
        IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<ApiResponse_New<GetOrganizationsResponse>>
        GetAlleAsync(
            CancellationToken cancellationToken)
    {
        var organizations =
            await _organizationRepository.GetAllAsync(
                cancellationToken);

        var response = new GetOrganizationsResponse
        {
            Organizations = organizations
                .Select(organization => new OrganizationDto
                {
                    Id = organization.Id,
                    Title = organization.Title,
                    Slogan = organization.Slogan,
                    Glyph = organization.Glyph,

                    Media = organization.Media
                        .Where(media => media.DisplayOrder == 1)
                        .Select(media => new OrganizationMediaDto
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

        return new ApiResponse_New<GetOrganizationsResponse>
        {
            IsSuccess = true,
            Code = 200,
            Payload = response
        };
    }
}