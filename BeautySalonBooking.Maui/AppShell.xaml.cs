using BeautySalonBooking.Maui.Common.Navigation;
using BeautySalonBooking.Maui.Features.Auth;
using BeautySalonBooking.Maui.Features.Auth.Views;
using BeautySalonBooking.Maui.Features.Booking.Views;
using BeautySalonBooking.Maui.Features.Main.Views;
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

        //BOOKING
        Routing.RegisterRoute(AppRoutes.ServiceSelection, typeof(ServiceSelectionPage));
        Routing.RegisterRoute(AppRoutes.OrganizationSelection, typeof(OrganizationSelectionPage));
        Routing.RegisterRoute(AppRoutes.StylistSelection, typeof(StylistSelectionPage));
        Routing.RegisterRoute(AppRoutes.BookingDateTimeSelection, typeof(DateTimeSelectionPage));
        Routing.RegisterRoute(AppRoutes.BookingConfirmation, typeof(ConfirmationPage));
        Routing.RegisterRoute(AppRoutes.BookingSuccess, typeof(SuccessPage));
    }
}