using BeautySalonBooking.Contracts.Auth.Requests;
using BeautySalonBooking.Contracts.Auth.Responses;
using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.Application.Authentication;

public interface IAuthenticationService
{
    Task<ApiResponse> RequestRegisterOtpAsync(
        RegisterInitiateRequest request,
        CancellationToken cancellationToken);

    Task<ApiResponse<AuthResult>> VerifyRegisterOtpAsync(
        VerifyOtpRequest request,
        CancellationToken cancellationToken);

    Task<ApiResponse> RequestLoginOtpAsync(
        LoginInitiateRequest request,
        CancellationToken cancellationToken);

    Task<ApiResponse<AuthResult>> VerifyLoginOtpAsync(
        VerifyOtpRequest request,
        CancellationToken cancellationToken);
}