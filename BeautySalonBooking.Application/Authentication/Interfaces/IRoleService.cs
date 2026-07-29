using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Entities;

namespace BeautySalonBooking.Application.Authentication.Interfaces;

public interface IRoleService
{
    Task<Role> GetDefaultCustomerRoleAsync(CancellationToken cancellationToken);


    // متدهای جدید
    Task<RoleListResponse> GetRolesAsync(RoleSearchRequest request, CancellationToken cancellationToken);
    Task<RoleDto> GetRoleByIdAsync(int id, CancellationToken cancellationToken);
    Task<RoleDto> CreateRoleAsync(CreateRoleRequest request, CancellationToken cancellationToken);
    Task<RoleDto> UpdateRoleAsync(UpdateRoleRequest request, CancellationToken cancellationToken);
    Task DeleteRoleAsync(int id, CancellationToken cancellationToken);
}