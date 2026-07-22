using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;

namespace BeautySalonBooking.Domain.UserAggregate.Repositories;

public interface IUserRepository
{
    Task<bool> ExistsByMobileNumberAsync(string mobileNumber, CancellationToken cancellationToken);
    Task<List<User>> GetByPersonIdAsync(long personId, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task UpdateAsync(User user, CancellationToken cancellationToken);
}