using BeautySalonBooking.Application.Appointment.Interfaces;
using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Contracts.Appointment.Dtos;
using BeautySalonBooking.Contracts.Appointment.Enums;
using BeautySalonBooking.Contracts.Appointment.Requests;
using BeautySalonBooking.Contracts.Appointment.Responses;
using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Contracts.Branch.BranchService.Dtos;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Domain.AppointmentAggregate.Queries;

using ContractAppointmentStatus =
    BeautySalonBooking.Contracts.Appointment.Enums.AppointmentStatus;

using ContractPaymentStatus =
    BeautySalonBooking.Contracts.Appointment.Enums.PaymentStatus;

namespace BeautySalonBooking.Application.Appointment.Services;

public sealed class AppointmentService
    : IAppointmentService
{
    private readonly IAppointmentQuery _appointmentQuery;
    private readonly ICurrentUserService _currentUserService;

    public AppointmentService(
        ICurrentUserService currentUserService,
        IAppointmentQuery appointmentQuery)
    {
        _appointmentQuery = appointmentQuery;
        _currentUserService = currentUserService;
    }

    public async Task<ApiResponse_New<GetAppointmentsResponse>>
        GetAppointmentsAsync(
            GetAppointmentsRequest request,
            CancellationToken cancellationToken = default)
    {
        if (_currentUserService.UserId <= 0)
        {
            return new ApiResponse_New<GetAppointmentsResponse>
            {
                IsSuccess = false,
                Code = 400,
                Message = "شناسه کاربر معتبر نیست."
            };
        }

        var appointments =
            request.ListType switch
            {
                AppointmentListType.All =>
                    await _appointmentQuery.GetAllAsync(
                        _currentUserService.UserId,
                        cancellationToken),

                AppointmentListType.Upcoming =>
                    await _appointmentQuery.GetUpcomingAsync(
                        _currentUserService.UserId,
                        cancellationToken),

                AppointmentListType.History =>
                    await _appointmentQuery.GetHistoryAsync(
                       _currentUserService.UserId,
                        cancellationToken),
                _ => null
            };

        if (appointments is null)
        {
            return new ApiResponse_New<GetAppointmentsResponse>
            {
                IsSuccess = false,
                Code = 400,
                Message = "نوع لیست رزرو نامعتبر است."
            };
        }

        var response = new GetAppointmentsResponse
        {
            Appointments =
                appointments
                    .Select(x => new AppointmentDto
                    {
                        AppointmentId = x.AppointmentId,
                        ServiceTitle = x.ServiceTitle,
                        OrganizationTitle = x.OrganizationTitle,
                        BranchTitle = x.BranchTitle,
                        StylistName = x.StylistName,

                        BranchImage = x.BranchImage is null
                        ? null
                        : new BranchMediaDto
                        {
                            Content = x.BranchImage
                        },

                        StylistImage = x.StylistImage is null
                        ? null
                        : new UserMediaDto
                        {
                            Content = x.StylistImage
                        },
                        Date = x.Date,
                        StartTime = x.StartTime,
                        EndTime = x.EndTime,
                        TotalPrice = x.TotalPrice,

                        Status = (ContractAppointmentStatus)x.Status,
                        PaymentStatus = (ContractPaymentStatus)x.PaymentStatus
                    })
                    .ToList()
        };

        return new ApiResponse_New<GetAppointmentsResponse>
        {
            IsSuccess = true,
            Code = 200,
            Payload = response
        };
    }
}