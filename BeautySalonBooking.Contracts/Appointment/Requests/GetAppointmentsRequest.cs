using BeautySalonBooking.Contracts.Appointment.Enums;

namespace BeautySalonBooking.Contracts.Appointment.Requests;

public sealed class GetAppointmentsRequest
{
    public long CustomerUserId { get; init; }

    public AppointmentListType ListType { get; init; }
}