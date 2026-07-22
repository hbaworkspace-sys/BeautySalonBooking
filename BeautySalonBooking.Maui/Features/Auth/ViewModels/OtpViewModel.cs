using BeautySalonBooking.Contracts.Auth.Requests;
using BeautySalonBooking.Contracts.Auth.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Maui.Common.Enums;
using BeautySalonBooking.Maui.Features.AccountSetup;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Auth
{
    public partial class OtpViewModel : ObservableObject, IQueryAttributable
    {
        private readonly AuthApiService _authApiService;
        public OtpViewModel(AuthApiService authApi)
        {
            _authApiService = authApi;
        }

        [ObservableProperty]
        private string countryCode;

        [ObservableProperty]
        private string phoneNumber;

        [ObservableProperty]
        private string otpCode;

        [ObservableProperty]
        private bool isBusy = false;

        [ObservableProperty]
        private OtpPurpose otpPurposeType;

        [RelayCommand]
        private async Task VerifyOtpAsync()
        {
            if (IsBusy)
                return;

            if (!await ValidateInputAsync())
                return;

            try
            {
                IsBusy = true;
                ApiResponse<AuthResult> response = null;
                var request = new VerifyOtpRequest
                {
                    CountryCode = CountryCode,
                    MobileNumber = PhoneNumber,
                    OtpCode = OtpCode
                };

                switch (OtpPurposeType)
                {
                    case OtpPurpose.Register:
                        response = await _authApiService.ConfirmRegistrationAsync(request);
                        break;
                    case OtpPurpose.Login:
                        response = await _authApiService.ConfirmLoginAsync(request);
                        break;
                }

                if (response == null)
                {
                    await Shell.Current.DisplayAlert("خطا", "پاسخی از سرور دریافت نشد", "باشه");
                    return;
                }

                if (!response.IsSuccess)
                {
                    await Shell.Current.DisplayAlert("خطا", response.Message, "باشه");
                    return;
                }

                var data = response.Data;

                if (data != null)
                {
                    if (!string.IsNullOrWhiteSpace(data.AccessToken))
                        await SecureStorage.SetAsync("access_token", data.AccessToken);

                    if (!string.IsNullOrWhiteSpace(data.RefreshToken))
                        await SecureStorage.SetAsync("refresh_token", data.RefreshToken);

                    await SecureStorage.SetAsync("user_id", data.personId.ToString());
                }
                await Shell.Current.GoToAsync(nameof(ChooseAccountTypePage));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("خطا", "مشکلی در ارتباط با سرور رخ داد", "باشه");
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
                await Shell.Current.DisplayAlert("خطا", "کد تأیید را وارد کنید.", "باشه");
                return false;
            }

            if (OtpCode.Length != 6)
            {
                await Shell.Current.DisplayAlert("خطا", "کد تأیید باید ۶ رقم باشد.", "باشه");
                return false;
            }

            return true;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            PhoneNumber = (string)query["PhoneNumber"];
            CountryCode = (string)query["CountryCode"];
            OtpPurposeType = (OtpPurpose)query["OtpPurposeType"];
        }
    }
}