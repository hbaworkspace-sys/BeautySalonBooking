using BeautySalonBooking.Contracts.Authentication;
using BeautySalonBooking.Contracts.Authentication.Requests;
using BeautySalonBooking.Contracts.Authentication.Responses;
using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.Application.Authentication.Interfaces;

public interface IAuthenticationService
{
    Task<ApiResponse> RequestRegisterOtpAsync(
        RegisterInitiateRequest request,
        CancellationToken cancellationToken);

    Task<ApiResponse_New<AuthResult>> VerifyRegisterOtpAsync(
        VerifyOtpRequest request,
        CancellationToken cancellationToken);

    Task<ApiResponse_New<AuthResult>> RequestLoginOtpAsync(
        LoginInitiateRequest request,
        CancellationToken cancellationToken);

    Task<ApiResponse_New<AuthResult>> VerifyLoginOtpAsync(
        VerifyOtpRequest request,
        CancellationToken cancellationToken);
    // ========== متد جدید برای Refresh Token ==========
    Task<ApiResponse_New<AuthResult>> RefreshTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken);

    Task<bool> LogoutAsync(long userId); // متد جدید
}