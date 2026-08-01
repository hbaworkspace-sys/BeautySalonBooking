using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Common.Services;
using BeautySalonBooking.Maui.Features.Auth;
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


            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IDialogService, DialogService>();
            return services;
        }
    }
}