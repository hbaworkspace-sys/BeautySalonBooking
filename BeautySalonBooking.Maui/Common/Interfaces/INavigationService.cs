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
    Task GoToLoginAsRootAsync();
    Task GoToRegisterAsync();
    Task GoToOtpAsync(OtpNavigationModel model);


    //Booking
    Task GoToServiceSelectionAsync();
    Task GoToStylistSelectionAsync();
    Task GoToBranchSelectionAsync();
    Task GoToBookingDateTimeSelectionAsync();
    Task GoToBookingConfirmationAsync();


    //shell root
    Task GoToRegistrationSuccessAsync();
    Task GoToMainAsync(MainTab tab);
    Task GoToBookingSuccessAsync();


    //profil
    Task GoToEditProfileAsync();
    Task GoToChangeMobileAsync();
    Task GoToSecurityAsync();
    Task GoToNotificationSettingsAsync();
    Task GoToPrivacyAsync();
}