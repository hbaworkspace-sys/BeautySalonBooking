using BeautySalonBooking.Contracts.Authentication.Dtos;

namespace BeautySalonBooking.Application.Authentication.Interfaces;

public interface IJwtTokenService
{
    string GenerateAccessToken(UserDto user);
}