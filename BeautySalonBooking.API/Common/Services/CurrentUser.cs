using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Contracts.Authentication.Dtos;
using System.Security.Claims;

namespace BeautySalonBooking.API.Common.Services;

public sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public long UserId
    {
        get
        {
            var claim =
                _httpContextAccessor.HttpContext?
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!long.TryParse(claim, out var userId))
                throw new UnauthorizedAccessException();

            return userId;
        }
    }

    public Task<UserDto?> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public long? GetCurrentUserId()
    {
        throw new NotImplementedException();
    }

    public Task<bool> HasPermissionAsync(string permissionCode, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsCurrentUserInRoleAsync(string role, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}