using BeautySalonBooking.Maui.Common.Enums;

namespace BeautySalonBooking.Maui.Common.Interfaces;

public interface INavigationService
{
    Task GoBackAsync();
    Task GoToOnboardingAsync();
    Task GoToLoginAsync();
    Task GoToRegisterAsync();
    Task GoToOtpAsync(string phoneNumber, OtpPurpose purpose);

}