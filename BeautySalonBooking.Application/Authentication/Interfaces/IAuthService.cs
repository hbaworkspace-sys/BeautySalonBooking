using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Contracts.Authentication.Responses;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;
using System.Threading;

namespace BeautySalonBooking.Application.Authentication.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> AuthenticateAsync(string email, string password, CancellationToken cancellationToken);
        Task<AuthResult> RefreshTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken);
        Task<bool> ValidateTokenAsync(string token, CancellationToken cancellationToken);
        Task<User?> GetCurrentUserAsync();
        Task<bool> HasPermissionAsync(string permission);
        Task<bool> LogoutCurrentUserAsync();
        Task<SessionCheckResult> QuickSessionCheckAsync(int userId);
        Task<User?> GetCurrentUserSafeAsync();
    }

}
