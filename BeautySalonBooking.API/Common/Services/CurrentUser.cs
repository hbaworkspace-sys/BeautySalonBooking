using BeautySalonBooking.Application.Common.Interfaces;
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
}