using BeautySalonBooking.Domain.PersonAggregate.Entities;

namespace BeautySalonBooking.Application.Security.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(Person person);
    string GenerateRefreshToken(Person person);
}