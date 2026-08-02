using BeautySalonBooking.Maui.Common.Enums;
namespace BeautySalonBooking.Maui.Features.Auth.Models;

public class OtpNavigationModel
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string MobileNumber { get; set; }
    public OtpPurpose Purpose { get; set; }
    public string? NationalCode { get; set; }
}