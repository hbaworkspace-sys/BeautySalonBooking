using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.CaptchaDto;

namespace BeautySalonBooking.Application.Authentication.Interfaces
{
    public interface IOtpService
    {
        Task<OtpResponse> SendOtpAsync(string mobile);
        Task<OtpResponse> VerifyOtpAsync(string mobile, string otpCode);
        Task<OtpInfo> GetOtpInfoAsync(string mobile);
        Task<bool> IsOtpValidAsync(string mobile, string otpCode);
        Task<int> GetRemainingAttemptsAsync(string mobile);
        Task CleanupExpiredOtpsAsync();
    }
}
