using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Contracts.Authentication.Requests;
using BeautySalonBooking.Contracts.Authentication.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.WebApp.Interfaces.Common;
using BeautySalonBooking.WebApp.Interfaces.Login;
using BeautySalonBooking.WebApp.Settings;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace BeautySalonBooking.WebApp.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly IApiClient _apiClient;
        private readonly ILogger<LoginService> _logger;
        private readonly ApiSettings _settings;
        private readonly ITokenService _tokenService;
        private readonly NavigationManager _navigationManager;

        public LoginService(
            IApiClient apiClient,
            IOptions<ApiSettings> settings,
            NavigationManager navigationManager,
            ITokenService tokenService,
            ILogger<LoginService> logger)
        {
            _apiClient = apiClient;
            _settings = settings.Value;
            _logger = logger;
            _tokenService = tokenService;
            _navigationManager = navigationManager;
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

                if (result.IsSuccess)
                {
                    // حالت 1: یک کاربر
                    if (result.Payload?.Tokens != null)
                    {
                        await _tokenService.SetTokensAsync(result.Payload.Tokens, result.Payload.User);

                        // ذخیره کاربر در لیست کاربران
                        var userList = new List<UserDto> { result.Payload.User };
                        await _tokenService.SetAllUsersAsync(userList);

                        _logger.LogInformation("✅ Single user login. UserId: {UserId}", result.Payload.User?.Id);
                    }
                    // حالت 2: چند کاربر
                    else if (result.ListPayload != null && result.ListPayload.Any())
                    {
                        _logger.LogInformation($"✅ Multiple users found: {result.ListPayload.Count}");

                        // ذخیره تمام توکن‌ها
                        await _tokenService.SetMultipleTokensAsync(result.ListPayload);

                        // ذخیره لیست کامل کاربران
                        var allUsers = result.ListPayload.Select(x => x.User).ToList();
                        await _tokenService.SetAllUsersAsync(allUsers);

                        // به صورت پیش‌فرض، اولین کاربر را انتخاب می‌کنیم
                        var firstUser = result.ListPayload.First();
                        await _tokenService.SetTokensAsync(firstUser.Tokens, firstUser.User);

                        _logger.LogInformation("✅ Multiple users saved. Default user: {UserId}", firstUser.User?.Id);
                    }
                }

                return result;
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


        public async Task<bool> Logout()
        {
            try
            {
                await _tokenService.ClearTokensAsync();
                _navigationManager.NavigateTo("/login", true);
                _logger.LogInformation("User logged out");
                return true;
            }
            catch (Exception)
            {
                return false;

            }

        }



        //public async Task<ApiResponse_New<AuthResult>> VerifyLoginOtpAsync(VerifyOtpRequest request)
        //{
        //    try
        //    {
        //        _logger.LogInformation("Verifying OTP for mobile: {MobileNumber}", request.MobileNumber);

        //        var endpoint = _settings.Endpoints.Authentication.VerifyOtp;
        //        var result = await _apiClient.PostAsync<VerifyOtpRequest, ApiResponse_New<AuthResult>>(endpoint, request);

        //        if (result.IsSuccess)
        //        {
        //            if (result.Payload != null)
        //                await _tokenService.SetTokensAsync(result.Payload.Tokens, result.Payload.User);
        //            else if (result.ListPayload.Count() > 0)
        //            {
        //                foreach (var token in result.ListPayload)
        //                {
        //                    await _tokenService.SetTokensAsync(token.Tokens, token.User);
        //                }
        //            }
        //        }

        //        return result;
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        _logger.LogError(ex, "HTTP error in VerifyLoginOtpAsync");
        //        return new ApiResponse_New<AuthResult>
        //        {
        //            IsSuccess = false,
        //            Code = 500,
        //            Message = "خطا در ارتباط با سرور"
        //        };
        //    }
        //    catch (JsonException ex)
        //    {
        //        _logger.LogError(ex, "JSON error in VerifyLoginOtpAsync");
        //        return new ApiResponse_New<AuthResult>
        //        {
        //            IsSuccess = false,
        //            Code = 500,
        //            Message = "پاسخ نامعتبر از سرور"
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error in VerifyLoginOtpAsync");
        //        return new ApiResponse_New<AuthResult>
        //        {
        //            IsSuccess = false,
        //            Code = 500,
        //            Message = "خطای داخلی سرور"
        //        };
        //    }
        //}




    }
}
