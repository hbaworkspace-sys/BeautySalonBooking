using BeautySalonBooking.Contracts.Authentication.Requests;
using BeautySalonBooking.Contracts.Authentication.Responses;
using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.Maui.Features.Auth.Services;

public interface IAuthApiService
{
    Task<ApiResponse_New<AuthResult>> RequestOtpForRegisterAsync(
        RegisterInitiateRequest request,
        CancellationToken cancellationToken = default);

    Task<ApiResponse_New<AuthResult>> ConfirmRegistrationAsync(
        VerifyOtpRequest request,
        CancellationToken cancellationToken = default);

    Task<ApiResponse_New<AuthResult>> RequestOtpForLoginAsync(
        LoginInitiateRequest request,
        CancellationToken cancellationToken = default);

    Task<ApiResponse_New<AuthResult>> ConfirmLoginAsync(
        VerifyOtpRequest request,
        CancellationToken cancellationToken = default);
}