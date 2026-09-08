namespace BeautySalonBooking.Contracts.Service.Dtos;

public sealed class ServiceDto
{
    public long Id { get; init; }

    public int CategoryId { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Code { get; init; } = string.Empty;

    public string? Description { get; init; }

    public decimal BasePrice { get; init; }

    public TimeSpan BaseDuration { get; init; }
}