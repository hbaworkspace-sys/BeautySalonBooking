using BeautySalonBooking.Maui.Features.Auth.Models;

namespace BeautySalonBooking.Contracts.Authentication.Services;

public interface IAuthSessionService
{
    Task SaveAsync(AuthSession session);

    Task<AuthSession?> GetAsync();

    Task ClearAsync();
}