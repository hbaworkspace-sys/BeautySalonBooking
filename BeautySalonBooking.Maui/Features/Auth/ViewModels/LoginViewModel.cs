using BeautySalonBooking.Contracts.Auth.Requests;
using BeautySalonBooking.Maui.Common.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;

namespace BeautySalonBooking.Maui.Features.Auth
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly AuthApiService _authApiService;
        public LoginViewModel(AuthApiService authApi)
        {
            _authApiService = authApi;
        }

        [ObservableProperty]
        private string phoneNumber;

        [ObservableProperty]
        private bool isBusy = false;

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

                if (result.IsSuccess)
                {
                    await Shell.Current.GoToAsync(nameof(OtpPage), new Dictionary<string, object>
                    {
                        ["PhoneNumber"] = PhoneNumber,
                        ["CountryCode"] = "+98",
                        ["OtpPurposeType"] = OtpPurpose.Login
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
        private async Task GoToRegisterAsync()
        {
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }
        private static readonly HashSet<string> ValidPrefixes = new()
        {
            // MCI
            "910","911","912","913","914","915","916","917","918","919",
            "990","991","992","993","994",

            // Irancell
            "901","902","903","904","905",
            "930","933","935","936","937","938","939",

            // Rightel
            "920","921","922"
        };
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
            if (!Regex.IsMatch(PhoneNumber, @"^9\d{9}$"))
            {
                await Shell.Current.DisplayAlert("خطا", "فرمت شماره موبایل نامعتبر است.", "باشه");
                return false;
            }

            var prefix = PhoneNumber.Substring(0, 3);
            if (!ValidPrefixes.Contains(prefix))
            {
                await Shell.Current.DisplayAlert("خطا", "پیش‌شماره موبایل معتبر نیست.", "باشه");
                return false;
            }
            return true;
        }

    }
}