using BeautySalonBooking.Common.Exceptions;
using BeautySalonBooking.Contracts.Common;
using Microsoft.AspNetCore.Diagnostics;
using FluentValidation;

namespace BeautySalonBooking.API.Common.Exceptions;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(
            exception,
            "Unhandled exception occurred.");

        var statusCode = exception switch
        {
            ValidationException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            ConflictException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        var message = exception switch
        {
            ValidationException => "اطلاعات وارد شده معتبر نیست.",
            NotFoundException => exception.Message,
            ConflictException => exception.Message,
            _ => "خطای داخلی سرور رخ داده است."
        };

        var errors = exception is ValidationException validationException
            ? validationException.Errors
                .Select(x => x.ErrorMessage)
                .Distinct()
                .ToList()
            : new List<string>();

        httpContext.Response.StatusCode = statusCode;

        var response = ApiResponse_New<object>.FailureResponse(
            message,
            statusCode,
            errors);

        await httpContext.Response.WriteAsJsonAsync(
            response,
            cancellationToken);

        return true;
    }
}