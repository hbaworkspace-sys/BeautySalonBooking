using BeautySalonBooking.Domain.Base.Enums;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;
using BeautySalonBooking.Domain.UserAggregate.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly BeautyDbContext _context;

    public UserRepository(BeautyDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetByPersonIdAsync(long personId, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.UserRole)
            .Where(u => u.PersonId == personId)
            .ToListAsync(cancellationToken);
    }
    public async Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _context.Users
           .Include(u => u.UserRole)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        _context.Users.Update(user);
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
}