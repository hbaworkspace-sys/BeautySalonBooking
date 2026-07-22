namespace BeautySalonBooking.Contracts.Auth.Responses;

public class AuthResult
{
    public string AccessToken { get; init; } = string.Empty;
    public string RefreshToken { get; init; } = string.Empty;
    public long PersonId { get; init; }
}