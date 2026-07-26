using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Entities;

namespace BeautySalonBooking.Domain.Identity.RoleAggregate.Repositories;

public interface IRoleOperationRepository : IRepository<RolePermission, int>
{
    //Task<RolePermission> UpdateRoleOperation(RolePermission model);
}
