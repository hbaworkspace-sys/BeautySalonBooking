using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text;
using BeautySalonBooking.WebApp.Settings;
using BeautySalonBooking.WebApp.Interfaces.Common;

namespace BeautySalonBooking.WebApp.Services.Common
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiClient> _logger;
        private readonly ApiSettings _settings;

        public ApiClient(
            HttpClient httpClient,
            IOptions<ApiSettings> settings,
            ILogger<ApiClient> logger)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _logger = logger;

            _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
        }

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

                var response = await _httpClient.PostAsync(fullUrl, content, cancellationToken);
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
                var response = await _httpClient.GetAsync(fullUrl, cancellationToken);
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

                var response = await _httpClient.PutAsync(fullUrl, content, cancellationToken);
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
                var response = await _httpClient.DeleteAsync(fullUrl, cancellationToken);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteAsync for endpoint: {Endpoint}", endpoint);
                throw;
            }
        }

        private string BuildUrl(string endpoint)
        {
            // اگر endpoint با / شروع شده باشد، آن را حذف می‌کنیم
            var cleanEndpoint = endpoint.StartsWith("/") ? endpoint[1..] : endpoint;
            return cleanEndpoint;
        }
    }
}
