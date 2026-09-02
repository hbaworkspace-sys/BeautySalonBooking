using BeautySalonBooking.Contracts.Branch.BranchMemberService.Dtos;
using BeautySalonBooking.Contracts.Branch.BranchService.Dtos;
using BeautySalonBooking.Contracts.Service.Dtos;
using BeautySalonBooking.Maui.Components.DateAndTime.Models;

namespace BeautySalonBooking.Maui.Features.Booking.Models;

public sealed class BookingSelection
{
    public ServiceDto? Service { get; set; }

    public BranchServiceOrganizationDto? Branch { get; set; }

    public BranchMemberServiceDto? Stylist { get; set; }

    public DateSelectionItem? Date { get; set; }

    public TimeSelectionItem? Time { get; set; }
}