using BeautySalonBooking.Contracts.Booking.Availability.Requests;
using BeautySalonBooking.Contracts.Booking.Availability.Responses;
using BeautySalonBooking.Contracts.Booking.CreateBooking.Requests;
using BeautySalonBooking.Contracts.Booking.CreateBooking.Responses;
using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.Maui.Features.Booking.Services;

public interface IBookingApiService
{
    Task<ApiResponse_New<GetBookingAvailabilityResponse>>
        GetAvailabilityAsync(
            GetBookingAvailabilityRequest request,
            CancellationToken cancellationToken = default);


    Task<ApiResponse_New<CreateBookingResponse>>
    CreateBookingAsync(
        CreateBookingRequest request,
        CancellationToken cancellationToken = default);
}