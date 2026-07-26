using BeautySalonBooking.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Common.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public async Task SendWelcomeEmailAsync(string email, string name)
        {
            // در واقعیت از SendGrid, MailKit, etc. استفاده می‌کنی
            _logger.LogInformation("Sending welcome email to {Email} for {Name}", email, name);

            // شبیه‌سازی ارسال ایمیل
            await Task.Delay(100);

            _logger.LogInformation("Welcome email sent to {Email}", email);
        }
    }
}
