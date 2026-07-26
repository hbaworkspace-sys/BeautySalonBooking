using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Application.Authentication.Services;
using BeautySalonBooking.Application.Common.Interfaces;
using BeautySalonBooking.Application.Common.Services;
using BeautySalonBooking.Application.Permission.Interfaces;
using BeautySalonBooking.Domain.Base.UnitOfWork;
using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Repositories;
using BeautySalonBooking.Infrastructure;
using BeautySalonBooking.Infrastructure.Persistence.Repositories.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonBooking.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddMemoryCache();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IOtpService, OtpService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserSessionService, UserSessionService>();
        //services.AddScoped<ICrudService, CrudService>();
        services.AddScoped<IEmailService, EmailService>();
        //services.AddScoped<IRequestLogService, RequestLogService>();
        services.AddScoped<ISmsService, SmsService>();
        //services.AddScoped<IMenuService, IMenuService>();
        return services;
    }
}