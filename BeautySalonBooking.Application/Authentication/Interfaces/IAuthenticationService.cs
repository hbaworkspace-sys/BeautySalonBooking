using BeautySalonBooking.Contracts.Authentication.Requests;
using BeautySalonBooking.Contracts.Authentication.Responses;
using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.Application.Authentication.Interfaces;

public interface IAuthenticationService
{

    /// <summary>
    /// ارسال OTP برای شروع ثبت نام
    /// </summary>
    Task<ApiResponse> RequestRegisterOtpAsync(
        RegisterInitiateRequest request,
        CancellationToken cancellationToken);



    /// <summary>
    /// تایید OTP ثبت نام و ایجاد کاربر
    /// </summary>
    Task<ApiResponse_New<AuthResult>> VerifyRegisterOtpAsync(
        VerifyOtpRequest request,
        CancellationToken cancellationToken);



    /// <summary>
    /// ارسال OTP برای ورود
    /// </summary>
    Task<ApiResponse_New<AuthResult>> RequestLoginOtpAsync(
        LoginInitiateRequest request,
        CancellationToken cancellationToken);



    /// <summary>
    /// تایید OTP ورود و دریافت Token
    /// </summary>
    Task<ApiResponse_New<AuthResult>> VerifyLoginOtpAsync(
        VerifyOtpRequest request,
        CancellationToken cancellationToken);



    /// <summary>
    /// Refresh کردن Access Token با Refresh Token
    /// </summary>
    Task<ApiResponse_New<AuthResult>> RefreshTokenAsync(
        string accessToken,
        string refreshToken,
        CancellationToken cancellationToken);



    /// <summary>
    /// خروج کاربر از سیستم
    /// </summary>
    Task<bool> LogoutAsync(
        long userId,
        CancellationToken cancellationToken = default);



    /// <summary>
    /// خروج از تمام دستگاه‌ها
    /// </summary>
    Task<bool> LogoutAllDevicesAsync(
        long userId,
        CancellationToken cancellationToken);

}