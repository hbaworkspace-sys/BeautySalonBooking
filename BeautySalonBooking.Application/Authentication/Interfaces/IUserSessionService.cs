using BeautySalonBooking.Contracts.Authentication.Dtos;

namespace BeautySalonBooking.Application.Authentication.Interfaces
{
    public interface IUserSessionService
    {
        Task<bool> IsUserSessionActiveAsync(long userId);
        Task ActivateUserSessionAsync(long userId);
        Task DeactivateUserSessionAsync(long userId);
        Task ExtendUserSessionAsync(long userId);
        Task<SessionInfo> GetSessionInfoAsync(long userId);
        //Task<long> GetActiveSessionsCountAsync();
    }
}
