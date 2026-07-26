// Infrastructure/Services/UserSessionService.cs
using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Contracts.Authentication.Dtos;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace BeautySalonBooking.Application.Authentication.Services
{
    public class UserSessionService : IUserSessionService, IDisposable
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<UserSessionService> _logger;
        private readonly Timer _cleanupTimer;
        private const int SESSION_TIMEOUT_MINUTES = 30;
        private const int CLEANUP_INTERVAL_MINUTES = 5;

        public UserSessionService(IMemoryCache memoryCache, ILogger<UserSessionService> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;

            // تایمر برای پاکسازی خودکار
            _cleanupTimer = new Timer(async _ => await CleanupExpiredSessionsAsync(),
                null, TimeSpan.FromMinutes(CLEANUP_INTERVAL_MINUTES),
                TimeSpan.FromMinutes(CLEANUP_INTERVAL_MINUTES));

            _logger.LogInformation("UserSessionService initialized");
        }

        public Task<bool> IsUserSessionActiveAsync(long userId)
        {
            var cacheKey = GetCacheKey(userId);

            if (!_memoryCache.TryGetValue(cacheKey, out SessionInfo session))
                return Task.FromResult(false);

            var isActive = session.IsActive && !session.IsExpired;

            if (!isActive)
            {
                // اگر منقضی شده، از cache حذف کن
                _memoryCache.Remove(cacheKey);
                _logger.LogDebug("Session expired for user {UserId}", userId);
            }

            return Task.FromResult(isActive);
        }

        public Task ActivateUserSessionAsync(long userId)
        {
            var cacheKey = GetCacheKey(userId);
            var session = new SessionInfo
            {
                UserId = userId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(SESSION_TIMEOUT_MINUTES),
                LastActivity = DateTime.UtcNow
            };

            // 5 دقیقه بیشتر برای buffer
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(SESSION_TIMEOUT_MINUTES + 5))
                .RegisterPostEvictionCallback((key, value, reason, state) =>
                {
                    _logger.LogDebug("Session removed from cache. Key: {Key}, Reason: {Reason}", key, reason);
                });

            _memoryCache.Set(cacheKey, session, cacheOptions);

            _logger.LogInformation("Session activated for user {UserId}. Expires at {ExpiresAt}",
                userId, session.ExpiresAt);

            return Task.CompletedTask;
        }

        public Task DeactivateUserSessionAsync(long userId)
        {
            var cacheKey = GetCacheKey(userId);
            _memoryCache.Remove(cacheKey);

            _logger.LogInformation("Session deactivated for user {UserId}", userId);
            return Task.CompletedTask;
        }

        public Task ExtendUserSessionAsync(long userId)
        {
            var cacheKey = GetCacheKey(userId);

            if (_memoryCache.TryGetValue(cacheKey, out SessionInfo session))
            {
                session.LastActivity = DateTime.UtcNow;
                session.ExpiresAt = DateTime.UtcNow.AddMinutes(SESSION_TIMEOUT_MINUTES);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(SESSION_TIMEOUT_MINUTES + 5));

                _memoryCache.Set(cacheKey, session, cacheOptions);

                _logger.LogDebug("Session extended for user {UserId}. New expiry: {ExpiresAt}",
                    userId, session.ExpiresAt);
            }

            return Task.CompletedTask;
        }

        public Task<SessionInfo> GetSessionInfoAsync(long userId)
        {
            var cacheKey = GetCacheKey(userId);

            if (_memoryCache.TryGetValue(cacheKey, out SessionInfo session))
            {
                return Task.FromResult(session);
            }

            return Task.FromResult<SessionInfo>(null);
        }

        //public Task<long> GetActiveSessionsCountAsync()
        //{
        //    // در پیاده‌سازی واقعی،可能需要 یک لیست جداگانه نگهداری کنید
        //    // این یک پیاده‌سازی ساده است
        //    return Task.FromResult(0);
        //}

        private async Task CleanupExpiredSessionsAsync()
        {
            try
            {
                // در پیاده‌سازی production، اینجا sessionهای منقضی را پاک کنید
                // برای MemoryCache، این کار به صورت خودکار انجام می‌شود
                _logger.LogDebug("Session cleanup completed at {Time}", DateTime.UtcNow);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during session cleanup");
            }
        }

        private static string GetCacheKey(long userId) => $"user_session_{userId}";

        public void Dispose()
        {
            _cleanupTimer?.Dispose();
            _logger.LogInformation("UserSessionService disposed");
        }
    }
}