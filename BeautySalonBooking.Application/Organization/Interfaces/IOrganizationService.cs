using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Contracts.Organization.Responses;

namespace BeautySalonBooking.Application.Organization.Interfaces;

public interface IOrganizationService
{
    Task<ApiResponse_New<GetOrganizationsResponse>> GetAlleAsync(
        CancellationToken cancellationToken);
}