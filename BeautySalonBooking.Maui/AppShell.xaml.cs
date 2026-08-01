using BeautySalonBooking.Maui.Common.Navigation;
using BeautySalonBooking.Maui.Features.Auth;
using BeautySalonBooking.Maui.Features.Splash.Views;

namespace BeautySalonBooking.Maui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        //SPLASH
        Routing.RegisterRoute(AppRoutes.Onboarding, typeof(OnboardingPage));

        //AUTH
        Routing.RegisterRoute(AppRoutes.Login, typeof(LoginPage));
        Routing.RegisterRoute(AppRoutes.Register, typeof(RegisterPage));
        Routing.RegisterRoute(AppRoutes.Otp, typeof(OtpPage));
    }
}