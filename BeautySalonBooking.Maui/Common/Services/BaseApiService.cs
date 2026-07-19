using BeautySalonBooking.Contracts.Common;
using System.Net.Http.Json;

namespace BeautySalonBooking.Maui.Common.Services
{
    public abstract class BaseApiService
    {
        protected readonly HttpClient HttpClient;

        protected BaseApiService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
        protected async Task<ApiResponse<TResponse>> SendAsync<TRequest, TResponse>(
            HttpMethod method,  string url, TRequest? body = default)
        {
            var request = new HttpRequestMessage(method, url);

            if (body != null)
            {
                request.Content = JsonContent.Create(body);
            }

            var response = await HttpClient.SendAsync(request);

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<TResponse>>();

            return result ?? new ApiResponse<TResponse>
            {
                IsSuccess = false,
                Message = "پاسخ دریافتی از سرور قابل پردازش نیست."
            };
        }
    }
}