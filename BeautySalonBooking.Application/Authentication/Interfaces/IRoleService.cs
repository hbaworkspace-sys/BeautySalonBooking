using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Contracts.Permission.Dtos;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Entities;

namespace BeautySalonBooking.Application.Authentication.Interfaces;

public interface IRoleService
{
    Task<Role> GetDefaultCustomerRoleAsync(CancellationToken cancellationToken);
    Task AssignPermissionsAsync(AssignRolePermissionsRequest request, CancellationToken cancellationToken);
    Task<List<RolePermissionDto>> GetRolePermissionsAsync(int roleId, CancellationToken cancellationToken);
    // متدهای جدید
    Task<RoleListResponse> GetRolesAsync(RoleSearchRequest request, CancellationToken cancellationToken);
    Task<RoleDto> GetRoleByIdAsync(int id, CancellationToken cancellationToken);
    Task<RoleDto> CreateRoleAsync(CreateRoleRequest request, CancellationToken cancellationToken);
    Task<RoleDto> UpdateRoleAsync(UpdateRoleRequest request, CancellationToken cancellationToken);
    Task DeleteRoleAsync(int id, CancellationToken cancellationToken);
}