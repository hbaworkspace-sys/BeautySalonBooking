using BeautySalonBooking.Domain.AppointmentAggregate.Entities;
using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.BranchAggregate.Entities;
using BeautySalonBooking.Domain.CategoryAggregate.Entities;
using BeautySalonBooking.Domain.CommonAggregate.Entities;
using BeautySalonBooking.Domain.ContactAggregate.Entities;
using BeautySalonBooking.Domain.GeographyAggregate.Entities;
using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Entities;
using BeautySalonBooking.Domain.Identity.PermissionAggregate.Entities;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Entities;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;
using BeautySalonBooking.Domain.OrganizationAggregate.Entities;
using BeautySalonBooking.Domain.PersonAggregate.Entities;
using BeautySalonBooking.Domain.RequestAggregate.Entities;
using BeautySalonBooking.Domain.SchedulingAggregate.Entities;
using BeautySalonBooking.Domain.ServiceAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonBooking.Infrastructure;

public class BeautyDbContext : DbContext
{
    public BeautyDbContext(DbContextOptions<BeautyDbContext> options)
        : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(BeautyDbContext).Assembly);
    }

    //Appointment Aggregate
    public DbSet<Appointment> Appointments => Set<Appointment>();


    //Branch Aggregate
    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<BranchMember> BranchMembers => Set<BranchMember>();
    public DbSet<BranchMemberService> BranchMemberServices => Set<BranchMemberService>();
    public DbSet<BranchRole> BranchRoles => Set<BranchRole>();
    public DbSet<BranchService> BranchServices => Set<BranchService>();


    //Category Aggregate
    public DbSet<Category> Categories => Set<Category>();


    //Common Aggregate
    public DbSet<Color> Colors => Set<Color>();


    //Geography Aggregate
    public DbSet<Address> Addresss => Set<Address>();
    public DbSet<Region> Regions => Set<Region>();


    //Identity
    //Identity - Authentication Aggregate
    public DbSet<OtpCode> OtpCodes => Set<OtpCode>();

    //Identity - Permission Aggregate
    public DbSet<Permission> Permissions => Set<Permission>();

    //Identity - Permission Aggregate
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();


    //Identity - User Aggregate
    public DbSet<User> Users => Set<User>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();


    //Organization Aggregate
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<OrganizationDetail> OrganizationDetails => Set<OrganizationDetail>();
    public DbSet<OrganizationMedia> OrganizationMedias => Set<OrganizationMedia>();
    public DbSet<OrganizationOwner> OrganizationOwners => Set<OrganizationOwner>();


    //Person Aggregate
    public DbSet<Person> Persons => Set<Person>();


    //Request Aggregate
    public DbSet<DocumentType> DocumentTypes => Set<DocumentType>();
    public DbSet<OrganizationRequestDetail> OrganizationRequestDetails => Set<OrganizationRequestDetail>();
    public DbSet<PersonRequestDetail> PersonRequestDetails => Set<PersonRequestDetail>();
    public DbSet<Request> Requests => Set<Request>();
    public DbSet<RequestDetail> RequestDetails => Set<RequestDetail>();
    public DbSet<RequestDocument> RequestDocuments => Set<RequestDocument>();
    public DbSet<RequestField> RequestFields => Set<RequestField>();
    public DbSet<RequestFieldLookup> RequestFieldLookups => Set<RequestFieldLookup>();
    public DbSet<RequestHistory> RequestHistories => Set<RequestHistory>();
    public DbSet<RequestRequiredDocument> RequestRequiredDocuments => Set<RequestRequiredDocument>();
    public DbSet<RequestType> RequestTypes => Set<RequestType>();


    //Scheduling Aggregate
    public DbSet<BranchHoliday> BranchHolidays => Set<BranchHoliday>();
    public DbSet<BranchMemberSchedule> BranchMemberSchedules => Set<BranchMemberSchedule>();
    public DbSet<BranchSchedule> BranchSchedules => Set<BranchSchedule>();
    public DbSet<BranchWorkingHour> BranchWorkingHours => Set<BranchWorkingHour>();
    public DbSet<ScheduleException> ScheduleExceptions => Set<ScheduleException>();
    public DbSet<TimeOff> TimeOffs => Set<TimeOff>();
    public DbSet<WorkingShift> WorkingShifts => Set<WorkingShift>();


    // Service Aggregate
    public DbSet<Service> Services => Set<Service>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is AuditableEntity<long> entity)
            {
                if (entry.State == EntityState.Added)
                {
                    entity.MarkCreated(null);
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.MarkUpdated(null);
                }
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
}