using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Contracts.Authentication.Requests;
using BeautySalonBooking.Contracts.Authentication.Responses;
using BeautySalonBooking.Contracts.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService/*, IValidator<LoginInitiateRequest> validator*/)
    {
        _authenticationService = authenticationService;
    }

    [AllowAnonymous]
    [HttpPost("register/request-otp")]
    public async Task<IActionResult> RegisterInitiate(
        [FromBody] RegisterInitiateRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _authenticationService
            .RequestRegisterOtpAsync(request, cancellationToken);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("register/verify-otp")]
    public async Task<IActionResult> VerifyRegistrationOtp([FromBody] VerifyOtpRequest request, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.VerifyRegisterOtpAsync(request, cancellationToken);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("login/request-otp")]
    public async Task<IActionResult> LoginInitiate([FromBody] LoginInitiateRequest request, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.RequestLoginOtpAsync(request, cancellationToken);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("login/verify-otp")]
    public async Task<IActionResult> VerifyLoginOtp([FromBody] VerifyOtpRequest request, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.VerifyLoginOtpAsync(request, cancellationToken);
        return Ok(result);
    }


    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        try
        {
            // دریافت Access Token از هدر
            var accessToken = HttpContext.Request.Headers["Authorization"]
                .ToString()
                .Replace("Bearer ", "");

            if (string.IsNullOrEmpty(accessToken))
            {
                return BadRequest(new ApiResponse_New<AuthResult>
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = "Access token در هدر ارسال نشده است"
                });
            }

            if (string.IsNullOrEmpty(request.RefreshToken))
            {
                return BadRequest(new ApiResponse_New<AuthResult>
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = "Refresh token ارسال نشده است"
                });
            }

            var result = await _authenticationService.RefreshTokenAsync(
                accessToken,
                request.RefreshToken,
                CancellationToken.None);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return Unauthorized(result);
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "Error in RefreshToken endpoint");
            return StatusCode(500, new ApiResponse_New<AuthResult>
            {
                IsSuccess = false,
                Code = 500,
                Message = "خطای داخلی سرور"
            });
        }
    }

    // ========== متد Logout ==========
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        try
        {
            // دریافت UserId از Claim
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !long.TryParse(userIdClaim, out long userId))
            {
                return BadRequest(new { Message = "کاربر نامعتبر" });
            }

            // غیرفعال کردن Session
            await _authenticationService.LogoutAsync(userId);

            return Ok(new { Message = "خروج با موفقیت انجام شد" });
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "Error in Logout endpoint");
            return StatusCode(500, new { Message = "خطای داخلی سرور" });
        }
    }


    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser(
    CancellationToken cancellationToken)
    {
        var userIdClaim =
            User.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (!long.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new ApiResponse_New<UserDto>
            {
                IsSuccess = false,
                Code = 401,
                Message = "کاربر احراز هویت نشده است"
            });
        }

        var result =
            await _authenticationService
                .GetCurrentUserAsync(
                    userId,
                    cancellationToken);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }

}