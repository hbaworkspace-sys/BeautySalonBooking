using BeautySalonBooking.Contracts.Authentication.Requests;
using BeautySalonBooking.Contracts.Authentication.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.WebApp.Interfaces.Common;
using BeautySalonBooking.WebApp.Interfaces.Login;
using BeautySalonBooking.WebApp.Settings;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace BeautySalonBooking.WebApp.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly IApiClient _apiClient;
        private readonly ILogger<LoginService> _logger;
        private readonly ApiSettings _settings;

        public LoginService(
            IApiClient apiClient,
            IOptions<ApiSettings> settings,
            ILogger<LoginService> logger)
        {
            _apiClient = apiClient;
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task<ApiResponse_New<AuthResult>> RequestLoginOtpAsync(LoginInitiateRequest request)
        {
            try
            {
                _logger.LogInformation("Requesting OTP for mobile: {MobileNumber}", request.MobileNumber);

                var endpoint = _settings.Endpoints.Authentication.RequestOtp;
                var result = await _apiClient.PostAsync<LoginInitiateRequest, ApiResponse_New<AuthResult>>(endpoint, request);

                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP error in RequestLoginOtpAsync");
                return new ApiResponse_New<AuthResult>
                {
                    IsSuccess = false,
                    Code = 500,
                    Message = "خطا در ارتباط با سرور"
                };
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON error in RequestLoginOtpAsync");
                return new ApiResponse_New<AuthResult>
                {
                    IsSuccess = false,
                    Code = 500,
                    Message = "پاسخ نامعتبر از سرور"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RequestLoginOtpAsync");
                return new ApiResponse_New<AuthResult>
                {
                    IsSuccess = false,
                    Code = 500,
                    Message = "خطای داخلی سرور"
                };
            }
        }

        public async Task<ApiResponse_New<AuthResult>> VerifyLoginOtpAsync(VerifyOtpRequest request)
        {
            try
            {
                _logger.LogInformation("Verifying OTP for mobile: {MobileNumber}", request.MobileNumber);

                var endpoint = _settings.Endpoints.Authentication.VerifyOtp;
                var result = await _apiClient.PostAsync<VerifyOtpRequest, ApiResponse_New<AuthResult>>(endpoint, request);

                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP error in VerifyLoginOtpAsync");
                return new ApiResponse_New<AuthResult>
                {
                    IsSuccess = false,
                    Code = 500,
                    Message = "خطا در ارتباط با سرور"
                };
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON error in VerifyLoginOtpAsync");
                return new ApiResponse_New<AuthResult>
                {
                    IsSuccess = false,
                    Code = 500,
                    Message = "پاسخ نامعتبر از سرور"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in VerifyLoginOtpAsync");
                return new ApiResponse_New<AuthResult>
                {
                    IsSuccess = false,
                    Code = 500,
                    Message = "خطای داخلی سرور"
                };
            }
        }
    }
}
