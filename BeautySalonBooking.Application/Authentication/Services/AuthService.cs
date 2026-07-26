// Infrastructure/Services/AuthService.cs
using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Contracts.Authentication.Responses;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;
using BeautySalonBooking.Domain.Identity.UserAggregate.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace BeautySalonBooking.Application.Authentication.Services;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<AuthService> _logger;
    private readonly IUserSessionService _userSessionService;

    public AuthService(
        ITokenService tokenService,
        IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor,
        ILogger<AuthService> logger,
        IUserSessionService userSessionService)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _userSessionService = userSessionService;
    }

    public async Task<AuthResult> AuthenticateAsync(string username, string password, CancellationToken cancellationToken)
    {
        try
        {
            var existingUser = await _userRepository.GetByUserName(username);

            if (existingUser == null || !VerifyPassword(password, existingUser.PasswordHash))
            {
                _logger.LogWarning("Authentication failed for username: {Username}", username);
                return AuthResult.Failure("Invalid credentials");
            }

            // فعال کردن session کاربر
            await _userSessionService.ActivateUserSessionAsync(existingUser.Id);

            var currentUser = MapToUserDto(existingUser);
            var tokens = await _tokenService.GenerateTokensAsync(currentUser, cancellationToken);
            // ✅ دریافت منوهای کاربر
            //var menuService = _httpContextAccessor.HttpContext.RequestServices
            //    .GetRequiredService<IMenuService>();
            //var userMenus = await menuService.GetUserMenusAsync(existingUser.Id);


            _logger.LogInformation("User {Username} authenticated successfully. UserId: {UserId}",
                username, existingUser.Id);

            return AuthResult.Success(tokens, currentUser);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Authentication failed for username: {Username}", username);
            return AuthResult.Failure("Authentication failed");
        }
    }

    public async Task<AuthResult> RefreshTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken)
    {
        try
        {
            var tokens = await _tokenService.RefreshTokensAsync(accessToken, refreshToken, cancellationToken);

            var principal = _tokenService.ValidateToken(tokens.AccessToken, cancellationToken);
            var userId = principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id))
                return AuthResult.Failure("Invalid token");

            // بررسی فعال بودن session
            var isSessionActive = await _userSessionService.IsUserSessionActiveAsync(id);
            if (!isSessionActive)
            {
                _logger.LogWarning("Session is not active for user {UserId} during token refresh", id);
                return AuthResult.Failure("Session expired");
            }

            // تمدید session
            await _userSessionService.ExtendUserSessionAsync(id);

            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                return AuthResult.Failure("User not found");

            var currentUser = MapToUserDto(existingUser);

            _logger.LogInformation("Token refreshed for user {UserId}", userId);
            return AuthResult.Success(tokens, currentUser);
        }
        catch (SecurityTokenException ex)
        {
            _logger.LogWarning("Token refresh failed: {Message}", ex.Message);
            return AuthResult.Failure(ex.Message);
        }
    }

    public async Task<bool> ValidateTokenAsync(string token, CancellationToken cancellationToken)
    {
        try
        {
            var principal = _tokenService.ValidateToken(token, cancellationToken);
            if (principal == null) return false;

            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id))
                return false;

            // بررسی وجود کاربر در دیتابیس
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return false;

            // بررسی فعال بودن session
            var isSessionActive = await _userSessionService.IsUserSessionActiveAsync(id);
            return isSessionActive;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Token validation failed");
            return false;
        }
    }

    public async Task<User?> GetCurrentUserAsync()
    {
        try
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext?.User?.Identity?.IsAuthenticated != true)
                return null;

            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id))
                return null;

            // بررسی فعال بودن session
            var isSessionActive = await _userSessionService.IsUserSessionActiveAsync(id);
            if (!isSessionActive)
            {
                _logger.LogDebug("Session not active for user {UserId}", id);
                return null;
            }

            // تمدید session با هر بار دسترسی
            await _userSessionService.ExtendUserSessionAsync(id);

            return await _userRepository.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting current user");
            return null;
        }
    }

    public async Task<bool> HasPermissionAsync(string permission)
    {
        var user = await GetCurrentUserAsync();
        return true;// user?.AuthenticationType >= GetRequiredPermissionLevel(permission);
    }

    // ✅ متد جدید برای بررسی سریع session
    public async Task<SessionCheckResult> QuickSessionCheckAsync(int userId)
    {
        try
        {
            var isActive = await _userSessionService.IsUserSessionActiveAsync(userId);
            var sessionInfo = await _userSessionService.GetSessionInfoAsync(userId);

            if (!isActive)
            {
                _logger.LogDebug("Session check failed for user {UserId}", userId);
            }

            return new SessionCheckResult
            {
                IsValid = isActive,
                RequiresLogin = !isActive,
                Message = isActive ? "Session is active" : "Session expired or not found",
                SessionInfo = sessionInfo
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during quick session check for user {UserId}", userId);
            return new SessionCheckResult
            {
                IsValid = false,
                RequiresLogin = true,
                Message = "Error checking session"
            };
        }
    }

    // ✅ متد برای لاگ‌اوت
    public async Task<bool> LogoutAsync(long userId)
    {
        try
        {
            await _userSessionService.DeactivateUserSessionAsync(userId);
            _logger.LogInformation("User {UserId} logged out successfully", userId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error logging out user {UserId}", userId);
            return false;
        }
    }

    // ✅ متد برای لاگ‌اوت کاربر جاری
    public async Task<bool> LogoutCurrentUserAsync()
    {
        try
        {
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                return await LogoutAsync(user.Id);
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error logging out current user");
            return false;
        }
    }

    private static bool VerifyPassword(string password, string storedPassword)
    {
        // در واقعیت از BCrypt یا similar استفاده کنید
        // این فقط برای demo است
        return password == storedPassword;
    }

    private static int GetRequiredPermissionLevel(string permission) => permission switch
    {
        "read" => 1,
        "write" => 2,
        "admin" => 3,
        _ => 0
    };
    public async Task<User?> GetCurrentUserSafeAsync()
    {
        try
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext?.User?.Identity?.IsAuthenticated != true)
                return null;

            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id))
                return null;

            // بررسی session
            var isSessionActive = await _userSessionService.IsUserSessionActiveAsync(id);
            if (!isSessionActive)
            {
                _logger.LogWarning("Session not active for user {UserId}", id);
                return null;
            }

            // تمدید session
            await _userSessionService.ExtendUserSessionAsync(id);

            return await _userRepository.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetCurrentUserSafeAsync");
            return null;
        }
    }
    private static UserDto MapToUserDto(User user)
    {
        return new UserDto
        {
            //Id = user.Id,
            //PhoneNumber = user.PhoneNumber,
            //FirstName = user.FirstName,
            //LastName = user.LastName,
            //Email = user.Email,
            //AuthenticationType = user.AuthenticationType,
            //NationalCode = user.NationalCode,
            UserName = user.UserName
        };
    }
}










//using System.Security.Claims;
//using Core.DTOs;  // ✅ برای AuthResult و TokenResponse
//using Core.Entities;
//using Core.Interfaces;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using Microsoft.IdentityModel.Tokens;

//namespace Infrastructure.Services
//{
//    public class AuthService : IAuthService
//    {
//        private readonly ITokenService _tokenService;
//        private readonly IUserRepository _userRepository;
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private readonly ILogger<AuthService> _logger;

//        public AuthService(
//            ITokenService tokenService,
//            IUserRepository userRepository,
//            IHttpContextAccessor httpContextAccessor,
//            ILogger<AuthService> logger)
//        {
//            _tokenService = tokenService;
//            _userRepository = userRepository;
//            _httpContextAccessor = httpContextAccessor;
//            _logger = logger;
//        }

//        public async Task<AuthResult> AuthenticateAsync(string username, string password)
//        {
//            try
//            {
//                var existingUser = await _userRepository.GetByUserName(username);
//                UserDto currentUser = new UserDto();
//                currentUser.Id = existingUser.Id;
//                currentUser.PhoneNumber = existingUser.PhoneNumber;
//                currentUser.FirstName = existingUser.FirstName;
//                currentUser.LastName = existingUser.LastName;
//                currentUser.Email = existingUser.Email;
//                currentUser.AuthenticationType = existingUser.AuthenticationType;
//                currentUser.NationalCode = existingUser.NationalCode;
//                if (existingUser == null || !VerifyPassword(password, "hashed_password"))
//                    return AuthResult.Failure("Invalid credentials");

//                var tokens = await _tokenService.GenerateTokensAsync(currentUser);

//                _logger.LogInformation("User {Email} authenticated successfully", username);
//                return AuthResult.Success(tokens, currentUser);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Authentication failed for {Email}", username);
//                return AuthResult.Failure("Authentication failed");
//            }
//        }

//        public async Task<AuthResult> RefreshTokenAsync(string accessToken, string refreshToken)
//        {
//            try
//            {
//                var tokens = await _tokenService.RefreshTokensAsync(accessToken, refreshToken);

//                var principal = _tokenService.ValidateToken(tokens.AccessToken);
//                var userId = principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

//                if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id))
//                    return AuthResult.Failure("Invalid token");

//                var existingUser = await _userRepository.GetByIdAsync(id);

//                _logger.LogInformation("Token refreshed for user {UserId}", userId);

//                UserDto currentUser = new UserDto();
//                currentUser.Id = existingUser.Id;
//                currentUser.PhoneNumber = existingUser.PhoneNumber;
//                currentUser.FirstName = existingUser.FirstName;
//                currentUser.LastName = existingUser.LastName;
//                currentUser.Email = existingUser.Email;
//                currentUser.AuthenticationType = existingUser.AuthenticationType;
//                currentUser.NationalCode = existingUser.NationalCode;
//                return AuthResult.Success(tokens, currentUser);
//            }
//            catch (SecurityTokenException ex)
//            {
//                _logger.LogWarning("Token refresh failed: {Message}", ex.Message);
//                return AuthResult.Failure(ex.Message);
//            }
//        }

//        public async Task<bool> ValidateTokenAsync(string token)
//        {
//            var principal = _tokenService.ValidateToken(token);
//            if (principal == null) return false;

//            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id)) return false;

//            var user = await _userRepository.GetByIdAsync(id);
//            return user != null;
//        }

//        public async Task<User?> GetCurrentUserAsync()
//        {
//            try
//            {
//                var httpContext = _httpContextAccessor.HttpContext;
//                if (httpContext?.User?.Identity?.IsAuthenticated != true)
//                    return null;

//                var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//                if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id))
//                    return null;

//                return await _userRepository.GetByIdAsync(id);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error getting current user");
//                return null;
//            }
//        }

//        public async Task<bool> HasPermissionAsync(string permission)
//        {
//            var user = await GetCurrentUserAsync();
//           return user?.AuthenticationType >= GetRequiredPermissionLevel(permission);
//        }

//        private static bool VerifyPassword(string password, string passwordHash)
//        {
//            return password == "password"; // در واقعیت از BCrypt استفاده کن
//        }

//        private static int GetRequiredPermissionLevel(string permission) => permission switch
//        {
//            "read" => 1,
//            "write" => 2,
//            "admin" => 3,
//            _ => 0
//        };
//    }
//}



