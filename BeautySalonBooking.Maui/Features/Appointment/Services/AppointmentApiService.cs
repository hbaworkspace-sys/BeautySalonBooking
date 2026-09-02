using BeautySalonBooking.Contracts.Appointment.Requests;
using BeautySalonBooking.Contracts.Appointment.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Maui.Common.Services;
using BeautySalonBooking.Maui.Features.Appointment.Constants;
using Microsoft.Extensions.Logging;

namespace BeautySalonBooking.Maui.Features.Appointment.Services;

public sealed class AppointmentApiService
    : BaseApiService,
      IAppointmentApiService
{
    public AppointmentApiService(
        HttpClient httpClient,
        ILogger<AppointmentApiService> logger)
        : base(httpClient, logger)
    {
    }

    public Task<ApiResponse_New<GetAppointmentsResponse>>
        GetAppointmentsAsync(
            GetAppointmentsRequest request,
            CancellationToken cancellationToken = default)
    {
        var url =
            $"{AppointmentRoutes.GetAppointments}" +
            $"?CustomerUserId={request.CustomerUserId}" +
            $"&ListType={(byte)request.ListType}";

        return GetAsync<GetAppointmentsResponse>(
            url,
            cancellationToken);
    }
}