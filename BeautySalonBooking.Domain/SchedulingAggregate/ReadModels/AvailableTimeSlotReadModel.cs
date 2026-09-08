namespace BeautySalonBooking.Domain.SchedulingAggregate.ReadModels;

public sealed class AvailableTimeSlotReadModel
{
    public TimeOnly StartTime { get; init; }

    public TimeOnly EndTime { get; init; }
}