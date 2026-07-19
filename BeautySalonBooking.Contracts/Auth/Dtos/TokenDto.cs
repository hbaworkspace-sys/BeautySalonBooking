namespace BeautySalonBooking.Contracts.Auth
{
    public class TokenDto
    {
        public string AccessToken { get; init; } = default!;

        public string RefreshToken { get; init; } = default!;

        public DateTime ExpiresAt { get; init; }
    }
}