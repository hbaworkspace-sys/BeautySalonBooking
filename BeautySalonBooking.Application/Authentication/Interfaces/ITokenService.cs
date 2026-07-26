using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Contracts.Authentication.Responses;
using System.Security.Claims;

namespace BeautySalonBooking.Application.Authentication.Interfaces;
public interface ITokenService
{
    Task<TokenResponse> GenerateTokensAsync(UserDto user, CancellationToken cancellationToken);
    Task<TokenResponse> RefreshTokensAsync(string accessToken, string refreshToken, CancellationToken cancellationToken);
    ClaimsPrincipal? ValidateToken(string token, CancellationToken cancellationToken);
    bool IsTokenExpired(string token, CancellationToken cancellationToken);
    Task<bool> RevokeTokenAsync(string refreshToken, CancellationToken cancellationToken);
}
