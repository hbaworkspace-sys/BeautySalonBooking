using BeautySalonBooking.Domain.OrganizationAggregate.Entities;

namespace BeautySalonBooking.Domain.OrganizationAggregate.Repositories;

public interface IOrganizationRepository
{
    Task<IReadOnlyList<Organization>> GetAllAsync(
        CancellationToken cancellationToken = default);
}