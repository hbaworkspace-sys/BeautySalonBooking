namespace BeautySalonBooking.Maui.Common.Navigation;

public static class AppRoutes
{
    // Splash
    public const string Onboarding = "onboarding";

    // Authentication
    public const string Login = "auth/login";
    public const string Register = "auth/register";
    public const string Otp = "auth/otp";

    //Main
    public const string Main = "main";

    // Booking
    public const string ServiceSelection = "booking/service-selection";
    public const string OrganizationSelection = "booking/organization-selection";
    public const string StylistSelection = "booking/stylist-selection";
    public const string BookingConfirmation = "booking/confirmation";
    public const string BookingDateTimeSelection = "booking/date-time-selection";


    //Shell root
    public const string RegistrationSuccess = "registration-success";
    public const string BookingSuccess = "booking-success";

    //Profile
    public const string EditProfile = "profile/edit";
    public const string ChangeMobile = "profile/change-mobile";
    public const string Security = "profile/security";
    public const string NotificationSettings = "profile/notification-settings";
    public const string Privacy = "profile/privacy";
}