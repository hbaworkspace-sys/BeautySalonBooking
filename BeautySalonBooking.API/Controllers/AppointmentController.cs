using BeautySalonBooking.Application.Appointment.Interfaces;
using BeautySalonBooking.Contracts.Appointment.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(
        IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAppointments(
        [FromQuery] GetAppointmentsRequest request,
        CancellationToken cancellationToken)
    {
        var result =
            await _appointmentService.GetAppointmentsAsync(
                request,
                cancellationToken);

        return Ok(result);
    }
}