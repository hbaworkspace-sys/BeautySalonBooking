using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Contracts.UserRoles.Requests;
using BeautySalonBooking.Contracts.UserRoles.Responses;

namespace BeautySalonBooking.Application.UserRoles
{
    public interface IUserRoleService
    {
        Task<ApiResponse> AssignRoleAsync(Guid userId, AssignRoleRequest request);
        Task<ApiResponse<UserRolesResponse>> GetUserRolesAsync(Guid userId);
        Task<ApiResponse<ActiveRoleResponse>> GetActiveRoleAsync(Guid userId);
        Task<ApiResponse> SwitchRoleAsync(Guid userId, SwitchRoleRequest request);

    }
}