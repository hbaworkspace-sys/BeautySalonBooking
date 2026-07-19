using BeautySalonBooking.Domain.UserAggregate.Entities;

namespace BeautySalonBooking.Application.Security.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken(User user);
    }
}
