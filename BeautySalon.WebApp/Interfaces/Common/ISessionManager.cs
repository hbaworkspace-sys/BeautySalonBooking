using BeautySalonBooking.Contracts.Authentication.Dtos;

namespace BeautySalonBooking.WebApp.Interfaces.Common
{
    public interface ISessionManager
    {
        Task<bool> ValidateCurrentSessionAsync();
        Task<bool> CheckSessionAndRedirectAsync();
        Task LogoutAsync();
        Task<UserDto> GetCurrentUserAsync();
    }
}
