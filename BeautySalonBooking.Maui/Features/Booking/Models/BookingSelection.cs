using BeautySalonBooking.Maui.Components.DateAndTime.Models;
using BeautySalonBooking.Maui.Features.Branch.Models;
using BeautySalonBooking.Maui.Features.Service.Models;

namespace BeautySalonBooking.Maui.Features.Booking.Models;

public sealed class BookingSelection
{
    public ServiceModel? Service { get; set; }

    public BranchServiceOrganizationModel? Branch { get; set; }

    public BranchMemberServiceModel? Stylist { get; set; }

    public DateSelectionItem? Date { get; set; }

    public TimeSelectionItem? Time { get; set; }
}