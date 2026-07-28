using BeautySalonBooking.Contracts.Auth.Requests;
using BeautySalonBooking.Maui.Common.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Auth
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly AuthApiService _authApiService;
        public RegisterViewModel(AuthApiService authApi)
        {
            _authApiService = authApi;
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
                        MobileNumber = PhoneNumber
                    });

                if (result.IsSuccess)
                {
                    await Shell.Current.GoToAsync(nameof(OtpPage), new Dictionary<string, object>
                    {
                        ["PhoneNumber"] = PhoneNumber,
                        ["OtpPurposeType"] = OtpPurpose.Register
                    });
                }
                else
                {
                    await Shell.Current.DisplayAlert("خطا", result.Message, "باشه");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("خطا", ex.Message, "باشه");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task GoToLoginAsync()
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
        private async Task<bool> ValidateInputAsync()
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                await Shell.Current.DisplayAlert("خطا", "شماره موبایل را وارد کنید.", "باشه");
                return false;
            }

            if (PhoneNumber.Length != 10)
            {
                await Shell.Current.DisplayAlert("خطا", "شماره موبایل باید ۱۰ رقم باشد.", "باشه");
                return false;
            }

            if (string.IsNullOrWhiteSpace(FirstName))
            {
                await Shell.Current.DisplayAlert("خطا", "نام را وارد کنید.", "باشه");
                return false;
            }

            if (FirstName.Length > 50)
            {
                await Shell.Current.DisplayAlert("خطا", "نام نباید بیشتر از 50 کاراکتر باشد.", "باشه");
                return false;
            }

            if (string.IsNullOrWhiteSpace(LastName))
            {
                await Shell.Current.DisplayAlert("خطا", "نام خانوادگی را وارد کنید.", "باشه");
                return false;
            }

            if (LastName.Length > 50)
            {
                await Shell.Current.DisplayAlert("خطا", "نام خانوادگی نباید بیشتر از 50 کاراکتر باشد.", "باشه");
                return false;
            }

            return true;
        }
    }
}