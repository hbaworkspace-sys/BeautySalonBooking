namespace BeautySalonBooking.Maui.Features.Auth.Models;
public sealed class AuthSession
{
    public string AccessToken { get; init; } = string.Empty;

    public string RefreshToken { get; init; } = string.Empty;

    public DateTime ExpiresAt { get; init; }
}