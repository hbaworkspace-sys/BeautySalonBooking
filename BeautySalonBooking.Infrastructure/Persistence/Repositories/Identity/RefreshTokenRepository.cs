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

    public async Task<RefreshToken?> GetByHashAsync(string hash)
    {
        return await _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt =>
                rt.TokenHash == hash &&
                !rt.IsRevoked &&
                rt.ExpiresAt > DateTime.UtcNow);
    }

    public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
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