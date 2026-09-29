using BeautySalonBooking.Contracts.Authentication.Requests;
using BeautySalonBooking.Maui.Common.Enums;
using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Components.Input;
using BeautySalonBooking.Maui.Features.Auth.Models;
using BeautySalonBooking.Maui.Features.Auth.Services;
using BeautySalonBooking.Maui.Features.Auth.Validators;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GenderEnum = BeautySalonBooking.Contracts.Authentication.Enums.Gender;

namespace BeautySalonBooking.Maui.Features.Auth.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly IAuthApiService _authApiService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private readonly AuthValidator _authValidator;

        public RegisterViewModel(
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
        private string firstName;

        [ObservableProperty]
        private string lastName;

        [ObservableProperty]
        private string phoneNumber;

        [ObservableProperty]
        private string nationalCode;

        [ObservableProperty]
        private bool isBusy = false;

        [ObservableProperty]
        private GenderEnum gender;

        public RadioButtonOption[] GenderOptions { get; } =
        {
            new(GenderEnum.Male, "مرد"),
            new(GenderEnum.Female, "زن")
        };

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
                        Gender = Gender
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
            var error = _authValidator.ValidateRegister(
                FirstName,
                LastName,
                PhoneNumber,
                NationalCode,
                Gender);

            if (error is null)
                return true;

            await _dialogService.ShowErrorAsync(error);
            return false;
        }
    }
}