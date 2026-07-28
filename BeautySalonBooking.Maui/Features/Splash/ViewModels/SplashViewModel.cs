using CommunityToolkit.Mvvm.ComponentModel;

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
        }
    }
}