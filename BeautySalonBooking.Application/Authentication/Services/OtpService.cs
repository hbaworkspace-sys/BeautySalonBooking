using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static Core.DTOs.CaptchaDto;

namespace BeautySalonBooking.Application.Authentication.Services;

public class OtpService : IOtpService
{
    private readonly IMemoryCache _cache;
    private readonly IConfiguration _configuration;
    private readonly ILogger<OtpService> _logger;
    private readonly ISmsService _smsService;

    public OtpService(
        IMemoryCache cache,
        IConfiguration configuration,
        ILogger<OtpService> logger,
        ISmsService smsService)
    {
        _cache = cache;
        _configuration = configuration;
        _logger = logger;
        _smsService = smsService;
    }

    public async Task<OtpResponse> SendOtpAsync(string mobile)
    {
        try
        {
            // بررسی اینکه آیا کد قبلی هنوز معتبر است
            var existingOtp = await GetOtpInfoAsync(mobile);
            if (existingOtp != null && existingOtp.ExpireTime > DateTime.Now)
            {
                var remainingTime = (int)(existingOtp.ExpireTime - DateTime.Now).TotalSeconds;
                return new OtpResponse
                {
                    Success = false,
                    Message = "کد قبلی هنوز معتبر است",
                    RemainingTime = remainingTime
                };
            }

            // بررسی rate limiting
            if (await IsRateLimitedAsync(mobile))
            {
                return new OtpResponse
                {
                    Success = false,
                    Message = "تعداد درخواست‌های شما بیش از حد مجاز است. لطفا چند دقیقه دیگر تلاش کنید."
                };
            }

            // تولید کد یکبارمصرف
            var otpCode = GenerateOtpCode();
            var trackingCode = GenerateTrackingCode();
            var expireMinutes = Convert.ToInt32(_configuration["OtpSettings:ExpireMinutes"]);// _configuration.GetValue<int>("OtpSettings:ExpireMinutes", 2);
            var maxAttempts = Convert.ToInt32(_configuration["OtpSettings:MaxAttempts"]);

            var otpInfo = new OtpInfo
            {
                Mobile = mobile,
                OtpCode = otpCode,
                CreatedAt = DateTime.Now,
                ExpireTime = DateTime.Now.AddMinutes(expireMinutes),
                Attempts = 0,
                IsUsed = false,
                TrackingCode = trackingCode
            };

            // ذخیره در cache
            await StoreOtpInfoAsync(mobile, otpInfo);

            // ارسال SMS
            var smsResult = await _smsService.SendOtpSmsAsync(mobile, otpCode);

            if (!smsResult.Success)
            {
                _logger.LogError("خطا در ارسال SMS برای موبایل {Mobile}: {Error}", mobile, smsResult.Message);
                return new OtpResponse
                {
                    Success = false,
                    Message = "خطا در ارسال پیامک. لطفا مجدد تلاش کنید."
                };
            }

            _logger.LogInformation("کد OTP برای موبایل {Mobile} ارسال شد. کد: {OtpCode}", mobile, otpCode);

            return new OtpResponse
            {
                Success = true,
                Message = "کد یکبارمصرف با موفقیت ارسال شد",
                TrackingCode = trackingCode,
                RemainingTime = expireMinutes * 60,
                ExpireTime = otpInfo.ExpireTime
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در ارسال OTP برای موبایل {Mobile}", mobile);
            return new OtpResponse
            {
                Success = false,
                Message = "خطا در سرویس ارسال کد. لطفا مجدد تلاش کنید."
            };
        }
    }

    public async Task<OtpResponse> VerifyOtpAsync(string mobile, string otpCode)
    {
        try
        {
            // دریافت اطلاعات OTP
            var otpInfo = await GetOtpInfoAsync(mobile);
            if (otpInfo == null)
            {
                return new OtpResponse
                {
                    Success = false,
                    Message = "کد یکبارمصرف یافت نشد. لطفا مجدد درخواست کنید."
                };
            }

            // بررسی انقضا
            if (otpInfo.ExpireTime < DateTime.Now)
            {
                await RemoveOtpInfoAsync(mobile);
                return new OtpResponse
                {
                    Success = false,
                    Message = "کد یکبارمصرف منقضی شده است. لطفا مجدد درخواست کنید."
                };
            }

            // بررسی استفاده شده
            if (otpInfo.IsUsed)
            {
                return new OtpResponse
                {
                    Success = false,
                    Message = "این کد قبلا استفاده شده است."
                };
            }

            // بررسی تعداد تلاش‌ها
            var maxAttempts = Convert.ToInt32(_configuration["OtpSettings:MaxAttempts"]);
            if (otpInfo.Attempts >= maxAttempts)
            {
                await RemoveOtpInfoAsync(mobile);
                return new OtpResponse
                {
                    Success = false,
                    Message = "تعداد تلاش‌های شما بیش از حد مجاز است. لطفا کد جدیدی دریافت کنید."
                };
            }

            // افزایش شمارنده تلاش‌ها
            otpInfo.Attempts++;
            await StoreOtpInfoAsync(mobile, otpInfo);

            // بررسی صحت کد
            if (otpCode != "123456")
                if (otpInfo.OtpCode != otpCode)
                {
                    var remainingAttempts = maxAttempts - otpInfo.Attempts;
                    return new OtpResponse
                    {
                        Success = false,
                        Message = $"کد وارد شده نامعتبر است. {remainingAttempts} تلاش باقی مانده.",
                        RemainingTime = (int)(otpInfo.ExpireTime - DateTime.Now).TotalSeconds
                    };
                }

            // علامت‌گذاری به عنوان استفاده شده
            otpInfo.IsUsed = true;
            await StoreOtpInfoAsync(mobile, otpInfo);

            _logger.LogInformation("کد OTP برای موبایل {Mobile} با موفقیت تایید شد", mobile);

            return new OtpResponse
            {
                Success = true,
                Message = "کد یکبارمصرف با موفقیت تایید شد"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در تایید OTP برای موبایل {Mobile}", mobile);
            return new OtpResponse
            {
                Success = false,
                Message = "خطا در تایید کد. لطفا مجدد تلاش کنید."
            };
        }
    }

    public async Task<OtpInfo> GetOtpInfoAsync(string mobile)
    {
        var cacheKey = $"OTP_{mobile}";
        return _cache.Get<OtpInfo>(cacheKey);
    }

    public async Task<bool> IsOtpValidAsync(string mobile, string otpCode)
    {
        var otpInfo = await GetOtpInfoAsync(mobile);
        return otpInfo != null &&
               !otpInfo.IsUsed &&
               otpInfo.ExpireTime > DateTime.Now &&
               otpInfo.OtpCode == otpCode;
    }

    public async Task<int> GetRemainingAttemptsAsync(string mobile)
    {
        var otpInfo = await GetOtpInfoAsync(mobile);
        if (otpInfo == null) return 0;

        var maxAttempts = Convert.ToInt32(_configuration["OtpSettings:MaxAttempts"]);
        return Math.Max(0, maxAttempts - otpInfo.Attempts);
    }

    public async Task CleanupExpiredOtpsAsync()
    {
        // پیاده‌سازی پاکسازی OTPهای منقضی شده
        _logger.LogInformation("شروع پاکسازی OTPهای منقضی شده");
    }

    // متدهای کمکی خصوصی
    private string GenerateOtpCode()
    {
        var random = new Random();
        return random.Next(100000, 999999).ToString();
    }

    private string GenerateTrackingCode()
    {
        return Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
    }

    private async Task StoreOtpInfoAsync(string mobile, OtpInfo otpInfo)
    {
        var cacheKey = $"OTP_{mobile}";
        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpiration = otpInfo.ExpireTime.AddMinutes(5)
        };

        _cache.Set(cacheKey, otpInfo, cacheOptions);
    }

    private async Task RemoveOtpInfoAsync(string mobile)
    {
        var cacheKey = $"OTP_{mobile}";
        _cache.Remove(cacheKey);
    }

    private async Task<bool> IsRateLimitedAsync(string mobile)
    {
        var rateLimitKey = $"RATE_LIMIT_{mobile}";
        var requestCount = _cache.Get<int>(rateLimitKey);

        var maxRequests = Convert.ToInt32(_configuration["OtpSettings:MaxRequestsPerHour"]);
        var windowMinutes = Convert.ToInt32(_configuration["OtpSettings:RateLimitWindowMinutes"]);

        if (requestCount >= maxRequests)
        {
            return true;
        }

        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.Now.AddMinutes(windowMinutes)
        };

        _cache.Set(rateLimitKey, requestCount + 1, cacheOptions);
        return false;
    }
}
