using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Entities;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Entities;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Enums;

namespace BeautySalonBooking.Domain.Identity.RoleAggregate.Repositories;

public interface IRoleRepository : IRepository<Role, int>
{
    Task<Role?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Role?> GetByCodeAsync(RoleCode role, CancellationToken cancellationToken);
    //Task AddAsync(Role role, CancellationToken cancellationToken);
}