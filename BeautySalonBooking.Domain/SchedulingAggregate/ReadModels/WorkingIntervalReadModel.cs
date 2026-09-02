namespace BeautySalonBooking.Domain.SchedulingAggregate.ReadModels;

public sealed class WorkingIntervalReadModel
{
    public DayOfWeek DayOfWeek { get; init; }

    public TimeOnly StartTime { get; init; }

    public TimeOnly EndTime { get; init; }
}