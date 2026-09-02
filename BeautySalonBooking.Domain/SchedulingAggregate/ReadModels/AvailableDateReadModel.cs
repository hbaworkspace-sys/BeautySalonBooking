namespace BeautySalonBooking.Domain.SchedulingAggregate.ReadModels;

public sealed class AvailableDateReadModel
{
    public DateOnly Date { get; init; }

    public IReadOnlyList<AvailableTimeSlotReadModel> Slots { get; init; } = [];
}