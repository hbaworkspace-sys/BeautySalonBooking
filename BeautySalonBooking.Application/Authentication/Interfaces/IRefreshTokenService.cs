using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Contracts.Authentication.Responses;
using System.Security.Claims;

namespace BeautySalonBooking.Application.Authentication.Interfaces;

public interface IRefreshTokenService
{
    Task<TokenResponse> CreateAsync(
        UserDto user,
        CancellationToken cancellationToken);

    Task<TokenResponse> RefreshAsync(
        string accessToken,
        string refreshToken,
        CancellationToken cancellationToken);

    Task<bool> RevokeAsync(
        string refreshToken,
        CancellationToken cancellationToken);

    Task<bool> RevokeAllAsync(
        long userId,
        CancellationToken cancellationToken);
    ClaimsPrincipal? ValidateAccessToken(string accessToken);

    ClaimsPrincipal? GetPrincipalFromExpiredToken(string accessToken);

    bool IsTokenExpired(string accessToken);
}