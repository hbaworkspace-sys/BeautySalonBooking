using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text;
using BeautySalonBooking.WebApp.Settings;
using BeautySalonBooking.WebApp.Interfaces.Common;
using BeautySalonBooking.Contracts.Authentication.Requests;
using BeautySalonBooking.Contracts.Authentication.Responses;
using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.WebApp.Services.Common
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiClient> _logger;
        private readonly ApiSettings _settings;
        private readonly ITokenService _tokenService;
        private readonly IServiceProvider _serviceProvider;
        private readonly SemaphoreSlim _refreshLock = new SemaphoreSlim(1, 1);
        private bool _isRefreshing = false;

        public ApiClient(
            HttpClient httpClient,
            IOptions<ApiSettings> settings,
            ILogger<ApiClient> logger,
            ITokenService tokenService,
            IServiceProvider serviceProvider)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _logger = logger;
            _tokenService = tokenService;
            _serviceProvider = serviceProvider;
            _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
        }

        private async Task AddAuthorizationHeaderAsync()
        {
            var token = await _tokenService.GetAccessTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        // ========== متد جدید برای Refresh Token ==========
        private async Task<bool> RefreshTokenAsync()
        {
            if (_isRefreshing)
            {
                await _refreshLock.WaitAsync();

                try
                {
                    var existingToken = await _tokenService.GetAccessTokenAsync();
                    return !string.IsNullOrEmpty(existingToken);
                }
                finally
                {
                    _refreshLock.Release();
                }
            }


            await _refreshLock.WaitAsync();

            try
            {
                _isRefreshing = true;


                var refreshToken = await _tokenService.GetRefreshTokenAsync();

                if (string.IsNullOrWhiteSpace(refreshToken))
                {
                    _logger.LogWarning("Refresh token not found");
                    return false;
                }


                var request = new RefreshTokenRequest
                {
                    RefreshToken = refreshToken
                };


                var json = JsonSerializer.Serialize(request);

                using var content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json"
                );


                // خواندن Endpoint از appsettings.json
                var endpoint = _settings.Endpoints.Authentication.RefreshToken;


                _logger.LogInformation(
                    "Sending refresh token request to {Endpoint}",
                    endpoint
                );


                var response = await _httpClient.PostAsync(
                    endpoint,
                    content
                );


                if (!response.IsSuccessStatusCode)
                {
                    var error =
                        await response.Content.ReadAsStringAsync();

                    _logger.LogWarning(
                        "Refresh token failed. Status: {Status}, Response: {Response}",
                        response.StatusCode,
                        error
                    );

                    return false;
                }


                var responseContent =
                    await response.Content.ReadAsStringAsync();


                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };


                /*
                  اگر API شما این خروجی را دارد:

                  {
                     "isSuccess":true,
                     "payload":{
                         "accessToken":"",
                         "refreshToken":"",
                         "expiresAt":""
                     }
                  }

                */

                var result =
                    JsonSerializer.Deserialize<ApiResponse_New<TokenResponse>>(
                        responseContent,
                        options
                    );


                if (result == null ||
                    !result.IsSuccess ||
                    result.Payload == null)
                {
                    _logger.LogWarning(
                        "Invalid refresh token response"
                    );

                    return false;
                }


                await _tokenService.UpdateTokensAsync(
                    result.Payload
                );


                _logger.LogInformation(
                    "Token refreshed successfully"
                );


                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error while refreshing token"
                );

                return false;
            }
            finally
            {
                _isRefreshing = false;
                _refreshLock.Release();
            }
        }

        // ========== متد کمکی برای ارسال درخواست با مدیریت 401 ==========
        private async Task<HttpResponseMessage> SendWithAuthAsync(
            Func<Task<HttpResponseMessage>> sendRequest,
            CancellationToken cancellationToken,
            int retryCount = 0)
        {
            // اضافه کردن هدر Authorization
            await AddAuthorizationHeaderAsync();

            var response = await sendRequest();

            // اگر 401 برگشت و کمتر از 2 بار تلاش شده
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized && retryCount < 2)
            {
                _logger.LogWarning($"⚠️ Received 401 (attempt {retryCount + 1}), trying to refresh token...");

                // تلاش برای Refresh Token
                var refreshed = await RefreshTokenAsync();

                if (refreshed)
                {
                    // حذف هدر قبلی و اضافه کردن توکن جدید
                    _httpClient.DefaultRequestHeaders.Authorization = null;
                    await AddAuthorizationHeaderAsync();

                    // ارسال مجدد درخواست (با یک بار تلاش بیشتر)
                    return await SendWithAuthAsync(sendRequest, cancellationToken, retryCount + 1);
                }
                else
                {
                    // اگر Refresh موفق نبود، توکن‌ها رو پاک کن
                    await _tokenService.ClearTokensAsync();
                    _logger.LogWarning("⚠️ Token refresh failed, user logged out");

                    // پرتاب Exception برای هدایت به صفحه لاگین
                    throw new UnauthorizedAccessException("Session expired. Please login again.");
                }
            }

            return response;
        }

        // ========== متدهای اصلی با تغییرات ==========
        public async Task<TResponse> PostAsync<TRequest, TResponse>(
            string endpoint,
            TRequest request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var fullUrl = BuildUrl(endpoint);
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await SendWithAuthAsync(
                    () => _httpClient.PostAsync(fullUrl, content, cancellationToken),
                    cancellationToken);

                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("API Error: {StatusCode} - {Content}", response.StatusCode, responseContent);
                    throw new HttpRequestException($"خطا در ارتباط با سرور: {response.StatusCode}");
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var result = JsonSerializer.Deserialize<TResponse>(responseContent, options);

                if (result == null)
                {
                    throw new JsonException("پاسخ نامعتبر از سرور");
                }

                return result;
            }
            catch (UnauthorizedAccessException)
            {
                // این Exception رو به بالا ارسال می‌کنیم تا در Component مدیریت شود
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PostAsync for endpoint: {Endpoint}", endpoint);
                throw;
            }
        }

        public async Task<TResponse> GetAsync<TResponse>(
            string endpoint,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var fullUrl = BuildUrl(endpoint);

                var response = await SendWithAuthAsync(
                    () => _httpClient.GetAsync(fullUrl, cancellationToken),
                    cancellationToken);

                var content = await response.Content.ReadAsStringAsync(cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("API Error: {StatusCode} - {Content}", response.StatusCode, content);
                    throw new HttpRequestException($"خطا در ارتباط با سرور: {response.StatusCode}");
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var result = JsonSerializer.Deserialize<TResponse>(content, options);

                return result ?? throw new JsonException("پاسخ نامعتبر از سرور");
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAsync for endpoint: {Endpoint}", endpoint);
                throw;
            }
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse>(
            string endpoint,
            TRequest request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var fullUrl = BuildUrl(endpoint);
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await SendWithAuthAsync(
                    () => _httpClient.PutAsync(fullUrl, content, cancellationToken),
                    cancellationToken);

                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("API Error: {StatusCode} - {Content}", response.StatusCode, responseContent);
                    throw new HttpRequestException($"خطا در ارتباط با سرور: {response.StatusCode}");
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var result = JsonSerializer.Deserialize<TResponse>(responseContent, options);

                return result ?? throw new JsonException("پاسخ نامعتبر از سرور");
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PutAsync for endpoint: {Endpoint}", endpoint);
                throw;
            }
        }

        public async Task<TResponse> DeleteAsync<TResponse>(
            string endpoint,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var fullUrl = BuildUrl(endpoint);

                var response = await SendWithAuthAsync(
                    () => _httpClient.DeleteAsync(fullUrl, cancellationToken),
                    cancellationToken);

                var content = await response.Content.ReadAsStringAsync(cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("API Error: {StatusCode} - {Content}", response.StatusCode, content);
                    throw new HttpRequestException($"خطا در ارتباط با سرور: {response.StatusCode}");
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var result = JsonSerializer.Deserialize<TResponse>(content, options);

                return result ?? throw new JsonException("پاسخ نامعتبر از سرور");
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteAsync for endpoint: {Endpoint}", endpoint);
                throw;
            }
        }

        private string BuildUrl(string endpoint)
        {
            var cleanEndpoint = endpoint.StartsWith("/") ? endpoint[1..] : endpoint;
            return cleanEndpoint;
        }
    }
}