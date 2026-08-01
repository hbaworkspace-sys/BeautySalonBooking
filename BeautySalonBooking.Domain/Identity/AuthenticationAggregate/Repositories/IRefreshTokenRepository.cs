using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Entities;

namespace BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Repositories;
public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetValidTokenAsync(string token);
    Task AddAsync(RefreshToken refreshToken);
    void Update(RefreshToken refreshToken);

    // ========== متد جدید ==========
    Task<IEnumerable<RefreshToken>> GetAllByUserIdAsync(long userId);

}