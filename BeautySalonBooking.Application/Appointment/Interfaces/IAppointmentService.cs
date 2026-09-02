using BeautySalonBooking.Contracts.Appointment.Requests;
using BeautySalonBooking.Contracts.Appointment.Responses;
using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.Application.Appointment.Interfaces;

public interface IAppointmentService
{
    Task<ApiResponse_New<GetAppointmentsResponse>>
        GetAppointmentsAsync(
            GetAppointmentsRequest request,
            CancellationToken cancellationToken = default);
}