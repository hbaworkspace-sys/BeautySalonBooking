// WebApp/Interfaces/Roles/IRoleService.cs
using BeautySalonBooking.Contracts.Authentication.Dtos;

namespace BeautySalonBooking.WebApp.Interfaces.Roles
{
    public interface IRoleService
    {
        Task<RoleListResponse> GetRolesAsync(string? searchTerm = null, int pageNumber = 1, int pageSize = 10);
        Task<RoleDto> GetRoleByIdAsync(int id);
        Task<RoleDto> CreateRoleAsync(CreateRoleRequest request);
        Task<RoleDto> UpdateRoleAsync(UpdateRoleRequest request);
        Task<bool> DeleteRoleAsync(int id);
    }
}