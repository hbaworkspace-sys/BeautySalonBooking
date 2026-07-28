using BeautySalonBooking.Maui.Features.AccountSetup;
using BeautySalonBooking.Maui.Features.AdminDashboard;
using BeautySalonBooking.Maui.Features.AdminDashboard.ViewModels;
using BeautySalonBooking.Maui.Features.AdminDashboard.Views;
using BeautySalonBooking.Maui.Features.Auth;
using BeautySalonBooking.Maui.Features.Home;
using BeautySalonBooking.Maui.Features.Splash;
using BeautySalonBooking.Maui.Features.Splash.Views;

namespace BeautySalonBooking.Maui.Common.DependencyInjection
{
    public static class PresentationDependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddTransient<SplashPage>();
            services.AddTransient<SplashViewModel>();

            services.AddTransient<OnboardingPage>();

            services.AddTransient<HomePage>();
            services.AddTransient<HomeViewModel>();

            services.AddTransient<LoginPage>();
            services.AddTransient<LoginViewModel>();

            services.AddTransient<OtpPage>();
            services.AddTransient<OtpViewModel>();

            services.AddTransient<RegisterPage>();
            services.AddTransient<RegisterViewModel>();

            services.AddTransient<ChooseAccountTypePage>();
            services.AddTransient<ChooseAccountTypeViewModel>();

            services.AddTransient<SalonIntroductionPage>();
            services.AddTransient<SalonIntroductionViewModel>();

            services.AddTransient<StylistIntroductionPage>();
            services.AddTransient<StylistIntroductionViewModel>();

            services.AddTransient<AdminDashboardPage>();
            services.AddTransient<AdminDashboardViewModel>();

            services.AddTransient<SalonRequestsPage>();
            services.AddTransient<SalonRequestsViewModel>();

            return services;
        }
    }
}