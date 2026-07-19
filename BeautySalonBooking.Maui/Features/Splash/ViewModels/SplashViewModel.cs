using BeautySalonBooking.Maui.Common.LocalStorage;
using BeautySalonBooking.Maui.Features.Auth;
using CommunityToolkit.Mvvm.ComponentModel;
using BeautySalonBooking.Maui.Features.AdminDashboard;

namespace BeautySalonBooking.Maui.Features.Splash
{
    public partial class SplashViewModel : ObservableObject
    {
        public bool IsLoggedIn { get; } = false;
        public SplashViewModel()
        {
            CheckLoginStatusAsync();
        }

        public async Task CheckLoginStatusAsync()
        {
            //var token = await AuthStorage.GetTokenAsync();

            //if (!string.IsNullOrEmpty(token))
            //    await Shell.Current.GoToAsync(nameof(AdminDashboardPage));
            //else
            await Shell.Current.GoToAsync(nameof(test));
        }
    }
}