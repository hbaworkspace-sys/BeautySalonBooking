using BeautySalonBooking.Contracts.Authentication.Requests;
using BeautySalonBooking.Contracts.Authentication.Responses;
using BeautySalonBooking.Contracts.Authentication.Services;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Maui.Features.Auth.Constants;
using BeautySalonBooking.Maui.Features.Auth.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace BeautySalonBooking.Maui.Common.Handlers;

public sealed class AuthTokenHandler : DelegatingHandler
{
    private readonly IAuthSessionService _authSessionService;
    private readonly IHttpClientFactory _httpClientFactory;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public AuthTokenHandler(
        IAuthSessionService authSessionService,
        IHttpClientFactory httpClientFactory)
    {
        _authSessionService = authSessionService;
        _httpClientFactory = httpClientFactory;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // 1. دریافت Session فعلی
        var session = await _authSessionService.GetAsync();

        // 2. اضافه کردن Access Token به درخواست
        if (request.Headers.Authorization is null &&
            session is not null &&
            !string.IsNullOrWhiteSpace(session.AccessToken))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    session.AccessToken);
        }

        // 3. ارسال درخواست اصلی
        var response = await base.SendAsync(
            request,
            cancellationToken);

        // 4. اگر 401 نبود، همان پاسخ را برگردان
        if (response.StatusCode != HttpStatusCode.Unauthorized)
        {
            return response;
        }

        // 5. Access Token احتمالاً منقضی شده
        response.Dispose();

        // 6. Refresh Token
        var refreshSucceeded = await RefreshTokenAsync(
            session,
            cancellationToken);

        if (!refreshSucceeded)
        {
            return new HttpResponseMessage(
                HttpStatusCode.Unauthorized);
        }

        // 7. دریافت Session جدید
        var newSession = await _authSessionService.GetAsync();

        if (newSession is null ||
            string.IsNullOrWhiteSpace(newSession.AccessToken))
        {
            return new HttpResponseMessage(
                HttpStatusCode.Unauthorized);
        }

        // 8. قرار دادن Access Token جدید روی درخواست
        request.Headers.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                newSession.AccessToken);

        // 9. ارسال مجدد درخواست اصلی
        return await base.SendAsync(
            request,
            cancellationToken);
    }

    private async Task<bool> RefreshTokenAsync(
        AuthSession? session,
        CancellationToken cancellationToken)
    {
        if (session is null ||
            string.IsNullOrWhiteSpace(session.AccessToken) ||
            string.IsNullOrWhiteSpace(session.RefreshToken))
        {
            return false;
        }

        var client =
            _httpClientFactory.CreateClient("RefreshTokenClient");

        var request = new RefreshTokenRequest
        {
            AccessToken = session.AccessToken,
            RefreshToken = session.RefreshToken
        };

        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Post,
            AuthRoutes.RefreshToken);

        httpRequest.Headers.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                session.AccessToken);

        httpRequest.Content =
            JsonContent.Create(request);

        using var response = await client.SendAsync(
            httpRequest,
            cancellationToken);

        var responseBody =
    await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        var result =
            await response.Content.ReadFromJsonAsync<
                ApiResponse_New<AuthResult>>(
                    JsonOptions,
                    cancellationToken);

        var tokens = result?.Payload?.Tokens;

        if (tokens is null ||
            string.IsNullOrWhiteSpace(tokens.AccessToken) ||
            string.IsNullOrWhiteSpace(tokens.RefreshToken))
        {
            return false;
        }

        // 10. ذخیره Tokenهای جدید
        await _authSessionService.SaveAsync(
            new AuthSession
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken,
                ExpiresAt = tokens.ExpiresAt
            });

        return true;
    }
}