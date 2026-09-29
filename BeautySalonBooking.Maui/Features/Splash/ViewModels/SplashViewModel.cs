using BeautySalonBooking.Contracts.Authentication.Requests;
using BeautySalonBooking.Contracts.Authentication.Services;
using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Auth.Models;
using BeautySalonBooking.Maui.Features.Auth.Services;
using BeautySalonBooking.Maui.Features.Main.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeautySalonBooking.Maui.Features.Splash.ViewModels;

public partial class SplashViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IAuthSessionService _authSessionService;
    private readonly IAuthApiService _authApiService;
    private readonly IUserContext _userContext;

    public SplashViewModel(
        INavigationService navigationService,
        IAuthSessionService authSessionService,
        IAuthApiService authApiService,
        IUserContext userContext)
    {
        _navigationService = navigationService;
        _authSessionService = authSessionService;
        _authApiService = authApiService;
        _userContext = userContext;
    }

    public async Task InitializeAsync(Task animationTask)
    {
        var loginCheckTask = CheckLoginStatusAsync();

        await Task.WhenAll(
            animationTask,
            loginCheckTask);
    }

    public async Task CheckLoginStatusAsync()
    {
        var session = await _authSessionService.GetAsync();

        if (session is null)
        {
            await _navigationService.GoToOnboardingAsync();
            return;
        }

        // Access Token منقضی شده → Refresh
        if (session.ExpiresAt <= DateTime.UtcNow)
        {
            var refreshRequest = new RefreshTokenRequest
            {
                AccessToken = session.AccessToken,
                RefreshToken = session.RefreshToken
            };

            var response = await _authApiService.RefreshTokenAsync(
                refreshRequest,
                session.AccessToken);

            var tokens = response.Payload?.Tokens;

            if (tokens is null ||
                string.IsNullOrWhiteSpace(tokens.AccessToken) ||
                string.IsNullOrWhiteSpace(tokens.RefreshToken))
            {
                await _authSessionService.ClearAsync();
                _userContext.Clear();

                await _navigationService.GoToOnboardingAsync();
                return;
            }

            await _authSessionService.SaveAsync(
                new AuthSession
                {
                    AccessToken = tokens.AccessToken,
                    RefreshToken = tokens.RefreshToken,
                    ExpiresAt = tokens.ExpiresAt
                });
        }

        // دریافت اطلاعات فعلی کاربر از سرور
        var userResponse =
            await _authApiService.GetCurrentUserAsync();

        if (!userResponse.IsSuccess || userResponse.Payload is null)
        {
            await _authSessionService.ClearAsync();
            _userContext.Clear();

            await _navigationService.GoToOnboardingAsync();
            return;
        }

        _userContext.SetUser(userResponse.Payload);

        await _navigationService.GoToMainAsync(MainTab.Dashboard);
    }
}