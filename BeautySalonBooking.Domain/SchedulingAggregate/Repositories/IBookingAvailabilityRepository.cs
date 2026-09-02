using BeautySalonBooking.Domain.SchedulingAggregate.ReadModels;

namespace BeautySalonBooking.Domain.SchedulingAggregate.Repositories;

public interface IBookingAvailabilityRepository
{
    Task<BookingAvailabilityReadModel?>
        GetAsync(
            long branchMemberServiceId,
            DateOnly fromDate,
            DateOnly toDate,
            CancellationToken cancellationToken = default);
}