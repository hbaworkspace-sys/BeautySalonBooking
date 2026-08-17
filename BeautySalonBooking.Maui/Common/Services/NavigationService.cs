using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Common.Navigation;
using BeautySalonBooking.Maui.Features.Auth.Models;

namespace BeautySalonBooking.Maui.Common.Services;

public sealed class NavigationService : INavigationService
{
    public Task GoBackAsync()
    {
        return Shell.Current.GoToAsync("..");
    }

    public Task GoToOnboardingAsync()
    {
        return Shell.Current.GoToAsync(AppRoutes.Onboarding);
    }

    public Task GoToLoginAsync()
    {
        return Shell.Current.GoToAsync(AppRoutes.Login);
    }

    public Task GoToRegisterAsync()
    {
        return Shell.Current.GoToAsync(AppRoutes.Register);
    }

    public Task GoToOtpAsync(OtpNavigationModel model)
    {
        return Shell.Current.GoToAsync(AppRoutes.Otp, new Dictionary<string, object>
        {
            ["OtpNavigation"] = model
        });
    }

    public Task GoToRegistrationSuccessAsync()
    {
        return Shell.Current.GoToAsync(AppRoutes.RegistrationSuccess);
    }

    public Task GoToDashboardAsync()
    {
        return Shell.Current.GoToAsync(AppRoutes.Dashboard);
    }

    public Task GoToAppointmentsAsyn()
    {
        return Shell.Current.GoToAsync(AppRoutes.Appointments);
    }
}