using BeautySalonBooking.Application.Booking.Interfaces;
using BeautySalonBooking.Application.Booking.Services;
using BeautySalonBooking.Contracts.Booking.Availability.Requests;
using BeautySalonBooking.Contracts.Booking.CreateBooking.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(
        IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet("availability/by-member-service")]
    public async Task<IActionResult> GetAvailability(
        [FromQuery] GetBookingAvailabilityRequest request,
        CancellationToken cancellationToken)
    {
        var result =
            await _bookingService.GetAvailabilityAsync(
                request,
                cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking(
       [FromBody] CreateBookingRequest request,
       CancellationToken cancellationToken)
    {
        var result =
            await _bookingService.CreateBookingAsync(
                request,
                cancellationToken);

        return Ok(result);
    }
}