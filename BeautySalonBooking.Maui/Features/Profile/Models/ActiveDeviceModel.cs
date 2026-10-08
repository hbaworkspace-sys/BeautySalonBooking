namespace BeautySalonBooking.Maui.Features.Profile.Models;

public class ActiveDeviceModel
{
    public string DeviceName { get; set; } = string.Empty;
    public string LastActivity { get; set; } = string.Empty;
    public bool IsCurrentDevice { get; set; }
    public bool CanLogout => !IsCurrentDevice;
}