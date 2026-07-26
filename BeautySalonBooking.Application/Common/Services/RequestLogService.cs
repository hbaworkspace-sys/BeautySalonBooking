namespace BeautySalonBooking.Application.Common.Services
{
    //public class RequestLogService : IRequestLogService
    //{
    //    private readonly IRequestLogRepository _logRepository;
    //    private readonly ILogger<RequestLogService> _logger;

    //    public RequestLogService(IRequestLogRepository logRepository, ILogger<RequestLogService> logger)
    //    {
    //        _logRepository = logRepository;
    //        _logger = logger;
    //    }

    //    public async Task LogRequestAsync(HttpContext context, DateTime requestTime)
    //    {
    //        try
    //        {
    //            var log = new RequestLog
    //            {
    //                HttpMethod = context.Request.Method,
    //                Url = context.Request.Path + context.Request.QueryString,
    //                IPAddress = GetClientIPAddress(context),
    //                UserId = GetUserIdFromContext(context) != null ? int.Parse(GetUserIdFromContext(context)!) : null,
    //                UserName = GetUserNameFromContext(context),
    //                RequestTime = requestTime,
    //                RequestHeaders = SerializeHeaders(context.Request.Headers),
    //                RequestBody = await GetRequestBodyAsync(context.Request),
    //                // ControllerName و ActionName را حذف کردیم
    //            };

    //            await _logRepository.AddAsync(log);
    //            context.Items["RequestLogId"] = log.Id;
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, "Error logging request");
    //        }
    //    }

    //    public async Task LogResponseAsync(HttpContext context, DateTime requestTime, Exception? exception = null)
    //    {
    //        try
    //        {
    //            if (!context.Items.ContainsKey("RequestLogId") || context.Items["RequestLogId"] == null)
    //                return;

    //            var logId = (int)context.Items["RequestLogId"]!;
    //            var log = await _logRepository.GetByIdAsync(logId);
    //            if (log == null) return;

    //            var responseTime = DateTime.UtcNow;
    //            var executionTime = (long)(responseTime - requestTime).TotalMilliseconds;

    //            log.ResponseTime = responseTime;
    //            log.StatusCode = context.Response.StatusCode;
    //            log.ExecutionTime = executionTime;
    //            log.ResponseStatus = exception != null ? "Exception" :
    //                               context.Response.StatusCode >= 400 ? "Error" : "Success";

    //            if (exception != null || context.Response.StatusCode >= 400)
    //            {
    //                log.ResponseBody = await GetResponseBodyAsync(context.Response);
    //            }

    //            if (exception != null)
    //            {
    //                log.ExceptionType = exception.GetType().Name;
    //                log.ExceptionMessage = exception.Message;
    //                log.ExceptionStackTrace = exception.StackTrace;
    //            }

    //            await _logRepository.UpdateAsync(log);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, "Error logging response");
    //        }
    //    }

    //    public string? GetUserIdFromContext(HttpContext context)
    //    {
    //        var userIdClaim = context.User?.FindFirst(ClaimTypes.NameIdentifier);
    //        return userIdClaim?.Value;
    //    }

    //    public string? GetUserNameFromContext(HttpContext context)
    //    {
    //        var userNameClaim = context.User?.FindFirst(ClaimTypes.Name);
    //        return userNameClaim?.Value;
    //    }

    //    private string? GetClientIPAddress(HttpContext context)
    //    {
    //        return context.Connection.RemoteIpAddress?.ToString();
    //    }

    //    private string SerializeHeaders(IHeaderDictionary headers)
    //    {
    //        try
    //        {
    //            // روش ساده بدون JSON
    //            var headerString = string.Join("; ", headers.Select(h => $"{h.Key}: {h.Value}"));
    //            return headerString.Length > 1000 ? headerString[..1000] + "..." : headerString;
    //        }
    //        catch
    //        {
    //            return "Unable to serialize headers";
    //        }
    //    }

    //    private async Task<string?> GetRequestBodyAsync(HttpRequest request)
    //    {
    //        try
    //        {
    //            if (!new[] { "POST", "PUT", "PATCH" }.Contains(request.Method.ToUpper()))
    //                return null;

    //            request.EnableBuffering();

    //            using var reader = new StreamReader(request.Body, Encoding.UTF8,
    //                detectEncodingFromByteOrderMarks: false,
    //                bufferSize: 1024,
    //                leaveOpen: true);

    //            var body = await reader.ReadToEndAsync();
    //            request.Body.Position = 0;

    //            return body.Length > 2000 ? body[..2000] + "... [TRUNCATED]" : body;
    //        }
    //        catch
    //        {
    //            return "Unable to read request body";
    //        }
    //    }

    //    private async Task<string?> GetResponseBodyAsync(HttpResponse response)
    //    {
    //        try
    //        {
    //            if (response.Body == null || !response.Body.CanRead || !response.Body.CanSeek)
    //                return null;

    //            // ایجاد یک کپی از response body برای خواندن
    //            response.Body.Seek(0, SeekOrigin.Begin);
    //            using var reader = new StreamReader(response.Body, Encoding.UTF8,
    //                detectEncodingFromByteOrderMarks: false,
    //                bufferSize: 1024,
    //                leaveOpen: true);

    //            var body = await reader.ReadToEndAsync();
    //            response.Body.Seek(0, SeekOrigin.Begin);

    //            return body.Length > 2000 ? body[..2000] + "... [TRUNCATED]" : body;
    //        }
    //        catch
    //        {
    //            return "Unable to read response body";
    //        }
    //    }
    //}
}
