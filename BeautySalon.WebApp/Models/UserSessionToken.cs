using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Contracts.Authentication.Responses;

namespace BeautySalonBooking.WebApp.Models
{
    public class UserSessionToken
    {
        public long UserId { get; set; }

        public UserDto User { get; set; }

        public TokenResponse Tokens { get; set; }
    }
}
