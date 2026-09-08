using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Application.Authentication.Services;
using BeautySalonBooking.Application.Common.Interfaces;
using BeautySalonBooking.Application.Common.Services;
using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Repositories;
using BeautySalonBooking.Infrastructure.Persistence.Repositories;
using BeautySalonBooking.Infrastructure.Persistence.Repositories.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonBooking.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddMemoryCache();


        // Authentication
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IUserPermissionService, UserPermissionService>();
        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IOtpService, OtpService>();

        services.AddScoped<IRoleService, RoleService>();

        services.AddScoped<IUserSessionService, UserSessionService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();


        // Other Services
        services.AddScoped<IEmailService, EmailService>();

        services.AddScoped<ISmsService, SmsService>();


        return services;
    }
}