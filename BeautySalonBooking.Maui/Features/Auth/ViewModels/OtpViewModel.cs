using BeautySalonBooking.Contracts.Authentication.Requests;
using BeautySalonBooking.Contracts.Authentication.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Maui.Common.Enums;
using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Auth.Models;
using BeautySalonBooking.Maui.Features.Auth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Auth;

public partial class OtpViewModel : ObservableObject, IQueryAttributable
{
    private readonly IAuthApiService _authApiService;
    private readonly IDialogService _dialogService;
    private readonly INavigationService _navigationService;
    private OtpNavigationModel? _navigationModel;
    const int timeSecond = 120;

    public OtpViewModel(IAuthApiService authApiService, IDialogService dialogService, INavigationService navigationService)
    {
        _authApiService = authApiService;
        _dialogService = dialogService;
        _navigationService = navigationService;
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

        if (_navigationModel == null)
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
                MobileNumber = _navigationModel.MobileNumber,
                OtpCode = OtpCode
            };

            ApiResponse_New<AuthResult>? response = null;

            switch (_navigationModel.Purpose)
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

            var data = response.Payload;
            if (data != null)
            {
                // ذخیره Token ها در SecureStorage
                // await SecureStorage.SetAsync(...)
            }
            switch (_navigationModel.Purpose)
            {
                case OtpPurpose.Register:
                    await _navigationService.GoToRegistrationSuccessAsync();
                    break;
                case OtpPurpose.Login:
                    await _navigationService.GoToDashboardAsync();
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

        if (_navigationModel == null)
        {
            await _dialogService.ShowErrorAsync("اطلاعات درخواست نامعتبر است.");
            return;
        }

        try
        {
            IsBusy = true;

            bool isSuccess = false;

            switch (_navigationModel.Purpose)
            {
                case OtpPurpose.Login:
                    var loginResult =
                        await _authApiService.RequestOtpForLoginAsync(
                            new LoginInitiateRequest
                            {
                                MobileNumber = _navigationModel.MobileNumber
                            });
                    isSuccess = loginResult.IsSuccess;
                    break;

                case OtpPurpose.Register:
                    var registerResult =
                        await _authApiService.RequestOtpForRegisterAsync(
                            new RegisterInitiateRequest
                            {
                                FirstName = _navigationModel.FirstName,
                                LastName = _navigationModel.LastName,
                                MobileNumber = _navigationModel.MobileNumber,
                                NationalCode = _navigationModel.NationalCode
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
        if (string.IsNullOrWhiteSpace(OtpCode))
        {
            await _dialogService.ShowErrorAsync("کد تأیید را وارد کنید.");
            return false;
        }
        if (OtpCode.Length != 6)
        {
            await _dialogService.ShowErrorAsync("کد تأیید باید ۶ رقم باشد.");
            return false;
        }
        return true;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("OtpNavigation", out var value))
        {
            _navigationModel = value as OtpNavigationModel;
        }

        CountdownSeconds = timeSecond;
        IsCountdownRunning = true;
    }
}