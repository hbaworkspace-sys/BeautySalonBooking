using BeautySalonBooking.Contracts.Appointment.Dtos;
using BeautySalonBooking.Contracts.Appointment.Enums;
using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Contracts.Branch.BranchService.Dtos;

namespace BeautySalonBooking.Maui.Features.Appointment.Models;

public sealed class AppointmentModel
{
    public long AppointmentId { get; init; }

    public string ServiceTitle { get; init; } = string.Empty;

    public string OrganizationTitle { get; init; } = string.Empty;

    public string BranchTitle { get; init; } = string.Empty;

    public string StylistName { get; init; } = string.Empty;

    public DateOnly Date { get; init; }

    public TimeOnly StartTime { get; init; }

    public TimeOnly EndTime { get; init; }

    public decimal TotalPrice { get; init; }

    public AppointmentStatus Status { get; init; }

    public PaymentStatus PaymentStatus { get; init; }

    public BranchMediaDto? BranchImage { get; init; }

    public UserMediaDto? StylistImage { get; init; }

    public ImageSource? BranchImageSource =>
        CreateImageSource(BranchImage?.Content);

    public ImageSource? StylistImageSource =>
        CreateImageSource(StylistImage?.Content);

    private static ImageSource? CreateImageSource(byte[]? content)
    {
        if (content is null || content.Length == 0)
            return null;

        return ImageSource.FromStream(
            () => new MemoryStream(content));
    }

    public static AppointmentModel FromDto(
        AppointmentDto dto)
    {
        return new AppointmentModel
        {
            AppointmentId = dto.AppointmentId,
            ServiceTitle = dto.ServiceTitle,
            OrganizationTitle = dto.OrganizationTitle,
            BranchTitle = dto.BranchTitle,
            StylistName = dto.StylistName,

            Date = dto.Date,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,

            TotalPrice = dto.TotalPrice,

            Status = dto.Status,
            PaymentStatus = dto.PaymentStatus,

            BranchImage = dto.BranchImage,
            StylistImage = dto.StylistImage
        };
    }
}