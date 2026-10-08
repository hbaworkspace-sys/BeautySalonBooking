using BeautySalonBooking.Contracts.Authentication.Dtos;

namespace BeautySalonBooking.Application.Authentication.Interfaces
{
    public interface ICurrentUserService
    {
        long UserId { get; }
        Task<UserDto?> GetCurrentUserAsync(CancellationToken cancellationToken = default);
        long? GetCurrentUserId();
        Task<bool> IsCurrentUserInRoleAsync(string role, CancellationToken cancellationToken = default);
        Task<bool> HasPermissionAsync(string permissionCode, CancellationToken cancellationToken = default);
    }
}
