namespace BeautySalonBooking.Maui.Common.Navigation;

public static class AppRoutes
{
    // Splash
    public const string Onboarding = "onboarding";

    // Authentication
    public const string Login = "auth/login";
    public const string Register = "auth/register";
    public const string Otp = "auth/otp";
    public const string RegistrationSuccess = "auth/registration-success";

    //Dashboard
    public const string Dashboard = "dashboard";

    // Appointments
    public const string Appointments = "appointments";
    public const string AppointmentDetail = "appointments/detail";

    // Booking
    public const string Booking = "appointments/booking";
    public const string BookingService = "appointments/booking/service";
    public const string BookingDate = "appointments/booking/date";
    public const string BookingTime = "appointments/booking/time";
}