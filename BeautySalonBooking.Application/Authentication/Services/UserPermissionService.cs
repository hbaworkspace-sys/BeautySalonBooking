using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Domain.Base.UnitOfWork;

namespace BeautySalonBooking.Application.Authentication.Services;

public sealed class UserPermissionService : IUserPermissionService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserPermissionService(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<PermissionDto>> GetPermissionsAsync(
        long userId,
        CancellationToken cancellationToken = default)
    {
        var permissions =
            await _unitOfWork
                .PermissionRepository
                .GetPermissionsByUserIdAsync(
                    userId,
                    cancellationToken);

        return permissions.Where(x => x.Type == Domain.Identity.PermissionAggregate.Enums.PermissionType.Permission)
            .Select(x => new PermissionDto
            {
                Code = x.Code,
                Title = x.Title
            })
            .ToList();
    }

    public async Task<List<PermissionDto>> GetMenusAsync(
        long userId,
        CancellationToken cancellationToken)
    {
        var permissions =
            await _unitOfWork
                .PermissionRepository
                .GetPermissionsByUserIdAsync(
                    userId,
                    cancellationToken);

        // فعلاً هر Permission که Menu باشد
        return permissions
            .Where(x => x.Type == Domain.Identity.PermissionAggregate.Enums.PermissionType.Permission)
            .Select(x => new PermissionDto
            {
                Code = x.Code,
                Title = x.Title
            })
            .ToList();
    }

    public async Task<List<string>> GetRoleNamesAsync(
        long userId,
        CancellationToken cancellationToken)
    {
        var user =
            await _unitOfWork
                .UserRepository
                .GetByIdAsync(
                    userId,
                    cancellationToken);

        if (user == null)
            return new List<string>();

        var role =
            await _unitOfWork
                .RoleRepository
                .GetByIdAsync(
                    user.UserRole.RoleId,
                    cancellationToken);

        if (role == null)
            return new List<string>();

        return new List<string>
        {
            role.Title
        };
    }
}