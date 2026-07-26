namespace BeautySalonBooking.Contracts.Common;

public class ApiResponse_New<T>
{
    public bool IsSuccess { get; set; }
    public bool RequiresLogin { get; set; }
    public string Message { get; set; } = string.Empty;
    public int Code { get; set; }
    public T? Payload { get; set; }
    public List<T> ListPayload { get; set; }
    public List<string> Errors { get; set; } = new();
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    // Constructorهای مختلف
    public ApiResponse_New() { }

    public ApiResponse_New(bool success, string message, int code, T? payload)
    {
        IsSuccess = success;
        Message = message;
        Code = code;
        Payload = payload;
    }

    // ✅ متدهای کمکی با پارامتر code
    public static ApiResponse_New<T> SuccessResponse(T payload, string message = "Operation completed successfully", int code = 200)
    {
        return new ApiResponse_New<T>
        {
            IsSuccess = true,
            Message = message,
            Code = code,
            Payload = payload
        };
    }

    public static ApiResponse_New<T> FailureResponse(string message, int code = 400, List<string>? errors = null)
    {
        return new ApiResponse_New<T>
        {
            IsSuccess = false,
            Message = message,
            Code = code,
            Errors = errors ?? new List<string>()
        };
    }

    //public static ApiResponse<T> NotFoundResponse(string message = "Resource not found")
    //{
    //    return new ApiResponse<T>
    //    {
    //        Success = false,
    //        Message = message,
    //        Code = 404
    //    };
    //}

    public static ApiResponse_New<T> CreatedResponse(T payload, string message = "Resource created successfully")
    {
        return new ApiResponse_New<T>
        {
            IsSuccess = true,
            Message = message,
            Code = 200,
            Payload = payload
        };
    }

    public static ApiResponse_New<T> SuccessResponse(T payload, string message = "عملیات با موفقیت انجام شد")
    {
        return new ApiResponse_New<T>
        {
            IsSuccess = true,
            Message = message,
            Payload = payload,
            Code = 200
        };
    }

    public static ApiResponse_New<T> ErrorResponse(string message, List<string> errors = null)
    {
        return new ApiResponse_New<T>
        {
            IsSuccess = false,
            Message = message,
            Errors = errors ?? new List<string>(),
            Code = 400
        };
    }

    public static ApiResponse_New<T> NotFoundResponse(string message = "موردی یافت نشد")
    {
        return new ApiResponse_New<T>
        {
            IsSuccess = false,
            Message = message,
            Code = 404
        };
    }

    public static ApiResponse_New<T> UnauthorizedResponse(string message = "دسترسی غیرمجاز")
    {
        return new ApiResponse_New<T>
        {
            IsSuccess = false,
            Message = message,
            Code = 401
        };
    }

    public static ApiResponse_New<T> InternalErrorResponse(string message = "خطای داخلی سرور")
    {
        return new ApiResponse_New<T>
        {
            IsSuccess = false,
            Message = message,
            Code = 500
        };
    }
}


public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; init; }
    public List<T> ListData { get; init; }
}

// برای مواقعی که Payload نداریم
public class ApiResponse
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; } = string.Empty;
    public int Code { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public IReadOnlyCollection<ApiError>? Errors { get; init; }

    public static ApiResponse SuccessResponse(string message = "Operation completed successfully", int code = 200)
    {
        return new ApiResponse
        {
            IsSuccess = true,
            Message = message,
            Code = code
        };
    }

    public static ApiResponse FailureResponse(string message, int code = 400, List<string>? errors = null)
    {
        return new ApiResponse
        {
            IsSuccess = false,
            Message = message,
            Code = code,
            // Errors = errors ?? new List<string>()
        };
    }

    public static ApiResponse CreatedResponse(string message = "Resource created successfully")
    {
        return new ApiResponse
        {
            IsSuccess = true,
            Message = message,
            Code = 200
        };
    }
}