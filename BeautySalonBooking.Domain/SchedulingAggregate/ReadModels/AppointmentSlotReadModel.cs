using BeautySalonBooking.Domain.AppointmentAggregate.Enums;

namespace BeautySalonBooking.Domain.SchedulingAggregate.ReadModels;

public sealed class AppointmentSlotReadModel
{
    public DateOnly Date { get; init; }

    public TimeOnly StartTime { get; init; }

    public TimeOnly EndTime { get; init; }

    public AppointmentStatus Status { get; init; }
}