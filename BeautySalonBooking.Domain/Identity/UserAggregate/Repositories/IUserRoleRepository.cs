using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;

namespace BeautySalonBooking.Domain.Identity.UserAggregate.Repositories;

public interface IUserRoleRepository
{
    Task<UserRole?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task AddAsync(UserRole userRole, CancellationToken cancellationToken);
    Task UpdateAsync(UserRole userRole, CancellationToken cancellationToken);
}
