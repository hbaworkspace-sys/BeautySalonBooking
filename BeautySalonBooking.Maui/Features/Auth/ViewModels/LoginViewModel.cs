using BeautySalonBooking.Contracts.Authentication.Requests;
using BeautySalonBooking.Maui.Common.Enums;
using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Auth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;

namespace BeautySalonBooking.Maui.Features.Auth;

public partial class LoginViewModel : ObservableObject
{
    private readonly IAuthApiService _authApiService;
    private readonly INavigationService _navigationService;
    private readonly IDialogService _dialogService;

    public LoginViewModel(IAuthApiService authApiService, INavigationService navigationService, IDialogService dialogService)
    {
        _authApiService = authApiService;
        _navigationService = navigationService;
        _dialogService = dialogService;
    }

    [ObservableProperty]
    private string phoneNumber = string.Empty;

    [ObservableProperty]
    private bool isBusy;

    [RelayCommand]
    private async Task LoginAsync()
    {
        if (IsBusy)
            return;

        if (!await ValidateInputAsync())
            return;

        try
        {
            IsBusy = true;

            var result = await _authApiService.RequestOtpForLoginAsync(
                new LoginInitiateRequest
                {
                    MobileNumber = PhoneNumber
                });

            var result1 = result;

            if (!result.IsSuccess)
            {
                await _dialogService.ShowErrorAsync(result.Message);
                return;
            }

            await _navigationService.GoToOtpAsync(PhoneNumber, OtpPurpose.Login);
        }
        catch (Exception ex)
        {
            await _dialogService.ShowErrorAsync(ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private Task GoToRegisterAsync()
    {
        return _navigationService.GoToRegisterAsync();
    }

    private static readonly HashSet<string> ValidPrefixes =
    [
        // MCI
        "910","911","912","913","914","915","916","917","918","919",
        "990","991","992","993","994",

        // Irancell
        "901","902","903","904","905",
        "930","933","935","936","937","938","939",

        // Rightel
        "920","921","922"
    ];
    private async Task<bool> ValidateInputAsync()
    {
        if (string.IsNullOrWhiteSpace(PhoneNumber))
        {
            await _dialogService.ShowErrorAsync("شماره موبایل را وارد کنید.");
            return false;
        }

        if (PhoneNumber.Length != 10)
        {
            await _dialogService.ShowErrorAsync("شماره موبایل باید ۱۰ رقم باشد.");
            return false;
        }

        if (!Regex.IsMatch(PhoneNumber, @"^9\d{9}$"))
        {
            await _dialogService.ShowErrorAsync("فرمت شماره موبایل نامعتبر است.");
            return false;
        }

        var prefix = PhoneNumber[..3];

        if (!ValidPrefixes.Contains(prefix))
        {
            await _dialogService.ShowErrorAsync("پیش‌شماره موبایل معتبر نیست.");
            return false;
        }

        return true;
    }
}