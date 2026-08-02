using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.Base.UnitOfWork;
using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Repositories;
using BeautySalonBooking.Domain.Identity.PermissionAggregate.Repositories;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Repositories;
using BeautySalonBooking.Domain.Identity.UserAggregate.Repositories;
using BeautySalonBooking.Domain.PersonAggregate.Repositories;
using BeautySalonBooking.Domain.Repositories;
using BeautySalonBooking.Infrastructure.Persistence.Repositories;
using BeautySalonBooking.Infrastructure.Persistence.Repositories.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonBooking.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IOtpRepository, OtpRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        return services;
    }
}