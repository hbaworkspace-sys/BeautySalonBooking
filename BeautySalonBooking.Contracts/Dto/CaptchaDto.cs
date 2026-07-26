using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class CaptchaDto
    {
        public class CaptchaRequest
        {
            public string SessionId { get; set; } = string.Empty;
        }

        public class CaptchaResponse
        {
            public string CaptchaImage { get; set; } = string.Empty; // Base64 image
            public string CaptchaId { get; set; } = string.Empty; // شناسه یکتا برای کپچا
            public string SessionId { get; set; } = string.Empty;
            public DateTime ExpiresAt { get; set; }
        }

        public class LoginWithCaptchaRequest
        {
            public string UserName { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string CaptchaText { get; set; } = string.Empty; // متن وارد شده توسط کاربر
            public string CaptchaId { get; set; } = string.Empty; // شناسه کپچا
            public string SessionId { get; set; } = string.Empty;
        }
        public class SendOtpRequest
        {
            [Required(ErrorMessage = "شماره موبایل الزامی است")]
            [RegularExpression(@"^09[0-9]{9}$", ErrorMessage = "فرمت شماره موبایل نامعتبر است")]
            public string Mobile { get; set; }

            [StringLength(6, ErrorMessage = "کد پیگیری باید ۶ کاراکتر باشد")]
            public string TrackingCode { get; set; }
        }

        // Models/Requests/VerifyOtpRequest.cs
        public class VerifyOtpRequest
        {
            [Required(ErrorMessage = "شماره موبایل الزامی است")]
            public string Mobile { get; set; }

            [Required(ErrorMessage = "کد یکبارمصرف الزامی است")]
            [StringLength(6, MinimumLength = 6, ErrorMessage = "کد یکبارمصرف باید ۶ رقمی باشد")]
            public string OtpCode { get; set; }
        }

        // Models/Responses/OtpResponse.cs
        public class OtpResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public string TrackingCode { get; set; }
            public int RemainingTime { get; set; } // زمان باقی‌مانده به ثانیه
            public DateTime ExpireTime { get; set; }
        }

        // Models/OtpInfo.cs
        public class OtpInfo
        {
            public string Mobile { get; set; }
            public string OtpCode { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime ExpireTime { get; set; }
            public int Attempts { get; set; }
            public bool IsUsed { get; set; }
            public string TrackingCode { get; set; }
        }
    }
}
