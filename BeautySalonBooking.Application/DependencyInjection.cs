using BeautySalonBooking.Application.Appointment.Interfaces;
using BeautySalonBooking.Application.Appointment.Services;
using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Application.Authentication.Services;
using BeautySalonBooking.Application.Booking.Interfaces;
using BeautySalonBooking.Application.Booking.Services;
using BeautySalonBooking.Application.Branch.BranchMemberService.Interfaces;
using BeautySalonBooking.Application.Branch.BranchMemberService.Services;
using BeautySalonBooking.Application.Branch.BranchService.Interfaces;
using BeautySalonBooking.Application.Branch.BranchService.Services;
using BeautySalonBooking.Application.Category.Interfaces;
using BeautySalonBooking.Application.Category.Services;
using BeautySalonBooking.Application.Common.Interfaces;
using BeautySalonBooking.Application.Common.Services;
using BeautySalonBooking.Application.Service.Interfaces;
using BeautySalonBooking.Application.Service.Services;
using BeautySalonBooking.Domain.SchedulingAggregate.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonBooking.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddMemoryCache();


        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
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

        //Category
        services.AddScoped<ICategoryService, CategoryService>();

        //Service
        services.AddScoped<IServiceService, ServiceService>();

        //Branch
        services.AddScoped<IBranchServiceService, BranchServiceService>();
        services.AddScoped<IBranchMemberServiceService, BranchMemberServiceService>();

        //Booking
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<AvailabilityCalculator>();
        services.AddScoped<IAppointmentService, AppointmentService>();

        return services;
    }
}