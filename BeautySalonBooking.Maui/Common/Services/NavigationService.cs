using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Common.Navigation;
using BeautySalonBooking.Maui.Features.Auth.Models;
using BeautySalonBooking.Maui.Features.Main.Models;

namespace BeautySalonBooking.Maui.Common.Services;

public sealed class NavigationService : INavigationService
{
    #region Back
    public Task GoBackAsync()
    {
        return Shell.Current.GoToAsync("..");
    }
    #endregion

    #region Onboarding
    public Task GoToOnboardingAsync()
    {
        return Shell.Current.GoToAsync(AppRoutes.Onboarding);
    }
    #endregion

    #region Login-Register
    public Task GoToLoginAsync()
    {
        return Shell.Current.GoToAsync(AppRoutes.Login);
    }
    public async Task GoToLoginAsRootAsync()
    {
        await Shell.Current.GoToAsync(AppRoutes.Login);

        var navigationStack = Shell.Current.Navigation.NavigationStack;

        if (navigationStack.Count > 1)
        {
            await Shell.Current.Navigation.PopToRootAsync();
        }
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
    #endregion

    #region Booking
    public Task GoToServiceSelectionAsync()
    {
        return Shell.Current.GoToAsync(AppRoutes.ServiceSelection);
    }

    public Task GoToStylistSelectionAsync()
    {
        return Shell.Current.GoToAsync(AppRoutes.StylistSelection);
    }

    public Task GoToBranchSelectionAsync()
    {
        return Shell.Current.GoToAsync(AppRoutes.OrganizationSelection);
    }

    public Task GoToBookingDateTimeSelectionAsync()
    {
        return Shell.Current.GoToAsync(AppRoutes.BookingDateTimeSelection);
    }

    public Task GoToBookingConfirmationAsync()
    {
        return Shell.Current.GoToAsync(AppRoutes.BookingConfirmation);
    }
    #endregion

    #region Main
    public Task GoToRegistrationSuccessAsync()
    {
        return Shell.Current.GoToAsync($"//{AppRoutes.RegistrationSuccess}");
    }

    public Task GoToMainAsync(MainTab tab)
    {
        return Shell.Current.GoToAsync(
            $"//{AppRoutes.Main}",
            new Dictionary<string, object>
            {
                ["SelectedTab"] = tab
            });
    }

    public Task GoToBookingSuccessAsync()
    {
        return Shell.Current.GoToAsync($"//{AppRoutes.BookingSuccess}");
    }

    public Task GoToEditProfileAsync()
    {
        return Shell.Current.GoToAsync(AppRoutes.EditProfile);
    }

    public Task GoToChangeMobileAsync()
    {
        return Shell.Current.GoToAsync(AppRoutes.ChangeMobile);
    }

    public Task GoToSecurityAsync()
    {
        return Shell.Current.GoToAsync(AppRoutes.Security);
    }

    public Task GoToNotificationSettingsAsync()
    {
        return Shell.Current.GoToAsync(AppRoutes.NotificationSettings);
    }

    public Task GoToPrivacyAsync()
    {
        return Shell.Current.GoToAsync(AppRoutes.Privacy);
    }
    #endregion
}