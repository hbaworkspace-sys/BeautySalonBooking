using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Entities;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Enums;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Repositories;

namespace BeautySalonBooking.Application.Authentication.Services;

public sealed class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Role> GetDefaultCustomerRoleAsync(CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByCodeAsync(RoleCode.Customer, cancellationToken);

        if (role is null)
            throw new InvalidOperationException("نقش پیش‌فرض Customer در سیستم یافت نشد.");

        return role;
    }
}