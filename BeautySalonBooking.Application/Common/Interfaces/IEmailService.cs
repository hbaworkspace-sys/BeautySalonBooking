using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendWelcomeEmailAsync(string email, string name);
    }
}
