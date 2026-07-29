using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Contracts.Authentication.Responses;

namespace BeautySalonBooking.WebApp.Interfaces.Common
{
    public interface ITokenService
    {
        // ========== متدهای اصلی ==========
        Task<string> GetAccessTokenAsync();
        Task<string> GetRefreshTokenAsync();
        Task SetTokensAsync(TokenResponse token, UserDto user);
        Task<bool> UpdateTokensAsync(TokenResponse newTokens);
        Task ClearTokensAsync();
        Task<bool> IsTokenValidAsync();
        Task<bool> IsSessionValidAsync();
        Task<UserDto> GetCurrentUserAsync();

        // ========== متدهای چند کاربر ==========
        Task<List<UserDto>> GetAllUsersAsync();
        Task SetAllUsersAsync(List<UserDto> users);
        Task SetMultipleTokensAsync(List<AuthResult> authResults);
        Task<TokenResponse> GetUserTokensAsync(long userId);
        Task<bool> SwitchUserAsync(long userId);
    }
}
