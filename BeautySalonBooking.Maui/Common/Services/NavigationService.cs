using BeautySalonBooking.Maui.Common.Enums;
using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Common.Navigation;

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

    public Task GoToOtpAsync(string phoneNumber, OtpPurpose purpose)
    {
        return Shell.Current.GoToAsync(
            AppRoutes.Otp,
            new Dictionary<string, object>
            {
                ["PhoneNumber"] = phoneNumber,
                ["OtpPurposeType"] = purpose
            });
    }
}