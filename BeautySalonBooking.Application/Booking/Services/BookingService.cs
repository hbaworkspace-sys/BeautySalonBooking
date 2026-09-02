using BeautySalonBooking.Application.Booking.Interfaces;
using BeautySalonBooking.Contracts.Booking.Availability.Dtos;
using BeautySalonBooking.Contracts.Booking.Availability.Requests;
using BeautySalonBooking.Contracts.Booking.Availability.Responses;
using BeautySalonBooking.Contracts.Booking.CreateBooking.Requests;
using BeautySalonBooking.Contracts.Booking.CreateBooking.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Domain.AppointmentAggregate.Repositories;
using BeautySalonBooking.Domain.Base.UnitOfWork;
using BeautySalonBooking.Domain.SchedulingAggregate.Queries;
using BeautySalonBooking.Domain.SchedulingAggregate.Services;
using AppointmentEntity =
    BeautySalonBooking.Domain.AppointmentAggregate.Entities.Appointment;

namespace BeautySalonBooking.Application.Booking.Services;

public sealed class BookingService
    : IBookingService
{
    private readonly IBookingAvailabilityQuery _bookingAvailabilityQuery;
    private readonly AvailabilityCalculator _availabilityCalculator;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BookingService(
        IBookingAvailabilityQuery bookingAvailabilityQuery,
        AvailabilityCalculator availabilityCalculator,
        IAppointmentRepository appointmentRepository,
        IUnitOfWork unitOfWork)
    {
        _bookingAvailabilityQuery = bookingAvailabilityQuery;
        _availabilityCalculator = availabilityCalculator;
        _appointmentRepository = appointmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse_New<GetBookingAvailabilityResponse>>
        GetAvailabilityAsync(
            GetBookingAvailabilityRequest request,
            CancellationToken cancellationToken = default)
    {
        if (request.BranchMemberServiceId <= 0)
        {
            return new ApiResponse_New<GetBookingAvailabilityResponse>
            {
                IsSuccess = false,
                Code = 400,
                Message = "شناسه سرویس عضو شعبه معتبر نیست."
            };
        }

        if (request.FromDate > request.ToDate)
        {
            return new ApiResponse_New<GetBookingAvailabilityResponse>
            {
                IsSuccess = false,
                Code = 400,
                Message = "بازه تاریخ معتبر نیست."
            };
        }

        if (request.SlotInterval <= TimeSpan.Zero)
        {
            return new ApiResponse_New<GetBookingAvailabilityResponse>
            {
                IsSuccess = false,
                Code = 400,
                Message = "فاصله زمانی Slot معتبر نیست."
            };
        }

        var availability =
            await _bookingAvailabilityQuery.GetAsync(
                request.BranchMemberServiceId,
                request.FromDate,
                request.ToDate,
                cancellationToken);

        if (availability is null)
        {
            return new ApiResponse_New<GetBookingAvailabilityResponse>
            {
                IsSuccess = false,
                Code = 404,
                Message = "اطلاعات زمان‌بندی موردنظر یافت نشد."
            };
        }

        var availableDates =
            _availabilityCalculator.Calculate(
                availability,
                request.FromDate,
                request.ToDate,
                request.SlotInterval);

        var response = new GetBookingAvailabilityResponse
        {
            Dates = availableDates
                .Select(date => new AvailableDateDto
                {
                    Date = date.Date,

                    Slots = date.Slots
                        .Select(slot => new AvailableTimeSlotDto
                        {
                            StartTime = slot.StartTime,
                            EndTime = slot.EndTime
                        })
                        .ToList()
                })
                .ToList()
        };

        return new ApiResponse_New<GetBookingAvailabilityResponse>
        {
            IsSuccess = true,
            Code = 200,
            Payload = response
        };
    }

    public async Task<ApiResponse_New<CreateBookingResponse>>
        CreateBookingAsync(
            CreateBookingRequest request,
            CancellationToken cancellationToken = default)
    {
        // ============================
        // Request Validation
        // ============================

        if (request.CustomerUserId <= 0)
        {
            return new ApiResponse_New<CreateBookingResponse>
            {
                IsSuccess = false,
                Code = 400,
                Message = "شناسه کاربر معتبر نیست."
            };
        }

        if (request.BranchMemberServiceId <= 0)
        {
            return new ApiResponse_New<CreateBookingResponse>
            {
                IsSuccess = false,
                Code = 400,
                Message = "شناسه سرویس عضو شعبه معتبر نیست."
            };
        }

        await _unitOfWork.BeginTransactionAsync(
            cancellationToken);

        try
        {
            // ============================
            // Serialize booking attempts
            // for same member/service + date
            // ============================

            await _appointmentRepository
                .AcquireBookingLockAsync(
                    request.BranchMemberServiceId,
                    request.Date,
                    cancellationToken);

            // ============================
            // Re-check Availability
            // ============================

            var availability =
                await _bookingAvailabilityQuery.GetAsync(
                    request.BranchMemberServiceId,
                    request.Date,
                    request.Date,
                    cancellationToken);

            if (availability is null)
            {
                await _unitOfWork.RollbackAsync(
                    cancellationToken);

                return new ApiResponse_New<CreateBookingResponse>
                {
                    IsSuccess = false,
                    Code = 404,
                    Message = "اطلاعات زمان‌بندی یافت نشد."
                };
            }

            // ============================
            // Validate Duration
            // ============================

            if (availability.ServiceDuration <= TimeSpan.Zero)
            {
                await _unitOfWork.RollbackAsync(
                    cancellationToken);

                return new ApiResponse_New<CreateBookingResponse>
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = "مدت زمان سرویس معتبر نیست."
                };
            }

            // ============================
            // Calculate EndTime
            // ============================

            var endTime =
                request.StartTime.Add(
                    availability.ServiceDuration,
                    out var wrappedDays);

            if (wrappedDays != 0)
            {
                await _unitOfWork.RollbackAsync(
                    cancellationToken);

                return new ApiResponse_New<CreateBookingResponse>
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = "بازه زمانی رزرو معتبر نیست."
                };
            }

            // ============================
            // Recalculate Availability
            //
            // This validates:
            // Schedule
            // Working Hours
            // Exceptions
            // TimeOff
            // Holidays
            // Existing Appointments
            // ============================

            var availableDates =
                _availabilityCalculator.Calculate(
                    availability,
                    request.Date,
                    request.Date,
                    TimeSpan.FromMinutes(15));

            var availableDate =
                availableDates.FirstOrDefault(
                    x => x.Date == request.Date);

            if (availableDate is null)
            {
                await _unitOfWork.RollbackAsync(
                    cancellationToken);

                return new ApiResponse_New<CreateBookingResponse>
                {
                    IsSuccess = false,
                    Code = 409,
                    Message = "در این تاریخ زمان قابل رزروی وجود ندارد."
                };
            }

            // ============================
            // Validate Requested Slot
            // ============================

            var requestedSlot =
                availableDate.Slots.FirstOrDefault(
                    x =>
                        x.StartTime == request.StartTime &&
                        x.EndTime == endTime);

            if (requestedSlot is null)
            {
                await _unitOfWork.RollbackAsync(
                    cancellationToken);

                return new ApiResponse_New<CreateBookingResponse>
                {
                    IsSuccess = false,
                    Code = 409,
                    Message = "این زمان دیگر قابل رزرو نیست."
                };
            }

            // ============================
            // Explicit Conflict Check
            // ============================

            var hasConflict =
                await _appointmentRepository.HasConflictAsync(
                    request.BranchMemberServiceId,
                    request.Date,
                    requestedSlot.StartTime,
                    requestedSlot.EndTime,
                    cancellationToken);

            if (hasConflict)
            {
                await _unitOfWork.RollbackAsync(
                    cancellationToken);

                return new ApiResponse_New<CreateBookingResponse>
                {
                    IsSuccess = false,
                    Code = 409,
                    Message = "این زمان قبلاً رزرو شده است."
                };
            }

            // ============================
            // Create Domain Entity
            // ============================

            var appointment =
                AppointmentEntity.Create(
                    request.CustomerUserId,
                    request.BranchMemberServiceId,
                    request.Date,
                    requestedSlot.StartTime,
                    requestedSlot.EndTime,
                    availability.ServicePrice,
                    request.Note);

            // ============================
            // Persist
            // ============================

            await _appointmentRepository.AddAsync(
                appointment,
                cancellationToken);

            // ============================
            // Commit
            // ============================

            await _unitOfWork.CommitAsync(
                cancellationToken);

            // ============================
            // Response
            // ============================

            return new ApiResponse_New<CreateBookingResponse>
            {
                IsSuccess = true,
                Code = 200,
                Payload = new CreateBookingResponse
                {
                    AppointmentId = appointment.Id,

                    ServiceTitle =
        availability.ServiceTitle,

                    OrganizationTitle =
        availability.OrganizationTitle,

                    BranchTitle =
        availability.BranchTitle,

                    BranchDescription =
        availability.BranchDescription,

                    StylistName =
        availability.StylistName,

                    Date =
        appointment.Date,

                    StartTime =
        appointment.StartTime,

                    EndTime =
        appointment.EndTime,

                    Duration =
        availability.ServiceDuration,

                    TotalPrice =
        availability.ServicePrice
                }
            };
        }
        catch
        {
            await _unitOfWork.RollbackAsync(
                cancellationToken);

            throw;
        }
    }
}