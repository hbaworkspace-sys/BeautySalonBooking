using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonBooking.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        //services.AddScoped<IUserRepository, UserRepository>();
        //services.AddScoped<IOtpRepository, OtpRepository>();
        //services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        //services.AddScoped<IRegionRepository, RegionRepository>();
        //services.AddScoped<ISalonVerificationRepository, SalonVerificationRepository>();

        return services;
    }
}