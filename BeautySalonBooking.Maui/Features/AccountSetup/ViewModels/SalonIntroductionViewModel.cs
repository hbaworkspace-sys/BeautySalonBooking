using BeautySalonBooking.Maui.Features.Salons;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.AccountSetup
{
    public partial class SalonIntroductionViewModel : ObservableObject
    {
        [RelayCommand]
        private async Task StartSalonRegistrationAsync()
        {
            await Shell.Current.GoToAsync(nameof(SalonRegistrationPage));
        }
    }
}