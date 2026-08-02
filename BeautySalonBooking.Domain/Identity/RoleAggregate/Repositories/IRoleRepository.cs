using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Base.Enums;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Entities;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Enums;


namespace BeautySalonBooking.Domain.Repositories;


public interface IRoleRepository
    : IRepository<Role, int>
{


    Task<Role?> GetByCodeAsync(
        RoleCode code,
        CancellationToken cancellationToken = default);



    Task<Role?> GetRoleWithPermissionsAsync(
        int roleId,
        CancellationToken cancellationToken = default);



    Task<IEnumerable<Role>> GetRolesByUserIdAsync(
        long userId,
        CancellationToken cancellationToken = default);

}