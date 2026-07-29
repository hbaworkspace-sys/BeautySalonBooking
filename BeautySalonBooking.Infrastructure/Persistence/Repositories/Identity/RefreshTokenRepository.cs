using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Entities;
using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories.Identity;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly BeautyDbContext _context;

    public RefreshTokenRepository(BeautyDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetValidTokenAsync(string token)
    {
        return await _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt =>
                rt.Token == token &&
                !rt.IsRevoked &&
                rt.ExpiresAt > DateTime.UtcNow);
    }

    public async Task AddAsync(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
    }

    public void Update(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Update(refreshToken);
    }
    public async Task<IEnumerable<RefreshToken>> GetAllByUserIdAsync(long userId)
    {
        return await _context.RefreshTokens
            .Where(rt => rt.UserId == userId && !rt.IsRevoked)
            .ToListAsync();
    }

}