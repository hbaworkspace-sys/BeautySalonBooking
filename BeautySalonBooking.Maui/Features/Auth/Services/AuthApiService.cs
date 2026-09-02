using BeautySalonBooking.Contracts.Authentication.Requests;
using BeautySalonBooking.Contracts.Authentication.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Maui.Common.Services;
using BeautySalonBooking.Maui.Features.Auth.Constants;
using BeautySalonBooking.Maui.Features.Auth.Services;
using Microsoft.Extensions.Logging;

namespace BeautySalonBooking.Maui.Features.Auth;

public class AuthApiService : BaseApiService, IAuthApiService
{
    public AuthApiService(HttpClient httpClient, ILogger<AuthApiService> logger)
        : base(httpClient, logger)
    {
    }

    public Task<ApiResponse_New<AuthResult>> RequestOtpForRegisterAsync(
        RegisterInitiateRequest request,
        CancellationToken cancellationToken = default)
        => PostAsync<RegisterInitiateRequest, AuthResult>(
            AuthRoutes.RegisterInitiate,
            request,
            cancellationToken);

    public Task<ApiResponse_New<AuthResult>> ConfirmRegistrationAsync(
        VerifyOtpRequest request,
        CancellationToken cancellationToken = default)
    {
        return PostAsync<VerifyOtpRequest, AuthResult>(
            AuthRoutes.RegisterVerifyOtp,
            request,
            cancellationToken);
       
    }

    public Task<ApiResponse_New<AuthResult>> RequestOtpForLoginAsync(
        LoginInitiateRequest request,
        CancellationToken cancellationToken = default)
        => PostAsync<LoginInitiateRequest, AuthResult>(
            AuthRoutes.LoginInitiate,
            request,
            cancellationToken);

    public Task<ApiResponse_New<AuthResult>> ConfirmLoginAsync(
        VerifyOtpRequest request,
        CancellationToken cancellationToken = default)
        => PostAsync<VerifyOtpRequest, AuthResult>(
            AuthRoutes.LoginVerifyOtp,
            request,
            cancellationToken);
}