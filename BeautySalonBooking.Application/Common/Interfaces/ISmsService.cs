using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Common.Interfaces
{
    public interface ISmsService
    {
        Task<SmsResult> SendOtpSmsAsync(string mobile, string otpCode);
        //Task<SmsResult> SendWelcomeSmsAsync(string mobile, string userName);

    }
}
