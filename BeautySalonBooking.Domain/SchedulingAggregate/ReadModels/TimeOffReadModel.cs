namespace BeautySalonBooking.Domain.SchedulingAggregate.ReadModels;

public sealed class TimeOffReadModel
{
    public DateOnly StartDate { get; init; }

    public DateOnly EndDate { get; init; }

    public TimeOnly? StartTime { get; init; }

    public TimeOnly? EndTime { get; init; }
}