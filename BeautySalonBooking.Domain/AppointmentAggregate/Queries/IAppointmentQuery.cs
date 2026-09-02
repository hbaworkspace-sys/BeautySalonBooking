using BeautySalonBooking.Domain.AppointmentAggregate.ReadModels;

namespace BeautySalonBooking.Domain.AppointmentAggregate.Queries;

public interface IAppointmentQuery
{
    Task<IReadOnlyList<AppointmentReadModel>> GetAllAsync(
        long customerUserId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AppointmentReadModel>> GetUpcomingAsync(
        long customerUserId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AppointmentReadModel>> GetHistoryAsync(
        long customerUserId,
        CancellationToken cancellationToken = default);
}