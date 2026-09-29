using BeautySalonBooking.Maui.Features.Auth.Models;

namespace BeautySalonBooking.Contracts.Authentication.Services;

public sealed class AuthSessionService : IAuthSessionService
{
    private const string AccessTokenKey = "auth_access_token";
    private const string RefreshTokenKey = "auth_refresh_token";
    private const string ExpiresAtKey = "auth_expires_at";

    public async Task SaveAsync(AuthSession session)
    {
        await SecureStorage.Default.SetAsync(
            AccessTokenKey,
            session.AccessToken);

        await SecureStorage.Default.SetAsync(
            RefreshTokenKey,
            session.RefreshToken);

        await SecureStorage.Default.SetAsync(
            ExpiresAtKey,
            session.ExpiresAt.ToString("O"));
    }

    public async Task<AuthSession?> GetAsync()
    {
        var accessToken =
            await SecureStorage.Default.GetAsync(AccessTokenKey);

        var refreshToken =
            await SecureStorage.Default.GetAsync(RefreshTokenKey);

        var expiresAtValue =
            await SecureStorage.Default.GetAsync(ExpiresAtKey);

        if (string.IsNullOrWhiteSpace(accessToken) ||
            string.IsNullOrWhiteSpace(refreshToken) ||
            string.IsNullOrWhiteSpace(expiresAtValue))
        {
            return null;
        }

        if (!DateTime.TryParse(
                expiresAtValue,
                null,
                System.Globalization.DateTimeStyles.RoundtripKind,
                out var expiresAt))
        {
            return null;
        }

        return new AuthSession
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = expiresAt
        };
    }

    public Task ClearAsync()
    {
        SecureStorage.Default.Remove(AccessTokenKey);
        SecureStorage.Default.Remove(RefreshTokenKey);
        SecureStorage.Default.Remove(ExpiresAtKey);

        return Task.CompletedTask;
    }
}
