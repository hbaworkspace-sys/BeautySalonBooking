using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Contracts.Authentication.Dtos
{
    public class SessionInfo
    {
        public long UserId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime LastActivity { get; set; }
        public TimeSpan TimeRemaining => ExpiresAt - DateTime.UtcNow;
        public bool IsExpired => DateTime.UtcNow > ExpiresAt;
    }

    public class SessionCheckResult
    {
        public bool IsValid { get; set; }
        public bool RequiresLogin { get; set; }
        public string Message { get; set; } = string.Empty;
        public SessionInfo? SessionInfo { get; set; }
    }
}
