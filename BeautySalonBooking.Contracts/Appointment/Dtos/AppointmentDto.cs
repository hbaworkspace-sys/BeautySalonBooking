using BeautySalonBooking.Contracts.Appointment.Enums;

namespace BeautySalonBooking.Contracts.Appointment.Dtos;

public sealed class AppointmentDto
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
}