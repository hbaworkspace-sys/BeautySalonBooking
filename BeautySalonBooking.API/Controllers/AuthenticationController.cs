using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Application.Authentication.Services;
using BeautySalonBooking.Contracts.Authentication.Requests;

using BeautySalonBooking.Contracts.Authentication.Responses;
using BeautySalonBooking.Contracts.Common;
//using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    //private readonly IValidator<LoginInitiateRequest> _validator;

    public AuthenticationController(IAuthenticationService authenticationService/*, IValidator<LoginInitiateRequest> validator*/)
    {
        _authenticationService = authenticationService;
        //  _validator = validator;
    }

    [HttpPost("register/request-otp")]
    public async Task<IActionResult> RegisterInitiate([FromBody] RegisterInitiateRequest request, CancellationToken cancellationToken)
    {

        var result = await _authenticationService.RequestRegisterOtpAsync(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("register/verify-otp")]
    public async Task<IActionResult> VerifyRegistrationOtp([FromBody] VerifyOtpRequest request, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.VerifyRegisterOtpAsync(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("login/request-otp")]
    public async Task<IActionResult> LoginInitiate([FromBody] LoginInitiateRequest request, CancellationToken cancellationToken)
    {
        //var validationResult = await _validator.ValidateAsync(request);
        //if (!validationResult.IsValid)
        //{
        //    return BadRequest(validationResult.Errors);
        //}
        var result = await _authenticationService.RequestLoginOtpAsync(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("login/verify-otp")]
    public async Task<IActionResult> VerifyLoginOtp([FromBody] VerifyOtpRequest request, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.VerifyLoginOtpAsync(request, cancellationToken);
        return Ok(result);
    }


    // ========== متد جدید برای Refresh Token ==========
    [HttpPost("refresh-token")]
    [AllowAnonymous] // یا [Authorize] اگر نیاز دارید
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
    [HttpPost("logout")]
    [Authorize]
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




}