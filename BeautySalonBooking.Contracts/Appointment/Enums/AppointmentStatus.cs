namespace BeautySalonBooking.Contracts.Appointment.Enums;

public enum AppointmentStatus : byte
{
    Pending = 1,
    Confirmed = 2,
    CheckedIn = 3,
    InProgress = 4,
    Completed = 5,
    Cancelled = 6,
    Rejected = 7,
    NoShow = 8
}