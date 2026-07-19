namespace BeautySalonBooking.Contracts.Common
{
    public class ApiResponse
    {
        public bool IsSuccess { get; init; }

        public string? Message { get; init; }

        public IReadOnlyCollection<ApiError>? Errors { get; init; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T? Data { get; init; }
    }
}
