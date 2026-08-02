using BeautySalonBooking.Contracts.Common;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace BeautySalonBooking.Maui.Common.Services;

public class BaseApiService
{
    protected readonly HttpClient HttpClient;
    protected readonly ILogger Logger;

    protected BaseApiService(HttpClient httpClient, ILogger logger)
    {
        HttpClient = httpClient;
        Logger = logger;
    }

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private async Task<ApiResponse_New<TResponse>> SendAsync<TRequest, TResponse>(
        HttpMethod method,
        string url,
        TRequest? body = default,
        CancellationToken cancellationToken = default)
    {
        try
        {
            using var request = new HttpRequestMessage(method, url);

            if (body is not null)
            {
                request.Content = JsonContent.Create(body);
            }

            var response = await HttpClient.SendAsync(
                request,
                cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync(cancellationToken);

                Logger.LogWarning(
                    "API Error ({StatusCode}) Url:{Url} Response:{Response}",
                    response.StatusCode,
                    url,
                    error);

                return new ApiResponse_New<TResponse>
                {
                    IsSuccess = false,
                    Message = string.IsNullOrWhiteSpace(error)
                        ? $"Server Error ({(int)response.StatusCode})"
                        : error
                };
            }

            var result =
                await response.Content.ReadFromJsonAsync<ApiResponse_New<TResponse>>(
                    JsonOptions,
                    cancellationToken);

            if (result == null)
            {
                Logger.LogError(
                    "Deserialize failed. Url:{Url}",
                    url);

                return new ApiResponse_New<TResponse>
                {
                    IsSuccess = false,
                    Message = "پاسخ سرور قابل پردازش نیست."
                };
            }

            return result;
        }

        catch (TaskCanceledException)
        {
            Logger.LogWarning("Request Timeout. Url:{Url}", url);

            return new ApiResponse_New<TResponse>
            {
                IsSuccess = false,
                Message = "درخواست لغو شد.",
                Code = 1,
            };
        }

        catch (HttpRequestException ex)
        {
            Logger.LogError(ex,
                "Http Error. Url:{Url}",
                url);

            return new ApiResponse_New<TResponse>
            {
                IsSuccess = false,
                Message = "ارتباط با سرور برقرار نشد."
            };
        }

        catch (Exception ex)
        {
            Logger.LogError(ex,
                "Unexpected Error. Url:{Url}",
                url);

            return new ApiResponse_New<TResponse>
            {
                IsSuccess = false,
                Message = "خطای غیرمنتظره رخ داده است."
            };
        }
    }

    protected Task<ApiResponse_New<TResponse>> GetAsync<TResponse>(
        string url,
        CancellationToken cancellationToken = default)
    {
        return SendAsync<object, TResponse>(
            HttpMethod.Get,
            url,
            null,
            cancellationToken);
    }

    protected Task<ApiResponse_New<TResponse>> PostAsync<TRequest, TResponse>(
        string url,
        TRequest body,
        CancellationToken cancellationToken = default)
    {
        return SendAsync<TRequest, TResponse>(
            HttpMethod.Post,
            url,
            body,
            cancellationToken);
    }

    protected Task<ApiResponse_New<TResponse>> PutAsync<TRequest, TResponse>(
        string url,
        TRequest body,
        CancellationToken cancellationToken = default)
    {
        return SendAsync<TRequest, TResponse>(
            HttpMethod.Put,
            url,
            body,
            cancellationToken);
    }

    protected Task<ApiResponse_New<TResponse>> DeleteAsync<TResponse>(
        string url,
        CancellationToken cancellationToken = default)
    {
        return SendAsync<object, TResponse>(
            HttpMethod.Delete,
            url,
            null,
            cancellationToken);
    }
}