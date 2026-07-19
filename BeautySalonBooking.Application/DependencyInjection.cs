using BeautySalonBooking.Application.Authentication;
using BeautySalonBooking.Application.Regions;
using BeautySalonBooking.Application.SalonVerifications;
using BeautySalonBooking.Application.Security.Interfaces;
using BeautySalonBooking.Application.Security.Services;
using BeautySalonBooking.Application.UserRoles;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonBooking.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserRoleService, UserRoleService>();
        services.AddScoped<IRegionService, RegionService>();
        services.AddScoped<ISalonVerificationService, SalonVerificationService>();


        return services;
    }
}