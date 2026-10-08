namespace BeautySalonBooking.Maui.Features.Profile.Models;

public sealed class ProfileDto
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string NationalCode { get; set; } = string.Empty;
    public string AvatarKey { get; set; } = "rose";
    public IReadOnlyList<string> Roles { get; set; } = [];
    public bool IsActive { get; set; } = true;
}

public sealed class UpdateProfileRequest
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string UserName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
}

public sealed class ChangeMobileRequest
{
    public string CurrentMobile { get; init; } = string.Empty;
    public string NewMobile { get; init; } = string.Empty;
}

public sealed class VerifyOtpRequest
{
    public string Mobile { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
}

public sealed class ProfileKpiDto
{
    public string Label { get; init; } = string.Empty;
    public string Value { get; init; } = string.Empty;
    public string Detail { get; init; } = string.Empty;
}

public sealed class NotificationSettingsDto
{
    public bool AppointmentNotifications { get; set; } = true;
    public bool SmsNotifications { get; set; } = true;
    public bool MarketingNotifications { get; set; }
}

public sealed class AppearanceSettingsDto
{
    public bool IsDarkMode { get; set; }
    public string LanguageCode { get; set; } = "fa";
}

public sealed class PrivacySettingsDto
{
    public bool IsProfileVisible { get; set; }
    public bool IsPersonalizedSuggestionsEnabled { get; set; } = true;
}

public enum ProfileAppointmentStatus
{
    Pending,
    Confirmed,
    Completed,
    Cancelled,
    InProgress,
    NoShow
}

public class AppointmentSummaryDto
{
    public long Id { get; init; }
    public string SalonName { get; init; } = string.Empty;
    public string BranchName { get; init; } = string.Empty;
    public string ServiceTitle { get; init; } = string.Empty;
    public string StylistName { get; init; } = string.Empty;
    public DateOnly Date { get; init; }
    public TimeOnly Time { get; init; }
    public decimal Price { get; init; }
    public ProfileAppointmentStatus Status { get; init; }
}

public sealed class AppointmentDetailsDto : AppointmentSummaryDto
{
    public string TrackingCode { get; init; } = string.Empty;
    public string PaymentStatus { get; init; } = string.Empty;
    public string? Notes { get; init; }
}

public sealed class FavoriteSalonDto
{
    public long Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Area { get; init; } = string.Empty;
    public string Rating { get; init; } = string.Empty;
    public bool IsFavorite { get; set; }
}

public sealed class SupportRequest
{
    public string Subject { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}

public sealed class DeleteAccountRequest
{
    public string ConfirmationText { get; init; } = string.Empty;
}
