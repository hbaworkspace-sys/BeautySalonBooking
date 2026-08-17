using BeautySalonBooking.Contracts.Authentication.Enums;
using BeautySalonBooking.Contracts.Authentication.Requests;
using BeautySalonBooking.Maui.Common.Enums;
using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Auth.Models;
using BeautySalonBooking.Maui.Features.Auth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Auth
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly IAuthApiService _authApiService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        public RegisterViewModel(IAuthApiService authApiService, INavigationService navigationService, IDialogService dialogService)
        {
            _authApiService = authApiService;
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        [ObservableProperty]
        private string firstName;

        [ObservableProperty]
        private string lastName;

        [ObservableProperty]
        private string phoneNumber;

        [ObservableProperty]
        private string nationalCode;

        [ObservableProperty]
        private bool isBusy = false;

        [RelayCommand]
        private async Task RegisterAsync()
        {
            if (IsBusy)
                return;

            if (!await ValidateInputAsync())
                return;

            try
            {
                IsBusy = true;

                var result = await _authApiService.RequestOtpForRegisterAsync(
                    new RegisterInitiateRequest
                    {
                        FirstName = FirstName,
                        LastName = LastName,
                        MobileNumber = PhoneNumber,
                        NationalCode = NationalCode,
                        Gender = Gender.Female
                    });
                if (result.IsSuccess)
                {
                    await _navigationService.GoToOtpAsync(
                         new OtpNavigationModel
                         {
                             FirstName = FirstName,
                             LastName = LastName,
                             MobileNumber = PhoneNumber,
                             Purpose = OtpPurpose.Register,
                         });
                }
                else
                {
                    await _dialogService.ShowErrorAsync(result.Message);
                }
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
        private Task GoToLoginAsync()
        {
            return _navigationService.GoToLoginAsync();
        }
        [RelayCommand]
        private Task GoBackAsync()
        {
            return _navigationService.GoBackAsync();
        }
        private async Task<bool> ValidateInputAsync()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                await _dialogService.ShowErrorAsync("نام را وارد کنید.");
                return false;
            }

            if (FirstName.Length > 50)
            {
                await _dialogService.ShowErrorAsync("نام نباید بیشتر از 50 کاراکتر باشد.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(LastName))
            {
                await _dialogService.ShowErrorAsync("نام خانوادگی را وارد کنید.");
                return false;
            }

            if (LastName.Length > 50)
            {
                await _dialogService.ShowErrorAsync("نام خانوادگی نباید بیشتر از 50 کاراکتر باشد.");
                return false;
            }

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

            if (NationalCode.Length != 10)
            {
                await _dialogService.ShowErrorAsync("کد ملی باید ۱۰ رقم باشد.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(NationalCode))
            {
                await _dialogService.ShowErrorAsync("کد ملی را وارد کنید.");
                return false;
            }
            return true;
        }
    }
}