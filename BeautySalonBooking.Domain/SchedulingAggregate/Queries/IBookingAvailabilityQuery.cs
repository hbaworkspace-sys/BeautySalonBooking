using BeautySalonBooking.Domain.SchedulingAggregate.ReadModels;

namespace BeautySalonBooking.Domain.SchedulingAggregate.Queries;

public interface IBookingAvailabilityQuery
{
    Task<BookingAvailabilityReadModel?>
        GetAsync(
            long branchMemberServiceId,
            DateOnly fromDate,
            DateOnly toDate,
            CancellationToken cancellationToken = default);
}