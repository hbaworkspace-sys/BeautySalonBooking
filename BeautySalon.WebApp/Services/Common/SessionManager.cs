using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.WebApp.Interfaces.Common;
using Microsoft.AspNetCore.Components;

namespace BeautySalonBooking.WebApp.Services.Common
{
    public class SessionManager : ISessionManager
    {
        private readonly ITokenService _tokenService;
        private readonly NavigationManager _navigationManager;
        private readonly ILogger<SessionManager> _logger;

        public SessionManager(
            ITokenService tokenService,
            NavigationManager navigationManager,
            ILogger<SessionManager> logger)
        {
            _tokenService = tokenService;
            _navigationManager = navigationManager;
            _logger = logger;
        }

        public async Task<bool> ValidateCurrentSessionAsync()
        {
            try
            {
                // 1. بررسی وجود توکن
                var token = await _tokenService.GetAccessTokenAsync();
                if (string.IsNullOrEmpty(token))
                {
                    _logger.LogDebug("No access token found");
                    return false;
                }

                // 2. بررسی انقضای توکن
                var isValid = await _tokenService.IsTokenValidAsync();
                if (!isValid)
                {
                    _logger.LogDebug("Token is expired");
                    await _tokenService.ClearTokensAsync();
                    return false;
                }

                // 3. بررسی وجود کاربر
                var user = await _tokenService.GetCurrentUserAsync();
                if (user == null)
                {
                    _logger.LogDebug("No user found");
                    await _tokenService.ClearTokensAsync();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating session");
                return false;
            }
        }

        public async Task<bool> CheckSessionAndRedirectAsync()
        {
            var isValid = await ValidateCurrentSessionAsync();
            if (!isValid)
            {
                var currentPath = _navigationManager.Uri.Replace(_navigationManager.BaseUri, "");

                var publicPaths = new[] { "", "/", "/login", "/verify-otp" };
                if (!publicPaths.Contains(currentPath, StringComparer.OrdinalIgnoreCase))
                {
                    _navigationManager.NavigateTo("/login", true);
                }
                return false;
            }
            return true;
        }

        public async Task LogoutAsync()
        {
            await _tokenService.ClearTokensAsync();
            _navigationManager.NavigateTo("/login", true);
            _logger.LogInformation("User logged out");
        }

        public async Task<UserDto> GetCurrentUserAsync()
        {
            return await _tokenService.GetCurrentUserAsync();
        }
    }
}
