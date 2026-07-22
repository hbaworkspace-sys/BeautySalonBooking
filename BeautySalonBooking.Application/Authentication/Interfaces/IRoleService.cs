using BeautySalonBooking.Domain.Identity.RoleAggregate.Entities;

namespace BeautySalonBooking.Application.Authentication.Interfaces;

public interface IRoleService
{
    Task<Role> GetDefaultCustomerRoleAsync(CancellationToken cancellationToken);
}