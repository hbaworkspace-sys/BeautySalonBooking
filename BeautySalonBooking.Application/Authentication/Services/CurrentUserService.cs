using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Domain.Base.UnitOfWork;
using BeautySalonBooking.Domain.PersonAggregate.Entities;
using Microsoft.AspNetCore.Http;

namespace BeautySalonBooking.Application.Authentication.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserPermissionService _userPermissionService;

    public CurrentUserService(
        IHttpContextAccessor httpContextAccessor,
        IUnitOfWork unitOfWork,
        IUserPermissionService userPermissionService)
    {
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
        _userPermissionService = userPermissionService;
    }

    public async Task<UserDto?> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        var userId = GetCurrentUserId();

        if (!userId.HasValue)
            return null;

        var user = await _unitOfWork
            .UserRepository
            .GetUserWithFullDetailsAsync(
                userId.Value,
                cancellationToken);

        if (user == null)
            return null;

        return await BuildUserDtoAsync(user, cancellationToken);
    }

    public long? GetCurrentUserId()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user?.Identity?.IsAuthenticated != true)
            return null;

        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)
                          ?? user.FindFirst(JwtRegisteredClaimNames.Sub);

        if (userIdClaim == null || !long.TryParse(userIdClaim.Value, out var userId))
            return null;

        return userId;
    }

    public async Task<bool> IsCurrentUserInRoleAsync(string role, CancellationToken cancellationToken = default)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        return user?.IsInRole(role) ?? false;
    }

    public async Task<bool> HasPermissionAsync(string permissionCode, CancellationToken cancellationToken = default)
    {
        var userId = GetCurrentUserId();

        if (!userId.HasValue)
            return false;

        var permissions = await _userPermissionService
            .GetPermissionsAsync(userId.Value, cancellationToken);

        return permissions.Any(p => p.Code == permissionCode);
    }

    private async Task<UserDto> BuildUserDtoAsync(
        Domain.Identity.UserAggregate.Entities.User user,
        CancellationToken cancellationToken)
    {
        var permissions = await _userPermissionService
            .GetPermissionsAsync(user.Id, cancellationToken);

        var menus = await _userPermissionService
            .GetMenusAsync(user.Id, cancellationToken);

        var roles = await _userPermissionService
            .GetRoleNamesAsync(user.Id, cancellationToken);

        return new UserDto
        {
            Id = user.Id,
            FirstName = user.Person.FirstName,
            LastName = user.Person.LastName,
            Email = string.Empty,
            CreatedAt = user.CreatedAt,
            IsActive = user.Person.IsActive,
            NationalCode = user.Person.NationalCode ?? string.Empty,
            PhoneNumber = user.PhoneNumbers
                .FirstOrDefault(x => x.IsDefault)
                ?.Number
                ?? user.PhoneNumbers
                    .FirstOrDefault()
                    ?.Number
                ?? string.Empty,
            AuthenticationType = (int)user.AuthenticationMode,
            UserName = user.UserName ?? string.Empty,
            Roles = roles,
            Permissions = permissions,
            Menus = menus
        };
    }
}