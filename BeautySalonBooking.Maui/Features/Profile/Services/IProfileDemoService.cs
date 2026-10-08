using BeautySalonBooking.Maui.Features.Profile.Models;

namespace BeautySalonBooking.Maui.Features.Profile.Services;

/// <summary>UI-only profile data source. Replace this registration with an API implementation later.</summary>
public interface IProfileDemoService
{
    Task<ProfileDto> GetProfileAsync(CancellationToken cancellationToken = default);
    Task<ProfileDto> UpdateProfileAsync(UpdateProfileRequest request, CancellationToken cancellationToken = default);
    Task RequestMobileChangeAsync(ChangeMobileRequest request, CancellationToken cancellationToken = default);
    Task<bool> VerifyMobileAsync(VerifyOtpRequest request, CancellationToken cancellationToken = default);
    Task UpdateAvatarAsync(string avatarKey, CancellationToken cancellationToken = default);
    Task<NotificationSettingsDto> GetNotificationSettingsAsync(CancellationToken cancellationToken = default);
    Task SaveNotificationSettingsAsync(NotificationSettingsDto settings, CancellationToken cancellationToken = default);
    Task<AppearanceSettingsDto> GetAppearanceSettingsAsync(CancellationToken cancellationToken = default);
    Task SaveAppearanceSettingsAsync(AppearanceSettingsDto settings, CancellationToken cancellationToken = default);
    Task<PrivacySettingsDto> GetPrivacySettingsAsync(CancellationToken cancellationToken = default);
    Task SavePrivacySettingsAsync(PrivacySettingsDto settings, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<AppointmentSummaryDto>> GetAppointmentsAsync(CancellationToken cancellationToken = default);
    Task<AppointmentDetailsDto?> GetAppointmentAsync(long id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<FavoriteSalonDto>> GetFavoritesAsync(CancellationToken cancellationToken = default);
    Task ToggleFavoriteAsync(long id, CancellationToken cancellationToken = default);
    Task SubmitSupportAsync(SupportRequest request, CancellationToken cancellationToken = default);
    Task DeleteAccountAsync(DeleteAccountRequest request, CancellationToken cancellationToken = default);
}
