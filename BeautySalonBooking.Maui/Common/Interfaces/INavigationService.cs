using BeautySalonBooking.Maui.Common.Enums;
using BeautySalonBooking.Maui.Features.Auth.Models;

namespace BeautySalonBooking.Maui.Common.Interfaces;

public interface INavigationService
{
    Task GoBackAsync();
    Task GoToOnboardingAsync();
    Task GoToLoginAsync();
    Task GoToRegisterAsync();
    Task GoToOtpAsync(OtpNavigationModel model);
    Task GoToRegistrationSuccessAsync();
    Task GoToDashboardAsync();
}