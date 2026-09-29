using BeautySalonBooking.Contracts.Authentication.Requests;
using BeautySalonBooking.Contracts.Authentication.Responses;
using BeautySalonBooking.Contracts.Authentication.Services;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Maui.Common.Enums;
using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Auth.Models;
using BeautySalonBooking.Maui.Features.Auth.Services;
using BeautySalonBooking.Maui.Features.Auth.Validators;
using BeautySalonBooking.Maui.Features.Main.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Auth.ViewModels;

public partial class OtpViewModel : ObservableObject, IQueryAttributable
{
    private readonly IAuthApiService _authApiService;
    private readonly IDialogService _dialogService;
    private readonly INavigationService _navigationService;
    private readonly IAuthSessionService _authSessionService;
    private readonly IUserContext _userContext;
    private readonly AuthValidator _authValidator;


    [ObservableProperty]
    private OtpNavigationModel? navigationModel;
    const int timeSecond = 120;

    public OtpViewModel(
        IAuthApiService authApiService,
        IDialogService dialogService,
        INavigationService navigationService,
        IAuthSessionService authSessionService,
        IUserContext userContext,
        AuthValidator authValidator)
    {
        _authApiService = authApiService;
        _dialogService = dialogService;
        _navigationService = navigationService;
        _authSessionService = authSessionService;
        _userContext = userContext;
        _authValidator = authValidator;
    }

    [ObservableProperty]
    private string otpCode = string.Empty;

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private int countdownSeconds = timeSecond;

    [ObservableProperty]
    private bool isCountdownRunning;

    [RelayCommand]
    private async Task VerifyOtpAsync()
    {
        if (IsBusy)
            return;

        if (NavigationModel == null)
        {
            await _dialogService.ShowErrorAsync("اطلاعات درخواست نامعتبر است.");
            return;
        }
        if (!await ValidateInputAsync())
            return;
        try
        {
            IsBusy = true;
            var request = new VerifyOtpRequest
            {
                MobileNumber = NavigationModel.MobileNumber,
                OtpCode = OtpCode
            };

            ApiResponse_New<AuthResult>? response = null;

            switch (NavigationModel.Purpose)
            {
                case OtpPurpose.Register:
                    response =
                        await _authApiService.ConfirmRegistrationAsync(request);
                    break;
                case OtpPurpose.Login:
                    response =
                        await _authApiService.ConfirmLoginAsync(request);
                    break;
            }

            if (response == null)
            {
                await _dialogService.ShowErrorAsync("پاسخی از سرور دریافت نشد.");
                return;
            }

            if (!response.IsSuccess)
            {
                await _dialogService.ShowErrorAsync(response.Message);
                return;
            }
            if (NavigationModel.Purpose == OtpPurpose.Login)
            {
                var tokens = response.Payload?.Tokens;
                var user = response.Payload?.User;

                if (tokens is null ||
                    string.IsNullOrWhiteSpace(tokens.AccessToken) ||
                    string.IsNullOrWhiteSpace(tokens.RefreshToken))
                {
                    await _dialogService.ShowErrorAsync(
                        "اطلاعات احراز هویت از سرور کامل دریافت نشد.");

                    return;
                }

                if (user is null)
                {
                    await _dialogService.ShowErrorAsync(
                        "اطلاعات کاربر از سرور دریافت نشد.");

                    return;
                }

                await _authSessionService.SaveAsync(
                    new AuthSession
                    {
                        AccessToken = tokens.AccessToken,
                        RefreshToken = tokens.RefreshToken,
                        ExpiresAt = tokens.ExpiresAt
                    });

                _userContext.SetUser(user);
            }
            switch (NavigationModel.Purpose)
            {
                case OtpPurpose.Register:
                    await _navigationService.GoToRegistrationSuccessAsync();
                    break;
                case OtpPurpose.Login:
                    await _navigationService.GoToMainAsync(MainTab.Dashboard);
                    break;
            }
        }
        catch (Exception)
        {
            await _dialogService.ShowErrorAsync("مشکلی در ارتباط با سرور رخ داد.");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task ResendOtpAsync()
    {
        if (IsBusy)
            return;

        if (NavigationModel == null)
        {
            await _dialogService.ShowErrorAsync("اطلاعات درخواست نامعتبر است.");
            return;
        }

        try
        {
            IsBusy = true;

            bool isSuccess = false;

            switch (NavigationModel.Purpose)
            {
                case OtpPurpose.Login:
                    var loginResult =
                        await _authApiService.RequestOtpForLoginAsync(
                            new LoginInitiateRequest
                            {
                                MobileNumber = NavigationModel.MobileNumber
                            });
                    isSuccess = loginResult.IsSuccess;
                    break;

                case OtpPurpose.Register:
                    var registerResult =
                        await _authApiService.RequestOtpForRegisterAsync(
                            new RegisterInitiateRequest
                            {
                                FirstName = NavigationModel.FirstName,
                                LastName = NavigationModel.LastName,
                                MobileNumber = NavigationModel.MobileNumber,
                                NationalCode = NavigationModel.NationalCode
                            });
                    isSuccess = registerResult.IsSuccess;
                    break;
            }
            if (isSuccess)
            {
                CountdownSeconds = timeSecond;
                IsCountdownRunning = true;
            }
            else
            {
                await _dialogService.ShowErrorAsync("ارسال مجدد کد انجام نشد.");
            }
        }
        catch (Exception)
        {
            await _dialogService.ShowErrorAsync("خطایی در ارسال مجدد کد رخ داد.");
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task<bool> ValidateInputAsync()
    {
        var error = _authValidator.ValidateOtp(OtpCode);

        if (error is null)
            return true;

        await _dialogService.ShowErrorAsync(error);
        return false;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("OtpNavigation", out var value))
        {
            NavigationModel = value as OtpNavigationModel;
        }

        CountdownSeconds = timeSecond;
        IsCountdownRunning = true;
    }

    [RelayCommand]
    private Task GoBackAsync()
    {
        return _navigationService.GoBackAsync();
    }
}