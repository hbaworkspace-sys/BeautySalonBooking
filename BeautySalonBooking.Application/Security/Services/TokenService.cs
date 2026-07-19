using BeautySalonBooking.Application.Security.Interfaces;
using BeautySalonBooking.Domain.UserAggregate.Entities;

namespace BeautySalonBooking.Application.Security.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateAccessToken(User user)
        {
            return "jwt_access_token";
        }

        public string GenerateRefreshToken(User user)
        {
            return "jwt_refresh_token";
        }
    }
}
