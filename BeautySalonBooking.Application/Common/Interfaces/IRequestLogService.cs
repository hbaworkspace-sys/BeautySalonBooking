using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace BeautySalonBooking.Application.Common.Interfaces
{
    public interface IRequestLogService
    {
        Task LogRequestAsync(HttpContext context, DateTime requestTime);
        Task LogResponseAsync(HttpContext context, DateTime requestTime, Exception? exception = null);
        string? GetUserIdFromContext(HttpContext context);
        string? GetUserNameFromContext(HttpContext context);
    }
}
