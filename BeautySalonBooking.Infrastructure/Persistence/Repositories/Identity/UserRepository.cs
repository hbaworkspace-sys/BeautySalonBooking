using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Base.Enums;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;
using BeautySalonBooking.Domain.Identity.UserAggregate.Repositories;
using BeautySalonBooking.Domain.Repositories;
using Microsoft.EntityFrameworkCore;


namespace BeautySalonBooking.Infrastructure.Persistence.Repositories;


public class UserRepository
    : Repository<User, long>, IUserRepository
{


    private readonly BeautyDbContext _context;



    public UserRepository(
        BeautyDbContext context)
        : base(context)
    {
        _context = context;
    }


    // متدهای کمکی و اختصاصی برای User
    public async Task<User?> GetUserWithFullDetailsAsync(long userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(u => u.Person)
            .Include(u => u.PhoneNumbers)
            .Include(u => u.OrganizationOwners)
                .ThenInclude(oo => oo.Organization)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public async Task<User?> GetUserWithPhoneNumbersAsync(long userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(u => u.PhoneNumbers)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public async Task<User?> GetUserWithOrganizationsAsync(long userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(u => u.OrganizationOwners)
                .ThenInclude(oo => oo.Organization)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetUserWithRolesAsync(long userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UserNameExistsAsync(string userName, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    public async Task<bool> ExistsByMobileNumberAsync(string mobileNumber, CancellationToken cancellationToken)
    {
        return await _context.Users
         .AnyAsync(
         x => x.PhoneNumbers.Any(p =>
         p.Number == mobileNumber &&
         p.Type == PhoneNumberType.Mobile),
         cancellationToken);
    }

    public async Task<List<User>> GetListUserAsync(string mobileNumber, CancellationToken cancellationToken)
    {
        var users = await _context.Users
              .Where(u => u.PhoneNumbers.Any(p =>
                p.Number == mobileNumber &&
                p.Type == PhoneNumberType.Mobile))
            .ToListAsync(cancellationToken);

        var personId = users.First().PersonId;

        var allUsers = await _context.Users
    .Include(u => u.Person)
    .Include(u => u.UserRole)
    .ThenInclude(u => u.Role)
    .Where(u => u.PersonId == personId)
    .ToListAsync(cancellationToken);

        return allUsers;

    }

    public async Task<User?> GetByPersonIdAsync(long personId, CancellationToken cancellationToken = default)
    {

        return await _context.Users
            .FirstOrDefaultAsync(
                x => x.PersonId == personId,
                cancellationToken);

    }




    public async Task<User?> GetByUserName(
        string userName)
    {

        return await _context.Users
            .FirstOrDefaultAsync(
                x => x.UserName == userName);

    }




    public async Task<User> UpdateAsync(
        User user,
        CancellationToken cancellationToken = default)
    {

        Update(user);

        return user;

    }

}