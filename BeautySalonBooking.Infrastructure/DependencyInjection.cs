using BeautySalonBooking.Domain.AppointmentAggregate.Queries;
using BeautySalonBooking.Domain.AppointmentAggregate.Repositories;
using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.Base.UnitOfWork;
using BeautySalonBooking.Domain.BranchAggregate.Repositories;
using BeautySalonBooking.Domain.CategoryAggregate.Repositories;
using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Repositories;
using BeautySalonBooking.Domain.PersonAggregate.Repositories;
using BeautySalonBooking.Domain.Repositories;
using BeautySalonBooking.Domain.SchedulingAggregate.Queries;
using BeautySalonBooking.Domain.ServiceAggregate.Repositories;
using BeautySalonBooking.Infrastructure.Persistence.Queries;
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
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IBranchServiceRepository, BranchServiceRepository>();
        services.AddScoped<IBranchMemberRepository, BranchMemberRepository>();

        services.AddScoped<IBookingAvailabilityQuery, BookingAvailabilityQuery>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IAppointmentQuery, AppointmentQuery>();

        return services;
    }
}