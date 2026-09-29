using BeautySalonBooking.Contracts.Organization.Dtos;

namespace BeautySalonBooking.Contracts.Organization.Responses;
public sealed class GetOrganizationsResponse
{
    public IReadOnlyList<OrganizationDto> Organizations { get; init; } = [];
}
