using BeautySalonBooking.Contracts.Authentication.Requests;
using BeautySalonBooking.Maui.Common.Enums;
using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Auth.Services;
using BeautySalonBooking.Maui.Features.Auth.Validators;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Auth.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IAuthApiService _authApiService;
    private readonly INavigationService _navigationService;
    private readonly IDialogService _dialogService;
    private readonly AuthValidator _authValidator;


    public LoginViewModel(
        IAuthApiService authApiService,
        INavigationService navigationService,
        IDialogService dialogService,
        AuthValidator authValidator)
    {
        _authApiService = authApiService;
        _navigationService = navigationService;
        _dialogService = dialogService;
        _authValidator = authValidator;
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

            if (!result.IsSuccess)
            {
                await _dialogService.ShowErrorAsync(result.Message);
                return;
            }

            await _navigationService.GoToOtpAsync(
                new Models.OtpNavigationModel
                {
                    MobileNumber = PhoneNumber,
                    Purpose = OtpPurpose.Login
                });
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

    [RelayCommand]
    private Task GoBackAsync()
    {
        return _navigationService.GoBackAsync();
    }

    private async Task<bool> ValidateInputAsync()
    {
        var error = _authValidator.ValidateLogin(PhoneNumber);

        if (error is null)
            return true;

        await _dialogService.ShowErrorAsync(error);
        return false;
    }
}