using BeautySalonBooking.Domain.AppointmentAggregate.Entities;
using BeautySalonBooking.Domain.AppointmentAggregate.Enums;
using BeautySalonBooking.Domain.AppointmentAggregate.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories;

public sealed class AppointmentRepository :
    IAppointmentRepository
{
    private readonly BeautyDbContext _dbContext;

    public AppointmentRepository(
        BeautyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> HasConflictAsync(
        long branchMemberServiceId,
        DateOnly date,
        TimeOnly startTime,
        TimeOnly endTime,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.Set<Appointment>()
            .AnyAsync(
                appointment =>
                    appointment.BranchMemberServiceId ==
                    branchMemberServiceId &&

                    appointment.Date == date &&

                    appointment.IsActive &&
                    !appointment.IsDeleted &&

                    appointment.Status !=
                        AppointmentStatus.Cancelled &&

                    appointment.Status !=
                        AppointmentStatus.Rejected &&

                    appointment.Status !=
                        AppointmentStatus.NoShow &&

                    appointment.StartTime < endTime &&
                    appointment.EndTime > startTime,
                cancellationToken);
    }

    public async Task AddAsync(
        Appointment appointment,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<Appointment>()
            .AddAsync(
                appointment,
                cancellationToken);
    }

    public async Task AcquireBookingLockAsync(
    long branchMemberServiceId,
    DateOnly date,
    CancellationToken cancellationToken = default)
    {
        var resource =
            $"BOOKING:{branchMemberServiceId}:{date:yyyy-MM-dd}";

        await _dbContext.Database.ExecuteSqlInterpolatedAsync(
            $"""
        EXEC sp_getapplock
            @Resource = {resource},
            @LockMode = 'Exclusive',
            @LockOwner = 'Transaction',
            @LockTimeout = 10000
        """,
            cancellationToken);
    }



}