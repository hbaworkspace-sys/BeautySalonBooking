using BeautySalonBooking.Contracts.UserRoles.DTOs;

namespace BeautySalonBooking.Contracts.Auth.Responses
{
    public class AuthResult
    {
        public string AccessToken { get; init; } = string.Empty;
        public string RefreshToken { get; init; } = string.Empty;
        public Guid UserId { get; init; }
        public UserRoleDto UserRole { get; init; }
    }
}