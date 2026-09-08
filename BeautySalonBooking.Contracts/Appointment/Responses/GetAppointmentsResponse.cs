using BeautySalonBooking.Contracts.Appointment.Dtos;

namespace BeautySalonBooking.Contracts.Appointment.Responses;

public sealed class GetAppointmentsResponse
{
    public IReadOnlyList<AppointmentDto> Appointments { get; init; } = [];
}