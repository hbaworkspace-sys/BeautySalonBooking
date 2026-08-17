using BeautySalonBooking.Maui.Common.Navigation;
using BeautySalonBooking.Maui.Features.Auth;
using BeautySalonBooking.Maui.Features.Auth.Views;
using BeautySalonBooking.Maui.Features.Dashboard.Views;
using BeautySalonBooking.Maui.Features.Pages.Appointment;
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
        Routing.RegisterRoute(AppRoutes.RegistrationSuccess, typeof(RegistrationSuccessPage));

        //Dashboard
        Routing.RegisterRoute(AppRoutes.Dashboard, typeof(DashboardPage));

        //Appointment
        Routing.RegisterRoute(AppRoutes.Appointments, typeof(AppointmentsPage));
    }
}