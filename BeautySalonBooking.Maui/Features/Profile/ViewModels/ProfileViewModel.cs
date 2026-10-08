using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using BeautySalonBooking.Contracts.Authentication.Services;
using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Auth.Services;
using BeautySalonBooking.Maui.Features.Profile.Models;
using BeautySalonBooking.Maui.Features.Profile.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Profile.ViewModels;

public partial class ProfileViewModel : ObservableObject
{
    private readonly IProfileDemoService _profileService;
    private readonly IAuthSessionService _authSessionService;
    private readonly IUserContext _userContext;
    private readonly INavigationService _navigationService;
    private readonly IDialogService _dialogService;
    private bool _initialized;

    public ProfileViewModel(IProfileDemoService profileService, IAuthSessionService authSessionService, IUserContext userContext, INavigationService navigationService, IDialogService dialogService)
    {
        _profileService = profileService;
        _authSessionService = authSessionService;
        _userContext = userContext;
        _navigationService = navigationService;
        _dialogService = dialogService;
    }

    public ObservableCollection<AppointmentSummaryDto> Appointments { get; } = [];
    public ObservableCollection<FavoriteSalonDto> Favorites { get; } = [];
    public ObservableCollection<string> FaqItems { get; } = ["چگونه نوبت خود را لغو کنم؟", "پرداخت چگونه انجام می‌شود؟", "چرا پیامک تأیید دریافت نکردم؟"];

    [ObservableProperty] private ProfileScreen currentScreen = ProfileScreen.Overview;
    [ObservableProperty] private bool isBusy;
    [ObservableProperty] private string? errorMessage;
    [ObservableProperty] private string? successMessage;
    [ObservableProperty] private bool hasError;
    [ObservableProperty] private bool hasSuccess;
    [ObservableProperty] private string fullName = string.Empty;
    [ObservableProperty] private string initials = "؟";
    [ObservableProperty] private string avatarKey = "rose";
    [ObservableProperty] private string firstName = string.Empty;
    [ObservableProperty] private string lastName = string.Empty;
    [ObservableProperty] private string userName = string.Empty;
    [ObservableProperty] private string email = string.Empty;
    [ObservableProperty] private string phoneNumber = string.Empty;
    [ObservableProperty] private string nationalCode = string.Empty;
    [ObservableProperty] private string rolesSummary = "مشتری";
    [ObservableProperty] private bool isBusinessRole;
    [ObservableProperty] private bool isAccountActive = true;
    [ObservableProperty] private string newMobileNumber = string.Empty;
    [ObservableProperty] private string otpCode = string.Empty;
    [ObservableProperty] private string supportSubject = string.Empty;
    [ObservableProperty] private string supportMessage = string.Empty;
    [ObservableProperty] private string deleteConfirmationText = string.Empty;
    [ObservableProperty] private bool appointmentNotifications;
    [ObservableProperty] private bool smsNotifications;
    [ObservableProperty] private bool marketingNotifications;
    [ObservableProperty] private bool isDarkMode;
    [ObservableProperty] private string languageCode = "fa";
    [ObservableProperty] private bool isProfileVisible;
    [ObservableProperty] private bool isPersonalizedSuggestionsEnabled;
    [ObservableProperty] private AppointmentDetailsDto? selectedAppointment;

    [ObservableProperty] private bool isOverviewVisible = true;
    [ObservableProperty] private bool isEditProfileVisible;
    [ObservableProperty] private bool isPhotoSelectionVisible;
    [ObservableProperty] private bool isChangeMobileVisible;
    [ObservableProperty] private bool isOtpVerificationVisible;
    [ObservableProperty] private bool isAccountInfoVisible;
    [ObservableProperty] private bool isSecurityVisible;
    [ObservableProperty] private bool isNotificationSettingsVisible;
    [ObservableProperty] private bool isAppearanceVisible;
    [ObservableProperty] private bool isLanguageVisible;
    [ObservableProperty] private bool isPrivacyVisible;
    [ObservableProperty] private bool isHelpCenterVisible;
    [ObservableProperty] private bool isContactSupportVisible;
    [ObservableProperty] private bool isAboutVisible;
    [ObservableProperty] private bool isAppointmentHistoryVisible;
    [ObservableProperty] private bool isAppointmentDetailsVisible;
    [ObservableProperty] private bool isSpendingSummaryVisible;
    [ObservableProperty] private bool isFavoritesVisible;
    [ObservableProperty] private bool isDeleteAccountVisible;
    [ObservableProperty] private double progress;
    public bool HasFavorites => Favorites.Count > 0;
    public string AccountStatus => IsAccountActive ? "فعال" : "غیرفعال";
    public string TotalSpending => Appointments.Where(x => x.Status == ProfileAppointmentStatus.Completed).Sum(x => x.Price).ToString("N0", CultureInfo.InvariantCulture) + " تومان";
    public string CompletedAppointments => Appointments.Count(x => x.Status == ProfileAppointmentStatus.Completed).ToString(CultureInfo.InvariantCulture);

    public async Task InitializeAsync()
    {
        if (_initialized) return;
        _initialized = true;
        IsBusy = true;
        try
        {
            var profileTask = _profileService.GetProfileAsync();
            var notificationTask = _profileService.GetNotificationSettingsAsync();
            var appearanceTask = _profileService.GetAppearanceSettingsAsync();
            var privacyTask = _profileService.GetPrivacySettingsAsync();
            var appointmentTask = _profileService.GetAppointmentsAsync();
            await Task.WhenAll(profileTask, notificationTask, appearanceTask, privacyTask, appointmentTask);
            ApplyProfile(await profileTask);
            var notifications = await notificationTask; AppointmentNotifications = notifications.AppointmentNotifications; SmsNotifications = notifications.SmsNotifications; MarketingNotifications = notifications.MarketingNotifications;
            var appearance = await appearanceTask; IsDarkMode = appearance.IsDarkMode; LanguageCode = appearance.LanguageCode;
            var privacy = await privacyTask; IsProfileVisible = privacy.IsProfileVisible; IsPersonalizedSuggestionsEnabled = privacy.IsPersonalizedSuggestionsEnabled;
            Replace(Appointments, await appointmentTask);
        }
        catch
        {
            ErrorMessage = "اطلاعات پروفایل بارگذاری نشد. دوباره تلاش کنید.";
        }
        finally { IsBusy = false; }
    }

    [RelayCommand]
    private async Task Navigate(ProfileScreen screen)
    {
        switch (screen)
        {
            case ProfileScreen.EditProfile:
                await _navigationService.GoToEditProfileAsync();
                return;

            case ProfileScreen.ChangeMobile:
                await _navigationService.GoToChangeMobileAsync();
                break;

            case ProfileScreen.Security:
                await _navigationService.GoToSecurityAsync();
                break;

            case ProfileScreen.NotificationSettings:
                await _navigationService.GoToNotificationSettingsAsync();
                break;

            case ProfileScreen.Privacy:
                await _navigationService.GoToPrivacyAsync();
                break;

            default:
                CurrentScreen = screen;
                ErrorMessage = null;
                SuccessMessage = null;
                break;
        }
    }
    [RelayCommand] private void GoBack() => Navigate(ProfileScreen.Overview);
    [RelayCommand] private Task RetryAsync() { _initialized = false; return InitializeAsync(); }

    [RelayCommand]
    private async Task SaveProfileAsync()
    {
        if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName)) { ErrorMessage = "نام و نام خانوادگی را وارد کنید."; return; }
        if (!string.IsNullOrWhiteSpace(Email) && !Email.Contains('@', StringComparison.Ordinal)) { ErrorMessage = "نشانی ایمیل معتبر نیست."; return; }
        IsBusy = true;
        try { ApplyProfile(await _profileService.UpdateProfileAsync(new UpdateProfileRequest { FirstName = FirstName, LastName = LastName, UserName = UserName, Email = Email })); SuccessMessage = "تغییرات پروفایل ذخیره شد."; }
        finally { IsBusy = false; }
    }

    [RelayCommand]
    private async Task SelectAvatarAsync(string avatar)
    {
        if (string.IsNullOrWhiteSpace(avatar)) return;
        await _profileService.UpdateAvatarAsync(avatar); AvatarKey = avatar; SuccessMessage = "تصویر پروفایل به‌روزرسانی شد.";
    }

    [RelayCommand]
    private async Task RequestMobileChangeAsync()
    {
        var normalized = NewMobileNumber.Trim();
        if (normalized.Length < 10) { ErrorMessage = "شماره همراه معتبر وارد کنید."; return; }
        await _profileService.RequestMobileChangeAsync(new ChangeMobileRequest { CurrentMobile = PhoneNumber, NewMobile = normalized });
        OtpCode = string.Empty; Navigate(ProfileScreen.OtpVerification); SuccessMessage = "کد آزمایشی ۱۲۳۴ ارسال شد.";
    }

    [RelayCommand]
    private async Task VerifyOtpAsync()
    {
        var accepted = await _profileService.VerifyMobileAsync(new VerifyOtpRequest { Mobile = NewMobileNumber.Trim(), Code = OtpCode.Trim() });
        if (!accepted) { ErrorMessage = "کد تأیید نادرست است. برای حالت آزمایشی از ۱۲۳۴ استفاده کنید."; return; }
        PhoneNumber = NewMobileNumber.Trim(); Navigate(ProfileScreen.AccountInfo); SuccessMessage = "شماره همراه تأیید شد.";
    }

    [RelayCommand] private async Task SaveNotificationsAsync() { await _profileService.SaveNotificationSettingsAsync(new NotificationSettingsDto { AppointmentNotifications = AppointmentNotifications, SmsNotifications = SmsNotifications, MarketingNotifications = MarketingNotifications }); SuccessMessage = "تنظیمات اعلان ذخیره شد."; }
    [RelayCommand]
    private async Task SaveAppearanceAsync()
    {
        await _profileService.SaveAppearanceSettingsAsync(new AppearanceSettingsDto { IsDarkMode = IsDarkMode, LanguageCode = LanguageCode });
        if (Application.Current is { } application)
            application.UserAppTheme = IsDarkMode ? AppTheme.Dark : AppTheme.Light;
        SuccessMessage = "تنظیمات نمایش ذخیره شد.";
    }
    [RelayCommand] private async Task SavePrivacyAsync() { await _profileService.SavePrivacySettingsAsync(new PrivacySettingsDto { IsProfileVisible = IsProfileVisible, IsPersonalizedSuggestionsEnabled = IsPersonalizedSuggestionsEnabled }); SuccessMessage = "تنظیمات حریم خصوصی ذخیره شد."; }
    [RelayCommand] private void SelectLanguage(string code) { LanguageCode = code; SuccessMessage = "زبان انتخاب شد. برای اعمال کامل، منابع ترجمه را جایگزین کنید."; }

    [RelayCommand]
    private async Task OpenAppointmentAsync(long id)
    {
        SelectedAppointment = await _profileService.GetAppointmentAsync(id);
        if (SelectedAppointment is null) { ErrorMessage = "جزئیات نوبت پیدا نشد."; return; }
        Navigate(ProfileScreen.AppointmentDetails);
    }

    [RelayCommand]
    private async Task LoadFavoritesAsync()
    {
        Replace(Favorites, await _profileService.GetFavoritesAsync()); OnPropertyChanged(nameof(HasFavorites));
    }

    [RelayCommand]
    private async Task ToggleFavoriteAsync(long id)
    {
        await _profileService.ToggleFavoriteAsync(id); await LoadFavoritesAsync();
    }

    [RelayCommand]
    private async Task SubmitSupportAsync()
    {
        if (string.IsNullOrWhiteSpace(SupportSubject) || string.IsNullOrWhiteSpace(SupportMessage)) { ErrorMessage = "موضوع و متن پیام را وارد کنید."; return; }
        await _profileService.SubmitSupportAsync(new SupportRequest { Subject = SupportSubject, Message = SupportMessage });
        SupportSubject = string.Empty; SupportMessage = string.Empty; SuccessMessage = "درخواست پشتیبانی ثبت شد.";
    }

    [RelayCommand]
    private async Task LogoutAsync()
    {
        if (!await _dialogService.ShowConfirmationAsync("خروج از حساب", "آیا می‌خواهید از حساب خارج شوید؟", "خروج", "انصراف")) return;
        await _authSessionService.ClearAsync(); _userContext.Clear(); await _navigationService.GoToLoginAsRootAsync();
    }

    [RelayCommand]
    private async Task DeleteAccountAsync()
    {
        if (!string.Equals(DeleteConfirmationText.Trim(), "حذف", StringComparison.Ordinal)) { ErrorMessage = "برای ادامه، واژه «حذف» را وارد کنید."; return; }
        if (!await _dialogService.ShowConfirmationAsync("حذف حساب", "این عملیات در نسخه آزمایشی فقط وضعیت محلی را تغییر می‌دهد.", "حذف", "انصراف")) return;
        await _profileService.DeleteAccountAsync(new DeleteAccountRequest { ConfirmationText = DeleteConfirmationText }); IsAccountActive = false; SuccessMessage = "حساب در حالت آزمایشی غیرفعال شد.";
    }

    partial void OnCurrentScreenChanged(ProfileScreen value)
    {
        IsOverviewVisible = value == ProfileScreen.Overview; IsEditProfileVisible = value == ProfileScreen.EditProfile; IsPhotoSelectionVisible = value == ProfileScreen.PhotoSelection;
        IsChangeMobileVisible = value == ProfileScreen.ChangeMobile; IsOtpVerificationVisible = value == ProfileScreen.OtpVerification; IsAccountInfoVisible = value == ProfileScreen.AccountInfo;
        IsSecurityVisible = value == ProfileScreen.Security; IsNotificationSettingsVisible = value == ProfileScreen.NotificationSettings; IsAppearanceVisible = value == ProfileScreen.Appearance;
        IsLanguageVisible = value == ProfileScreen.Language; IsPrivacyVisible = value == ProfileScreen.Privacy; IsHelpCenterVisible = value == ProfileScreen.HelpCenter;
        IsContactSupportVisible = value == ProfileScreen.ContactSupport; IsAboutVisible = value == ProfileScreen.About; IsAppointmentHistoryVisible = value == ProfileScreen.AppointmentHistory;
        IsAppointmentDetailsVisible = value == ProfileScreen.AppointmentDetails; IsSpendingSummaryVisible = value == ProfileScreen.SpendingSummary; IsFavoritesVisible = value == ProfileScreen.Favorites;
        IsDeleteAccountVisible = value == ProfileScreen.DeleteAccount;
        if (value == ProfileScreen.Favorites) _ = LoadFavoritesAsync();
    }

    partial void OnErrorMessageChanged(string? value) => HasError = !string.IsNullOrWhiteSpace(value);
    partial void OnSuccessMessageChanged(string? value) => HasSuccess = !string.IsNullOrWhiteSpace(value);

    private void ApplyProfile(ProfileDto profile)
    {
        FirstName = profile.FirstName; LastName = profile.LastName; UserName = profile.UserName; Email = profile.Email; PhoneNumber = profile.PhoneNumber; NationalCode = profile.NationalCode; AvatarKey = profile.AvatarKey; IsAccountActive = profile.IsActive;
        FullName = string.Join(" ", new[] { FirstName, LastName }.Where(x => !string.IsNullOrWhiteSpace(x))); Initials = string.Concat(FullName.Split(' ', StringSplitOptions.RemoveEmptyEntries).Take(2).Select(x => x[0]));
        RolesSummary = profile.Roles.Count == 0 ? "مشتری" : string.Join(" • ", profile.Roles); IsBusinessRole = profile.Roles.Any(x => x.Contains("مدیر", StringComparison.Ordinal) || x.Contains("Owner", StringComparison.OrdinalIgnoreCase));
    }
    private static void Replace<T>(ObservableCollection<T> target, IEnumerable<T> source) { target.Clear(); foreach (var item in source) target.Add(item); }
}
