using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;

namespace BeautySalonBooking.Domain.Identity.UserAggregate.Repositories;

public interface IUserRepository : IRepository<User, long>
{
    Task<bool> ExistsByMobileNumberAsync(string mobileNumber, CancellationToken cancellationToken);
    Task<List<User>> GetListUserAsync(string mobileNumber, CancellationToken cancellationToken);
    Task<List<User>> GetByPersonIdAsync(long personId, CancellationToken cancellationToken);
    //Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<User?> GetByUserName(string username);
    //Task AddAsync(User user, CancellationToken cancellationToken);
    Task UpdateAsync(User user, CancellationToken cancellationToken);
}