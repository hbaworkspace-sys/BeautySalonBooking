using BeautySalonBooking.Maui.Features;
using BeautySalonBooking.Maui.Features.AccountSetup;
using BeautySalonBooking.Maui.Features.AdminDashboard;
using BeautySalonBooking.Maui.Features.AdminDashboard.Views;
using BeautySalonBooking.Maui.Features.Auth;
using BeautySalonBooking.Maui.Features.Home;
using BeautySalonBooking.Maui.Features.Salons;

namespace BeautySalonBooking.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //AUTH
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(OtpPage), typeof(OtpPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));

            //ACCOUNT SETUP
            Routing.RegisterRoute(nameof(ChooseAccountTypePage), typeof(ChooseAccountTypePage));
            Routing.RegisterRoute(nameof(SalonIntroductionPage), typeof(SalonIntroductionPage));
            Routing.RegisterRoute(nameof(StylistIntroductionPage), typeof(StylistIntroductionPage));

            //SALON
            Routing.RegisterRoute(nameof(SalonRegistrationPage), typeof(SalonRegistrationPage));
            Routing.RegisterRoute(nameof(SalonVerificationSuccessPage), typeof(SalonVerificationSuccessPage));

            //ADMIN DASHBOARD
            Routing.RegisterRoute(nameof(AdminDashboardPage), typeof(AdminDashboardPage));
            Routing.RegisterRoute(nameof(SalonRequestsPage), typeof(SalonRequestsPage));

            //HOME
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(test), typeof(test));
        }
    }
}