namespace BeautySalonBooking.Contracts.Common;
public class ApiResponse_New<T>
{
    public bool IsSuccess { get; set; }
    public bool RequiresLogin { get; set; }
    public string Message { get; set; } = string.Empty;
    public int Code { get; set; }

    public T? Payload { get; set; }
    public List<T> ListPayload { get; set; } = new();

    public List<string> Errors { get; set; } = new();

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public static ApiResponse_New<T> SuccessResponse(
        T payload,
        string message = "Operation completed successfully",
        int code = 200)
    {
        return new ApiResponse_New<T>
        {
            IsSuccess = true,
            Message = message,
            Code = code,
            Payload = payload
        };
    }

    public static ApiResponse_New<T> FailureResponse(
        string message,
        int code = 400,
        List<string>? errors = null)
    {
        return new ApiResponse_New<T>
        {
            IsSuccess = false,
            Message = message,
            Code = code,
            Errors = errors ?? new()
        };
    }

    public static ApiResponse_New<T> CreatedResponse(
        T payload,
        string message = "Resource created successfully")
    {
        return new ApiResponse_New<T>
        {
            IsSuccess = true,
            Message = message,
            Code = 201,
            Payload = payload
        };
    }
}

// برای مواقعی که Payload نداریم
public class ApiResponse
{
    public bool IsSuccess { get; set; }
    public bool RequiresLogin { get; set; }
    public string Message { get; set; } = string.Empty;
    public int Code { get; set; }

    public List<string> Errors { get; set; } = new();

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public static ApiResponse SuccessResponse(
        string message = "Operation completed successfully",
        int code = 200)
    {
        return new ApiResponse
        {
            IsSuccess = true,
            Message = message,
            Code = code
        };
    }

    public static ApiResponse FailureResponse(
        string message,
        int code = 400,
        List<string>? errors = null)
    {
        return new ApiResponse
        {
            IsSuccess = false,
            Message = message,
            Code = code,
            Errors = errors ?? new()
        };
    }

    public static ApiResponse CreatedResponse(
        string message = "Resource created successfully")
    {
        return new ApiResponse
        {
            IsSuccess = true,
            Message = message,
            Code = 201
        };
    }
}