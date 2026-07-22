using BeautySalonBooking.Domain.Identity.RoleAggregate.Entities;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Enums;

namespace BeautySalonBooking.Domain.Identity.RoleAggregate.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Role?> GetByCodeAsync(RoleCode role, CancellationToken cancellationToken);
    Task AddAsync(Role role, CancellationToken cancellationToken);
}