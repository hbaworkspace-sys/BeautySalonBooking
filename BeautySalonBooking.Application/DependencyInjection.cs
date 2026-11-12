using BeautySalonBooking.Application.Appointment.Interfaces;
using BeautySalonBooking.Application.Appointment.Services;
using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Application.Authentication.Services;
using BeautySalonBooking.Application.Authentication.Validations;
using BeautySalonBooking.Application.Booking.Interfaces;
using BeautySalonBooking.Application.Booking.Services;
using BeautySalonBooking.Application.Branch.Branch.Interfaces;
using BeautySalonBooking.Application.Branch.Branch.Services;
using BeautySalonBooking.Application.Branch.BranchMemberService.Interfaces;
using BeautySalonBooking.Application.Branch.BranchMemberService.Services;
using BeautySalonBooking.Application.Branch.BranchService.Interfaces;
using BeautySalonBooking.Application.Branch.BranchService.Services;
using BeautySalonBooking.Application.Category.Interfaces;
using BeautySalonBooking.Application.Category.Services;
using BeautySalonBooking.Application.Common.Interfaces;
using BeautySalonBooking.Application.Common.Services;
using BeautySalonBooking.Application.Organization.Interfaces;
using BeautySalonBooking.Application.Organization.Services;
using BeautySalonBooking.Application.Request.Interfaces;
using BeautySalonBooking.Application.Request.Services;
using BeautySalonBooking.Application.Requests.Services;
using BeautySalonBooking.Application.Scheduling.Interfaces;
using BeautySalonBooking.Application.Scheduling.Services;
using BeautySalonBooking.Application.Service.Interfaces;
using BeautySalonBooking.Application.Service.Services;
using BeautySalonBooking.Domain.BranchAggregate.Repositories;
using BeautySalonBooking.Domain.OrganizationAggregate.Repositories;
using BeautySalonBooking.Domain.RequestAggregate.Repositories;
using BeautySalonBooking.Domain.SchedulingAggregate.Services;
using BeautySalonBooking.Infrastructure.Persistence.Repositories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonBooking.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddMemoryCache();

        // Repositories
        services.AddScoped<IBranchRepository, BranchRepository>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddScoped<IOrganizationDetailRepository, OrganizationDetailRepository>();

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
        services.AddScoped<IBranchScheduleService, BranchScheduleService>();
        services.AddScoped<IBranchMemberScheduleService, BranchMemberScheduleService>();
        services.AddScoped<IBranchMemberAppService, BranchMemberAppService>();

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

        // Validation
        services.AddValidatorsFromAssemblyContaining<LoginInitiateRequestValidator>();
        services.AddScoped<IValidationService, ValidationService>();


        //Organization
        services.AddScoped<IOrganizationService, OrganizationService>();

        // Repositories
        services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
        services.AddScoped<IRequestTypeRepository, RequestTypeRepository>();
        services.AddScoped<IRequestRepository, RequestRepository>();
        services.AddScoped<IRequestRequiredDocumentRepository, RequestRequiredDocumentRepository>();
        // Services
        services.AddScoped<IDocumentTypeService, DocumentTypeService>();
        services.AddScoped<IRequestTypeService, RequestTypeService>();
        services.AddScoped<IRequestService, RequestService>();
        services.AddScoped<IRequestRequiredDocumentService, RequestRequiredDocumentService>();


        return services;
    }
}