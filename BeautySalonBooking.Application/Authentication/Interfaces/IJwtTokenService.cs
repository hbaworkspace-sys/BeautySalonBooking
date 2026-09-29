using BeautySalonBooking.Contracts.Authentication.Dtos;
using System.Security.Claims;

namespace BeautySalonBooking.Application.Authentication.Interfaces;

public interface IJwtTokenService
{
    string GenerateAccessToken(UserDto user);
    ClaimsPrincipal? GetPrincipalFromToken(string token, bool validateLifetime = true);
}