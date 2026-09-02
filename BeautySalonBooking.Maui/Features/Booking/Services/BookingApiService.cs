using BeautySalonBooking.Contracts.Booking.Availability.Requests;
using BeautySalonBooking.Contracts.Booking.Availability.Responses;
using BeautySalonBooking.Contracts.Booking.CreateBooking.Requests;
using BeautySalonBooking.Contracts.Booking.CreateBooking.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Maui.Common.Services;
using BeautySalonBooking.Maui.Features.Booking.Constants;
using Microsoft.Extensions.Logging;

namespace BeautySalonBooking.Maui.Features.Booking.Services;

public sealed class BookingApiService
    : BaseApiService,
      IBookingApiService
{
    public BookingApiService(
        HttpClient httpClient,
        ILogger<BookingApiService> logger)
        : base(httpClient, logger)
    {
    }

    public Task<ApiResponse_New<GetBookingAvailabilityResponse>>
        GetAvailabilityAsync(
            GetBookingAvailabilityRequest request,
            CancellationToken cancellationToken = default)
    {
        var url =
            $"{BookingRoutes.AvailabilityByMemberService}" +
            $"?BranchMemberServiceId={request.BranchMemberServiceId}" +
            $"&FromDate={request.FromDate:yyyy-MM-dd}" +
            $"&ToDate={request.ToDate:yyyy-MM-dd}" +
            $"&SlotInterval={request.SlotInterval}";

        return GetAsync<GetBookingAvailabilityResponse>(
            url,
            cancellationToken);
    }

    public Task<ApiResponse_New<CreateBookingResponse>>
        CreateBookingAsync(
            CreateBookingRequest request,
            CancellationToken cancellationToken = default)
    {
        return PostAsync<CreateBookingRequest, CreateBookingResponse>(
            BookingRoutes.Create,
            request,
            cancellationToken);
    }
}