using BeautySalonBooking.Contracts.Appointment.Requests;
using BeautySalonBooking.Contracts.Appointment.Responses;
using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.Maui.Features.Appointment.Services;

public interface IAppointmentApiService
{
    Task<ApiResponse_New<GetAppointmentsResponse>>
        GetAppointmentsAsync(
            GetAppointmentsRequest request,
            CancellationToken cancellationToken = default);
}