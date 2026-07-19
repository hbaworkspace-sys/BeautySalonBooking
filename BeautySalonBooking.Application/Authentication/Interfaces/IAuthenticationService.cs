using BeautySalonBooking.Contracts.Auth.Requests;
using BeautySalonBooking.Contracts.Auth.Responses;
using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.Application.Authentication
{
    public interface IAuthenticationService
    {
        Task<ApiResponse> RequestOtpForRegisterAsync(RegisterInitiateRequest request);
        Task<ApiResponse<AuthResult>> ConfirmRegistrationAsync(VerifyOtpRequest request);
        Task<ApiResponse> RequestOtpForLoginAsync(LoginInitiateRequest request);
        Task<ApiResponse<AuthResult>> ConfirmLoginAsync(VerifyOtpRequest request);
    }
}