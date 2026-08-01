// ITokenService.cs
using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Contracts.Authentication.Responses;
using BeautySalonBooking.WebApp.Interfaces.Common;
using BeautySalonBooking.WebApp.Settings;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

public class TokenService : ITokenService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly ILogger<TokenService> _logger;
    private string _cachedToken;
    private string _cachedRefreshToken;

    public TokenService(IJSRuntime jsRuntime, ILogger<TokenService> logger)
    {
        _jsRuntime = jsRuntime;
        _logger = logger;
    }

    // ========== متد کمکی برای اجرای امن JS ==========
    private async Task<T> SafeInvokeAsync<T>(Func<Task<T>> action, T defaultValue = default)
    {
        try
        {
            return await action();
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("JavaScript interop calls cannot be issued"))
        {
            _logger.LogWarning("JS interop not available yet, returning default value");
            return defaultValue;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in JS interop");
            return defaultValue;
        }
    }

    // ========== متدهای اصلی ==========

    public async Task<string> GetAccessTokenAsync()
    {
        try
        {
            if (!string.IsNullOrEmpty(_cachedToken))
                return _cachedToken;

            var token = await SafeInvokeAsync(
                () => _jsRuntime.InvokeAsync<string>("localStorage.getItem", "accessToken").AsTask(),
                null
            );

            return token;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting access token");
            return null;
        }
    }

    public async Task<string> GetRefreshTokenAsync()
    {
        try
        {
            if (!string.IsNullOrEmpty(_cachedRefreshToken))
                return _cachedRefreshToken;

            var token = await SafeInvokeAsync(
                () => _jsRuntime.InvokeAsync<string>("localStorage.getItem", "refreshToken").AsTask(),
                null
            );

            return token;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting refresh token");
            return null;
        }
    }

    public async Task SetTokensAsync(TokenResponse token, UserDto user)
    {
        try
        {
            await SafeInvokeAsync(async () =>
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "accessToken", token.AccessToken);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "refreshToken", token.RefreshToken);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "tokenExpiry", token.ExpiresAt.ToString("O"));

                if (user != null)
                {
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "user",
                        System.Text.Json.JsonSerializer.Serialize(user));
                }

                _cachedToken = token.AccessToken;
                _cachedRefreshToken = token.RefreshToken;

                return Task.CompletedTask;
            });

            _logger.LogInformation("Tokens saved successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving tokens");
        }
    }

    public async Task<bool> UpdateTokensAsync(TokenResponse newTokens)
    {
        try
        {
            var currentUser = await GetCurrentUserAsync();
            await SetTokensAsync(newTokens, currentUser);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating tokens");
            return false;
        }
    }

    public async Task ClearTokensAsync()
    {
        try
        {
            await SafeInvokeAsync(async () =>
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "accessToken");
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "refreshToken");
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "tokenExpiry");
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "user");
                _cachedToken = null;
                _cachedRefreshToken = null;

                return Task.CompletedTask;
            });

            _logger.LogInformation("Tokens cleared successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error clearing tokens");
        }
    }

    public async Task<bool> IsTokenValidAsync()
    {
        try
        {
            var expiryStr = await SafeInvokeAsync(
                () => _jsRuntime.InvokeAsync<string>("localStorage.getItem", "tokenExpiry").AsTask(),
                null
            );

            if (string.IsNullOrEmpty(expiryStr))
                return false;

            var expiry = DateTime.Parse(expiryStr);
            return expiry > DateTime.UtcNow.AddMinutes(5);
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> IsSessionValidAsync()
    {
        try
        {
            // 1. بررسی وجود توکن
            var token = await GetAccessTokenAsync();
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogDebug("No access token found");
                return false;
            }

            // 2. بررسی انقضای توکن
            var isValid = await IsTokenValidAsync();
            if (!isValid)
            {
                _logger.LogDebug("Token is expired");
                return false;
            }

            // 3. بررسی وجود کاربر
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                _logger.LogDebug("No user found");
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

    public async Task<UserDto> GetCurrentUserAsync()
    {
        try
        {
            var userJson = await SafeInvokeAsync(
                () => _jsRuntime.InvokeAsync<string>("localStorage.getItem", "user").AsTask(),
                null
            );

            if (string.IsNullOrEmpty(userJson))
                return null;

            return System.Text.Json.JsonSerializer.Deserialize<UserDto>(userJson);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting current user");
            return null;
        }
    }

    // ========== متدهای جدید برای چند کاربر ==========

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        try
        {
            var json = await SafeInvokeAsync(
                () => _jsRuntime.InvokeAsync<string>("localStorage.getItem", "all_users").AsTask(),
                null
            );

            if (string.IsNullOrEmpty(json)) return new List<UserDto>();
            return System.Text.Json.JsonSerializer.Deserialize<List<UserDto>>(json) ?? new List<UserDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all users");
            return new List<UserDto>();
        }
    }

    public async Task SetAllUsersAsync(List<UserDto> users)
    {
        try
        {
            await SafeInvokeAsync(async () =>
            {
                var json = System.Text.Json.JsonSerializer.Serialize(users);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "all_users", json);
                return Task.CompletedTask;
            });

            _logger.LogInformation($"✅ All users saved: {users.Count} users");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving all users");
        }
    }

    public async Task SetMultipleTokensAsync(List<AuthResult> authResults)
    {
        try
        {
            await SafeInvokeAsync(async () =>
            {
                var tokensDict = new Dictionary<long, TokenResponse>();
                foreach (var authResult in authResults)
                {
                    if (authResult.User != null && authResult.Tokens != null)
                    {
                        tokensDict[authResult.User.Id] = authResult.Tokens;
                    }
                }
                var json = System.Text.Json.JsonSerializer.Serialize(tokensDict);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "user_tokens", json);
                return Task.CompletedTask;
            });

            _logger.LogInformation($"✅ Multiple tokens saved for {authResults.Count} users");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving multiple tokens");
        }
    }

    public async Task<TokenResponse> GetUserTokensAsync(long userId)
    {
        try
        {
            var json = await SafeInvokeAsync(
                () => _jsRuntime.InvokeAsync<string>("localStorage.getItem", "user_tokens").AsTask(),
                null
            );

            if (string.IsNullOrEmpty(json)) return null;

            var dict = System.Text.Json.JsonSerializer.Deserialize<Dictionary<long, TokenResponse>>(json);
            return dict?.GetValueOrDefault(userId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting user tokens for userId: {UserId}", userId);
            return null;
        }
    }

    public async Task<bool> SwitchUserAsync(long userId)
    {
        try
        {
            var token = await GetUserTokensAsync(userId);
            if (token == null) return false;

            var allUsers = await GetAllUsersAsync();
            var user = allUsers.FirstOrDefault(u => u.Id == userId);
            if (user == null) return false;

            await SetTokensAsync(token, user);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error switching user");
            return false;
        }
    }
}