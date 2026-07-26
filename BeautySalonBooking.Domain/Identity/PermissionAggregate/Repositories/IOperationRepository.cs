using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.Identity.PermissionAggregate.Entities;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Entities;

namespace BeautySalonBooking.Domain.Identity.PermissionAggregate.Repositories;


public interface IOperationRepository : IRepository<Permission, int>
{
    Task<List<Permission>> GetOperationsByRoleIdsAsync(List<int> roleIds);
    Task<List<Permission>> GetAllOperationsAsync();
    Task<List<RolePermission>> GetRoleOperationsByRoleIdsAsync(List<int> roleIds);
    Task<List<int>> GetUserRoleIdsAsync(int userId);
    Task<List<Permission>> GetChildByParents(int parentId);
    // Task<ApiResponse<bool>> UpdateRoleOperation(UpdateOperationRequest model);
}
