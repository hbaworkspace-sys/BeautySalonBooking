using BeautySalonBooking.Contracts.Region;
using BeautySalonBooking.Contracts.SalonVerifications;
using BeautySalonBooking.Maui.Features.Region;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BeautySalonBooking.Maui.Features.Salons
{
    public partial class SalonRegistrationViewModel : ObservableObject
    {
        private readonly RegionApiService _regionApiService;
        private readonly SalonVerificationApiService _salonVerificationApiService;

        public SalonRegistrationViewModel(RegionApiService regionApiService, SalonVerificationApiService salonVerificationApiService)
        {
            _regionApiService = regionApiService;
            _salonVerificationApiService = salonVerificationApiService;

            _ = LoadProvincesAsync();
        }

        // =======================
        // اطلاعات سالن
        // =======================

        [ObservableProperty]
        private string salonName = string.Empty;

        [ObservableProperty]
        private string licenseNumber = string.Empty;

        [ObservableProperty]
        private string landlinePhoneNumber = string.Empty;

        [ObservableProperty]
        private string postalCode;

        [ObservableProperty]
        private string address;

        // =======================
        // استان
        // =======================

        [ObservableProperty]
        private ObservableCollection<RegionDto> provinces = new();

        [ObservableProperty]
        private RegionDto? selectedProvince;



        partial void OnSelectedProvinceChanged(RegionDto? value)
        {
            if (value is null) return;

            _ = OnProvinceChanged(value.Id);
        }

        // =======================
        // شهرستان
        // =======================

        [ObservableProperty]
        private ObservableCollection<RegionDto> counties = new();

        [ObservableProperty]
        private RegionDto? selectedCounty;

        partial void OnSelectedCountyChanged(RegionDto? value)
        {
            if (value is null) return;

            _ = OnCountyChanged(value.Id);
        }

        // =======================
        // بخش
        // =======================

        [ObservableProperty]
        private ObservableCollection<RegionDto> districts = new();

        [ObservableProperty]
        private RegionDto? selectedDistrict;

        partial void OnSelectedDistrictChanged(RegionDto? value)
        {
            if (value is null) return;

            _ = OnDistrictChanged(value.Id);
        }

        // =======================
        // دهستان
        // =======================

        [ObservableProperty]
        private ObservableCollection<RegionDto> ruralDistricts = new();

        [ObservableProperty]
        private RegionDto? selectedRuralDistrict;

        partial void OnSelectedRuralDistrictChanged(RegionDto? value)
        {
            if (value is null) return;

            _ = LoadVillagesAsync(value.Id);
        }

        // =======================
        // روستا
        // =======================

        [ObservableProperty]
        private ObservableCollection<RegionDto> villages = new();

        [ObservableProperty]
        private RegionDto? selectedVillage;

        // =======================
        // LOADERS
        // =======================

        private async Task LoadProvincesAsync()
        {
            Provinces.Clear();
            SelectedProvince = null;

            Counties.Clear();
            SelectedCounty = null;

            Districts.Clear();
            SelectedDistrict = null;

            RuralDistricts.Clear();
            SelectedRuralDistrict = null;

            Villages.Clear();
            SelectedVillage = null;

            var result = await _regionApiService.GetChildRegionsAsync(new GetChildRegionsRequest
            {
                ParentId = 17
            });

            if (!result.IsSuccess || result.Data == null)
                return;

            Provinces = new ObservableCollection<RegionDto>(result.Data.Regions);
        }

        private async Task OnProvinceChanged(int provinceId)
        {
            Counties.Clear();
            SelectedCounty = null;

            Districts.Clear();
            SelectedDistrict = null;

            RuralDistricts.Clear();
            SelectedRuralDistrict = null;

            Villages.Clear();
            SelectedVillage = null;

            var result = await _regionApiService.GetChildRegionsAsync(new GetChildRegionsRequest
            {
                ParentId = provinceId
            });

            if (!result.IsSuccess || result.Data == null)
                return;

            Counties = new ObservableCollection<RegionDto>(result.Data.Regions);
        }

        private async Task OnCountyChanged(int countyId)
        {
            Districts.Clear();
            SelectedDistrict = null;

            RuralDistricts.Clear();
            SelectedRuralDistrict = null;

            Villages.Clear();
            SelectedVillage = null;

            var result = await _regionApiService.GetChildRegionsAsync(new GetChildRegionsRequest
            {
                ParentId = countyId
            });

            if (!result.IsSuccess || result.Data == null)
                return;

            Districts = new ObservableCollection<RegionDto>(result.Data.Regions);
        }

        private async Task OnDistrictChanged(int districtId)
        {
            RuralDistricts.Clear();
            SelectedRuralDistrict = null;

            Villages.Clear();
            SelectedVillage = null;

            var result = await _regionApiService.GetChildRegionsAsync(new GetChildRegionsRequest
            {
                ParentId = districtId
            });

            if (!result.IsSuccess || result.Data == null)
                return;

            RuralDistricts = new ObservableCollection<RegionDto>(result.Data.Regions);
        }

        private async Task LoadVillagesAsync(int ruralDistrictId)
        {
            Villages.Clear();
            SelectedVillage = null;

            var result = await _regionApiService.GetChildRegionsAsync(new GetChildRegionsRequest
            {
                ParentId = ruralDistrictId
            });

            if (!result.IsSuccess || result.Data == null)
                return;

            Villages = new ObservableCollection<RegionDto>(result.Data.Regions);
        }

        [ObservableProperty]
        private bool isBusy = false;

        [RelayCommand]
        private async Task RegisterAsync()
        {
            if (IsBusy)
                return;

            if (!await ValidateInputAsync())
                return;

            try
            {
                IsBusy = true;

                var request = new RegisterSalonVerificationRequest
                {
                    Name = SalonName,
                    LicenceNumber = LicenseNumber,
                    LandlinePhoneNumber = LandlinePhoneNumber,
                    RegionId = GetSelectedRegionId(),
                    Address = Address,
                    PostalCode = PostalCode
                };
                var userIdString = await SecureStorage.GetAsync("user_id");

                if (!Guid.TryParse(userIdString, out var userId))
                {
                    await Shell.Current.DisplayAlert("خطا", "شناسه کاربر معتبر نیست.", "باشه");
                    return;
                }
                request.OwnerUserId = userId;

                var response = await _salonVerificationApiService.RegisterAsync(request);

                if (response == null)
                {
                    await Shell.Current.DisplayAlert("خطا", "پاسخی از سرور دریافت نشد.", "باشه");
                    return;
                }

                if (!response.IsSuccess)
                {
                    await Shell.Current.DisplayAlert("خطا", response.Message, "باشه");
                    return;
                }

                // در صورت نیاز بعداً اطلاعات درخواست را در ViewModel نگه می‌داریم
                // CurrentVerificationId = response.Data?.Id;

                await Shell.Current.GoToAsync(nameof(SalonVerificationSuccessPage));
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("خطا", "مشکلی در ارتباط با سرور رخ داد.", "باشه");
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task<bool> ValidateInputAsync()
        {
            if (string.IsNullOrWhiteSpace(SalonName))
            {
                await Shell.Current.DisplayAlert("خطا", "لطفاً نام سالن را وارد کنید.", "باشه");
                return false;
            }

            if (SalonName.Length < 3)
            {
                await Shell.Current.DisplayAlert("خطا", "نام سالن باید حداقل ۳ کاراکتر باشد.", "باشه");
                return false;
            }

            if (string.IsNullOrWhiteSpace(LicenseNumber))
            {
                await Shell.Current.DisplayAlert("خطا", "لطفاً شماره مجوز را وارد کنید.", "باشه");
                return false;
            }

            if (string.IsNullOrWhiteSpace(LandlinePhoneNumber))
            {
                await Shell.Current.DisplayAlert("خطا", "لطفاً شماره تلفن ثابت سالن را وارد کنید.", "باشه");
                return false;
            }

            if (SelectedProvince is null)
            {
                await Shell.Current.DisplayAlert("خطا", "لطفاً استان را انتخاب کنید.", "باشه");
                return false;
            }

            if (SelectedCounty is null)
            {
                await Shell.Current.DisplayAlert("خطا", "لطفاً شهرستان را انتخاب کنید.", "باشه");
                return false;
            }

            return true;
        }
        private int GetSelectedRegionId()
        {
            if (SelectedVillage is not null)
                return SelectedVillage.Id;

            if (SelectedRuralDistrict is not null)
                return SelectedRuralDistrict.Id;

            if (SelectedDistrict is not null)
                return SelectedDistrict.Id;

            if (SelectedCounty is not null)
                return SelectedCounty.Id;

            if (SelectedProvince is not null)
                return SelectedProvince.Id;

            throw new InvalidOperationException("هیچ منطقه‌ای انتخاب نشده است.");
        }
    }
}