using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Common.Services;
using BeautySalonBooking.Maui.Features.Appointment.ViewModels;
using BeautySalonBooking.Maui.Features.Appointment.Views;
using BeautySalonBooking.Maui.Features.Auth.ViewModels;
using BeautySalonBooking.Maui.Features.Auth.Views;
using BeautySalonBooking.Maui.Features.Booking.ViewModels;
using BeautySalonBooking.Maui.Features.Booking.Views;
using BeautySalonBooking.Maui.Features.Dashboard.ViewModels;
using BeautySalonBooking.Maui.Features.Dashboard.Views;
using BeautySalonBooking.Maui.Features.Main.ViewModels;
using BeautySalonBooking.Maui.Features.Main.Views;
using BeautySalonBooking.Maui.Features.Splash.ViewModels;
using BeautySalonBooking.Maui.Features.Splash.Views;

namespace BeautySalonBooking.Maui.Common.DependencyInjection;

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

        services.AddTransient<DashboardView>();
        services.AddTransient<DashboardViewModel>();

        services.AddTransient<RegistrationSuccessPage>();
        services.AddTransient<RegistrationSuccessViewModel>();

        services.AddTransient<AppointmentsView>();
        services.AddTransient<AppointmentsViewModel>();

        services.AddSingleton<MainPage>();
        services.AddSingleton<MainViewModel>();

        services.AddTransient<ServiceSelectionPage>();
        services.AddTransient<ServiceSelectionViewModel>();

        services.AddTransient<OrganizationSelectionPage>();
        services.AddTransient<OrganizationSelectionViewModel>();

        services.AddTransient<StylistSelectionPage>();
        services.AddTransient<StylistSelectionViewModel>();

        services.AddTransient<DateTimeSelectionPage>();
        services.AddTransient<DateTimeSelectionViewModel>();

        services.AddTransient<ConfirmationPage>();
        services.AddTransient<ConfirmationViewModel>();

        services.AddTransient<SuccessPage>();
        services.AddTransient<SuccessViewModel>();

        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<IDialogService, DialogService>();
        services.AddSingleton<IViewService, ViewService>();

        return services;
    }
}