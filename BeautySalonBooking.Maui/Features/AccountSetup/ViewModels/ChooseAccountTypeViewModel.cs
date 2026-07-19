using BeautySalonBooking.Contracts.Common.Enums;
using BeautySalonBooking.Contracts.UserRoles.Requests;
using BeautySalonBooking.Maui.Features.Home;
using BeautySalonBooking.Maui.Features.UserRoles;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.AccountSetup
{
    public partial class ChooseAccountTypeViewModel : ObservableObject
    {
        private readonly UserRoleApiService _userRoleApiService;

        public ChooseAccountTypeViewModel(UserRoleApiService userRoleApiService)
        {
            _userRoleApiService = userRoleApiService;
        }

        [ObservableProperty]
        private bool isBusy;

        [RelayCommand]
        private async Task SelectRoleAsync(UserTypeDto role)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var userId = await SecureStorage.GetAsync("user_id");
                var result = await _userRoleApiService.AssignRoleAsync(Guid.Parse(userId),

                    new AssignRoleRequest
                    {
                        Role = role
                    });

                if (result.IsSuccess)
                {
                    switch (role)
                    {
                        case UserTypeDto.SalonOwner:
                            await Shell.Current.GoToAsync(nameof(SalonIntroductionPage));
                            break;

                        case UserTypeDto.Stylist:
                            await Shell.Current.GoToAsync(nameof(StylistIntroductionPage));
                            break;

                        case UserTypeDto.Customer:
                            await Shell.Current.GoToAsync(nameof(HomePage));
                            break;
                    }
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
    }
}
