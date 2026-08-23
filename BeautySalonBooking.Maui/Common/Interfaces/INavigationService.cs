using BeautySalonBooking.Maui.Features.Auth.Models;
using BeautySalonBooking.Maui.Features.Main.Models;

namespace BeautySalonBooking.Maui.Common.Interfaces;

public interface INavigationService
{
    //Back
    Task GoBackAsync();

    //Onboarding
    Task GoToOnboardingAsync();

    //Login-Register
    Task GoToLoginAsync();
    Task GoToRegisterAsync();
    Task GoToOtpAsync(OtpNavigationModel model);
    Task GoToRegistrationSuccessAsync();

    //Main
    Task GoToMainAsync(MainTab tab);

    //Booking
    Task GoToServiceSelectionAsync();
    Task GoToStylistSelectionAsync();
    Task GoToOrganizationSelectionAsync();
    Task GoToBookingDateTimeSelectionAsync();
    Task GoToBookingConfirmationAsync();
    Task GoToBookingSuccessAsync();
}