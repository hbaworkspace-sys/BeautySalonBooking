using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Base.Enums;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;
using System.Threading.Tasks;

namespace BeautySalonBooking.Domain.Repositories;

public interface IUserRepository
    : IRepository<User, long>
{


    Task<User?> GetUserWithFullDetailsAsync(long userId, CancellationToken cancellationToken = default);
    Task<User?> GetUserWithPhoneNumbersAsync(long userId, CancellationToken cancellationToken = default);
    Task<User?> GetUserWithOrganizationsAsync(long userId, CancellationToken cancellationToken = default);
    Task<List<User>> GetListUserAsync(string mobileNumber, CancellationToken cancellationToken);
    Task<bool> ExistsByMobileNumberAsync(string mobileNumber, CancellationToken cancellationToken);
    Task<User?> GetByPersonIdAsync(long personId, CancellationToken cancellationToken = default);



    Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);



    Task<bool> UserNameExistsAsync(string userName, CancellationToken cancellationToken = default);



    Task<User?> GetUserWithRolesAsync(long userId, CancellationToken cancellationToken = default);

}