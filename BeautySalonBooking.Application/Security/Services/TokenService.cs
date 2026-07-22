using BeautySalonBooking.Application.Security.Interfaces;
using BeautySalonBooking.Domain.PersonAggregate.Entities;

namespace BeautySalonBooking.Application.Security.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateAccessToken(Person user)
        {
            return "jwt_access_token";
        }

        public string GenerateRefreshToken(Person user)
        {
            return "jwt_refresh_token";
        }
    }
}
