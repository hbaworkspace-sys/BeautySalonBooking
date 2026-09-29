using BeautySalonBooking.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BeautySalonBooking.Application.Common.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public long UserId
    {
        get
        {
            var userId =
                _httpContextAccessor
                    .HttpContext?
                    .User?
                    .FindFirst(ClaimTypes.NameIdentifier)
                    ?.Value;

            if (int.TryParse(userId, out var id))
                return id;

            return 0;
        }
    }
}
