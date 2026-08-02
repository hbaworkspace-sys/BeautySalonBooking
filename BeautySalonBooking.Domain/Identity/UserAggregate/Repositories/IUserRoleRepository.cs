using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Base.Enums;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;


namespace BeautySalonBooking.Domain.Repositories;


public interface IUserRoleRepository
    : IRepository<UserRole, long>
{

    Task<bool> ExistsAsync(long userId, int roleId, CancellationToken cancellationToken = default);
    Task<IEnumerable<UserRole>> GetByRoleIdAsync(int roleId, CancellationToken cancellationToken = default);
    Task<IEnumerable<UserRole>> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default);

    Task<UserRole?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default);




}