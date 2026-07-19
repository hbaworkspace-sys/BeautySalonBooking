using BeautySalonBooking.Contracts.Auth.Requests;
using BeautySalonBooking.Contracts.Auth.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Maui.Common.Services;
using BeautySalonBooking.Maui.Features.Auth.Constants;

namespace BeautySalonBooking.Maui.Features.Auth
{
    public class AuthApiService : BaseApiService
    {
        public AuthApiService(HttpClient httpClient)
                : base(httpClient)
        {
        }
        public Task<ApiResponse<AuthResult>> RequestOtpForRegisterAsync(RegisterInitiateRequest request)
        {
            return SendAsync<RegisterInitiateRequest, AuthResult>(
                HttpMethod.Post,
                AuthRoutes.RegisterInitiate,
                request);
        }

        public Task<ApiResponse<AuthResult>> ConfirmRegistrationAsync(VerifyOtpRequest request)
        {
            return SendAsync<VerifyOtpRequest, AuthResult>(
                HttpMethod.Post,
                AuthRoutes.RegisterVerifyOtp,
                request);
        }

        public Task<ApiResponse<AuthResult>> RequestOtpForLoginAsync(LoginInitiateRequest request)
        {
            return SendAsync<LoginInitiateRequest, AuthResult>(
                HttpMethod.Post,
                AuthRoutes.LoginInitiate,
                request);
        }

        public Task<ApiResponse<AuthResult>> ConfirmLoginAsync(VerifyOtpRequest request)
        {
            return SendAsync<VerifyOtpRequest, AuthResult>(
                HttpMethod.Post,
                AuthRoutes.LoginVerifyOtp,
                request);
        }
    }
}
