using BeautySalonBooking.Domain.AppointmentAggregate.Entities;

namespace BeautySalonBooking.Domain.AppointmentAggregate.Repositories;

public interface IAppointmentRepository
{
    Task<bool> HasConflictAsync(
        long branchMemberServiceId,
        DateOnly date,
        TimeOnly startTime,
        TimeOnly endTime,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Appointment appointment,
        CancellationToken cancellationToken = default);

    Task AcquireBookingLockAsync(
        long branchMemberServiceId,
        DateOnly date,
        CancellationToken cancellationToken = default);
}