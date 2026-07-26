namespace BeautySalonBooking.Contracts.Common;

public sealed class ApiError
{
    public string Code { get; init; } = default!;

    public string Message { get; init; } = default!;
}
