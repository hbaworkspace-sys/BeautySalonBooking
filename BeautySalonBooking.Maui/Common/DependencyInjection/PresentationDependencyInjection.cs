using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Common.Services;
using BeautySalonBooking.Maui.Features.Auth;
using BeautySalonBooking.Maui.Features.Auth.ViewModels;
using BeautySalonBooking.Maui.Features.Auth.Views;
using BeautySalonBooking.Maui.Features.Dashboard.ViewModels;
using BeautySalonBooking.Maui.Features.Dashboard.Views;
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

            services.AddTransient<LoginPage>();
            services.AddTransient<LoginViewModel>();

            services.AddTransient<OtpPage>();
            services.AddTransient<OtpViewModel>();

            services.AddTransient<RegisterPage>();
            services.AddTransient<RegisterViewModel>();

            services.AddTransient<DashboardPage>();
            services.AddTransient<DashboardViewModel>();

            services.AddTransient<RegistrationSuccessPage>();
            services.AddTransient<RegistrationSuccessViewModel>();

            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IDialogService, DialogService>();
            return services;
        }
    }
}