namespace BeautySalonBooking.Domain.SchedulingAggregate.ReadModels;

public sealed class ScheduleExceptionReadModel
{
    public DateOnly Date { get; init; }

    public TimeOnly StartTime { get; init; }

    public TimeOnly EndTime { get; init; }

    public bool IsWorkingDay { get; init; }
}