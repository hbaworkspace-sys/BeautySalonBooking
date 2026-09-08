using BeautySalonBooking.Domain.AppointmentAggregate.Enums;
using BeautySalonBooking.Domain.AppointmentAggregate.Queries;
using BeautySalonBooking.Domain.AppointmentAggregate.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonBooking.Infrastructure.Persistence.Queries;

public sealed class AppointmentQuery : IAppointmentQuery
{
    private readonly BeautyDbContext _context;

    public AppointmentQuery(BeautyDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<AppointmentReadModel>> GetAllAsync(
        long customerUserId,
        CancellationToken cancellationToken = default)
    {
        return await BuildQuery(customerUserId)
            .OrderByDescending(x => x.Date)
            .ThenByDescending(x => x.StartTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<AppointmentReadModel>> GetUpcomingAsync(
        long customerUserId,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.Now;
        var today = DateOnly.FromDateTime(now);
        var currentTime = TimeOnly.FromDateTime(now);

        return await BuildQuery(customerUserId)
            .Where(x =>
                x.Status == AppointmentStatus.Pending ||
                x.Status == AppointmentStatus.Confirmed ||
                x.Status == AppointmentStatus.CheckedIn ||
                x.Status == AppointmentStatus.InProgress)
            .Where(x =>
                x.Date > today ||
                (x.Date == today && x.StartTime >= currentTime))
            .OrderBy(x => x.Date)
            .ThenBy(x => x.StartTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<AppointmentReadModel>> GetHistoryAsync(
        long customerUserId,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.Now;
        var today = DateOnly.FromDateTime(now);
        var currentTime = TimeOnly.FromDateTime(now);

        return await BuildQuery(customerUserId)
            .Where(x =>
                x.Status == AppointmentStatus.Completed ||
                x.Status == AppointmentStatus.Rejected ||
                x.Status == AppointmentStatus.Cancelled ||
                x.Status == AppointmentStatus.NoShow ||
                x.Date < today ||
                (x.Date == today && x.EndTime < currentTime))
            .OrderByDescending(x => x.Date)
            .ThenByDescending(x => x.StartTime)
            .ToListAsync(cancellationToken);
    }

    private IQueryable<AppointmentReadModel> BuildQuery(
        long customerUserId)
    {
        return _context.Appointments
            .AsNoTracking()
            .Where(a =>
                a.CustomerUserId == customerUserId &&
                a.IsActive &&
                !a.IsDeleted)
            .Select(a => new AppointmentReadModel
            {
                AppointmentId = a.Id,

                ServiceTitle =
                    a.BranchMemberService
                        .BranchService
                        .Service
                        .Title,

                OrganizationTitle =
                    a.BranchMemberService
                        .BranchService
                        .Branch
                        .Organization
                        .Title,

                BranchTitle =
                    a.BranchMemberService
                        .BranchService
                        .Branch
                        .Title,

                StylistName =
                    a.BranchMemberService
                        .BranchMember
                        .Person
                        .FirstName
                    + " " +
                    a.BranchMemberService
                        .BranchMember
                        .Person
                        .LastName,

                Date = a.Date,
                StartTime = a.StartTime,
                EndTime = a.EndTime,
                TotalPrice = a.TotalPrice,
                Status = a.Status,
                PaymentStatus = a.PaymentStatus
            });
    }
}