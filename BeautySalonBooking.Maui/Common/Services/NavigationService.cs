using BeautySalonBooking.Contracts.Branch.BranchMemberService.Dtos;
using BeautySalonBooking.Contracts.Branch.BranchService.Dtos;
using BeautySalonBooking.Contracts.Service.Dtos;
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
    #endregion

    #region Main
    public Task GoToMainAsync(MainTab tab)
    {
        return Shell.Current.GoToAsync(
            $"//{AppRoutes.Main}",
            new Dictionary<string, object>
            {
                ["SelectedTab"] = tab
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

    public Task GoToBookingSuccessAsync()
    {
        return Shell.Current.GoToAsync(AppRoutes.BookingSuccess);
    }
    #endregion
}