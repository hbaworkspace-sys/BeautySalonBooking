using BeautySalonBooking.Maui.Features.Profile.Models;

namespace BeautySalonBooking.Maui.Features.Profile.Services;

/// <summary>In-memory, deterministic demonstration implementation; it never sends network traffic.</summary>
public sealed class ProfileDemoService : IProfileDemoService
{
    private ProfileDto _profile = new()
    {
        Id = 1, FirstName = "نگار", LastName = "محمدی", UserName = "negar.m",
        PhoneNumber = "۰۹۱۲ ۱۲۳ ۴۵۶۷", Email = "negar@example.com", NationalCode = "۰۰۱۲۳۴۵۶۷۸",
        Roles = ["مشتری"], AvatarKey = "rose"
    };
    private NotificationSettingsDto _notifications = new();
    private AppearanceSettingsDto _appearance = new();
    private PrivacySettingsDto _privacy = new();
    private readonly List<FavoriteSalonDto> _favorites =
    [
        new() { Id = 1, Name = "آرایشگاه بلاسم", Area = "نیاوران", Rating = "۴٫۹", IsFavorite = true },
        new() { Id = 2, Name = "سالن زیبایی رُز", Area = "ونک", Rating = "۴٫۸", IsFavorite = true }
    ];
    private readonly List<AppointmentDetailsDto> _appointments =
    [
        new() { Id = 101, SalonName = "آرایشگاه بلاسم", BranchName = "شعبه نیاوران", ServiceTitle = "کراتین مو", StylistName = "ساناز موسوی", Date = new DateOnly(2025, 1, 15), Time = new TimeOnly(11, 0), Price = 850000, Status = ProfileAppointmentStatus.Confirmed, TrackingCode = "BS-10351", PaymentStatus = "پرداخت شده", Notes = "لطفاً ۱۰ دقیقه زودتر مراجعه کنید." },
        new() { Id = 102, SalonName = "سالن زیبایی رُز", BranchName = "شعبه ونک", ServiceTitle = "رنگ و هایلایت", StylistName = "مریم احمدی", Date = new DateOnly(2024, 12, 28), Time = new TimeOnly(14, 30), Price = 1200000, Status = ProfileAppointmentStatus.Completed, TrackingCode = "BS-10192", PaymentStatus = "پرداخت شده" },
        new() { Id = 103, SalonName = "آرایشگاه لاله", BranchName = "شعبه مرکزی", ServiceTitle = "کوتاهی و آرایش", StylistName = "نیلوفر رضایی", Date = new DateOnly(2024, 11, 10), Time = new TimeOnly(10, 0), Price = 450000, Status = ProfileAppointmentStatus.Completed, TrackingCode = "BS-10074", PaymentStatus = "پرداخت شده" }
    ];
    private string? _pendingMobile;

    public Task<ProfileDto> GetProfileAsync(CancellationToken cancellationToken = default) => Task.FromResult(Clone(_profile));
    public Task<ProfileDto> UpdateProfileAsync(UpdateProfileRequest request, CancellationToken cancellationToken = default)
    {
        _profile.FirstName = request.FirstName.Trim(); _profile.LastName = request.LastName.Trim();
        _profile.UserName = request.UserName.Trim(); _profile.Email = request.Email.Trim();
        return GetProfileAsync(cancellationToken);
    }
    public Task RequestMobileChangeAsync(ChangeMobileRequest request, CancellationToken cancellationToken = default) { _pendingMobile = request.NewMobile; return Task.CompletedTask; }
    public Task<bool> VerifyMobileAsync(VerifyOtpRequest request, CancellationToken cancellationToken = default)
    {
        var valid = NormalizeDigits(request.Code) == "1234" && request.Mobile == _pendingMobile;
        if (valid) _profile.PhoneNumber = request.Mobile;
        return Task.FromResult(valid);
    }
    public Task UpdateAvatarAsync(string avatarKey, CancellationToken cancellationToken = default) { _profile.AvatarKey = avatarKey; return Task.CompletedTask; }
    public Task<NotificationSettingsDto> GetNotificationSettingsAsync(CancellationToken cancellationToken = default) => Task.FromResult(new NotificationSettingsDto { AppointmentNotifications = _notifications.AppointmentNotifications, SmsNotifications = _notifications.SmsNotifications, MarketingNotifications = _notifications.MarketingNotifications });
    public Task SaveNotificationSettingsAsync(NotificationSettingsDto settings, CancellationToken cancellationToken = default) { _notifications = settings; return Task.CompletedTask; }
    public Task<AppearanceSettingsDto> GetAppearanceSettingsAsync(CancellationToken cancellationToken = default) => Task.FromResult(new AppearanceSettingsDto { IsDarkMode = _appearance.IsDarkMode, LanguageCode = _appearance.LanguageCode });
    public Task SaveAppearanceSettingsAsync(AppearanceSettingsDto settings, CancellationToken cancellationToken = default) { _appearance = settings; return Task.CompletedTask; }
    public Task<PrivacySettingsDto> GetPrivacySettingsAsync(CancellationToken cancellationToken = default) => Task.FromResult(new PrivacySettingsDto { IsProfileVisible = _privacy.IsProfileVisible, IsPersonalizedSuggestionsEnabled = _privacy.IsPersonalizedSuggestionsEnabled });
    public Task SavePrivacySettingsAsync(PrivacySettingsDto settings, CancellationToken cancellationToken = default) { _privacy = settings; return Task.CompletedTask; }
    public Task<IReadOnlyList<AppointmentSummaryDto>> GetAppointmentsAsync(CancellationToken cancellationToken = default) => Task.FromResult<IReadOnlyList<AppointmentSummaryDto>>(_appointments);
    public Task<AppointmentDetailsDto?> GetAppointmentAsync(long id, CancellationToken cancellationToken = default) => Task.FromResult(_appointments.SingleOrDefault(x => x.Id == id));
    public Task<IReadOnlyList<FavoriteSalonDto>> GetFavoritesAsync(CancellationToken cancellationToken = default) => Task.FromResult<IReadOnlyList<FavoriteSalonDto>>(_favorites.Where(x => x.IsFavorite).ToList());
    public Task ToggleFavoriteAsync(long id, CancellationToken cancellationToken = default) { var item = _favorites.SingleOrDefault(x => x.Id == id); if (item is not null) item.IsFavorite = !item.IsFavorite; return Task.CompletedTask; }
    public Task SubmitSupportAsync(SupportRequest request, CancellationToken cancellationToken = default) => Task.CompletedTask;
    public Task DeleteAccountAsync(DeleteAccountRequest request, CancellationToken cancellationToken = default) { _profile.IsActive = false; return Task.CompletedTask; }
    private static ProfileDto Clone(ProfileDto source) => new() { Id = source.Id, FirstName = source.FirstName, LastName = source.LastName, UserName = source.UserName, PhoneNumber = source.PhoneNumber, Email = source.Email, NationalCode = source.NationalCode, AvatarKey = source.AvatarKey, IsActive = source.IsActive, Roles = source.Roles.ToList() };
    private static string NormalizeDigits(string value) => value
        .Replace('۰', '0').Replace('۱', '1').Replace('۲', '2').Replace('۳', '3').Replace('۴', '4')
        .Replace('۵', '5').Replace('۶', '6').Replace('۷', '7').Replace('۸', '8').Replace('۹', '9');
}
