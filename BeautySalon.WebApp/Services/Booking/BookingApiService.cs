using BeautySalonBooking.Contracts.Booking.Availability.Requests;
using BeautySalonBooking.Contracts.Booking.Availability.Responses;
using BeautySalonBooking.Contracts.Booking.CreateBooking.Requests;
using BeautySalonBooking.Contracts.Booking.CreateBooking.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.WebApp.Interfaces.Booking;
using BeautySalonBooking.WebApp.Interfaces.Common;

namespace BeautySalonBooking.WebApp.Services.Booking;

public class BookingApiService : IBookingApiService
{
    private readonly IApiClient _apiClient;
    private readonly ILogger<BookingApiService> _logger;

    public BookingApiService(IApiClient apiClient, ILogger<BookingApiService> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    public async Task<ApiResponse_New<GetBookingAvailabilityResponse>> GetAvailabilityAsync(
        GetBookingAvailabilityRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var endpoint = $"api/booking/availability/by-member-service" +
                           $"?BranchMemberServiceId={request.BranchMemberServiceId}" +
                           $"&FromDate={request.FromDate:yyyy-MM-dd}" +
                           $"&ToDate={request.ToDate:yyyy-MM-dd}" +
                           $"&SlotInterval={request.SlotInterval}";

            return await _apiClient.GetPublicAsync<ApiResponse_New<GetBookingAvailabilityResponse>>(
                endpoint, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting availability");
            return new ApiResponse_New<GetBookingAvailabilityResponse>
            {
                IsSuccess = false,
                Message = "خطا در دریافت زمان‌های خالی"
            };
        }
    }


    public async Task<ApiResponse_New<CreateBookingResponse>> CreateBookingAsync(
    CreateBookingRequest request,
    CancellationToken cancellationToken = default)
    {
        try
        {
            // استفاده از PostAsync عادی (نه Public) تا توکن احراز هویت ارسال شود
            return await _apiClient.PostAsync<CreateBookingRequest, ApiResponse_New<CreateBookingResponse>>(
                "api/booking", request, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating booking");
            return new ApiResponse_New<CreateBookingResponse>
            {
                IsSuccess = false,
                Message = "خطا در ثبت نهایی رزرو"
            };
        }
    }
}