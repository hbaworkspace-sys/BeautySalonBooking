using BeautySalonBooking.Contracts.Authentication.Requests;
using BeautySalonBooking.Contracts.Authentication.Responses;
using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.WebApp.Interfaces.Login
{
    public interface ILoginService
    {
        Task<ApiResponse_New<AuthResult>> RequestLoginOtpAsync(LoginInitiateRequest request);
        Task<ApiResponse_New<AuthResult>> VerifyLoginOtpAsync(VerifyOtpRequest request);
        Task<bool> Logout();
    }
}
