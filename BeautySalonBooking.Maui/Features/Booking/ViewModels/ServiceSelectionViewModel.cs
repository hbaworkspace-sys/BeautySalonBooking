using BeautySalonBooking.Contracts.Category.Dtos;
using BeautySalonBooking.Contracts.Service.Dtos;
using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Booking.Services;
using BeautySalonBooking.Maui.Features.Category.Cache;
using BeautySalonBooking.Maui.Features.Category.Constants;
using BeautySalonBooking.Maui.Features.Category.Services;
using BeautySalonBooking.Maui.Features.Service.Cache;
using BeautySalonBooking.Maui.Features.Service.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Booking.ViewModels;

public partial class ServiceSelectionViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly ICategoryApiService _categoryApiService;
    private readonly ICategoryCache _categoryCache;
    private readonly IServiceApiService _serviceApiService;
    private readonly IServiceCache _serviceCache;
    private readonly IBookingSelectionState _bookingSelectionState;

    public ServiceSelectionViewModel(
        INavigationService navigationService,
        ICategoryApiService categoryApiService,
        ICategoryCache categoryCache,
        IServiceApiService serviceApiService,
        IServiceCache serviceCache,
        IBookingSelectionState bookingSelectionState)
    {
        _navigationService = navigationService;
        _categoryApiService = categoryApiService;
        _categoryCache = categoryCache;
        _serviceApiService = serviceApiService;
        _serviceCache = serviceCache;
        _bookingSelectionState = bookingSelectionState;
    }

    #region Categories

    [ObservableProperty]
    private IReadOnlyList<CategoryDto> categories = [];

    [ObservableProperty]
    private IReadOnlyList<CategoryDto> subCategories = [];

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private bool isSubCategoriesLoading;

    [ObservableProperty]
    private bool isSubCategoriesVisible;

    [ObservableProperty]
    private CategoryDto? selectedCategory;

    #endregion

    #region Services

    [ObservableProperty]
    private IReadOnlyList<ServiceDto> services = [];

    [ObservableProperty]
    private bool isServicesVisible;

    #endregion

    #region Load Categories

    public async Task LoadCategoriesAsync(
        CancellationToken cancellationToken = default)
    {
        if (_categoryCache.TryGet(
                CategoryCodes.Service,
                out var cachedCategories))
        {
            Categories = cachedCategories;
            return;
        }

        IsLoading = true;

        try
        {
            var result =
                await _categoryApiService.GetChildrenAsync(
                    CategoryCodes.Service,
                    cancellationToken);

            if (!result.IsSuccess ||
                result.Payload?.Categories is null)
            {
                return;
            }

            Categories = result.Payload.Categories;

            _categoryCache.Set(
                CategoryCodes.Service,
                Categories);
        }
        finally
        {
            IsLoading = false;
        }
    }

    #endregion

    #region Category Selection

    [RelayCommand]
    private async Task SelectCategoryAsync(
        CategoryDto category)
    {
        if (IsSubCategoriesLoading)
            return;

        if (category is null)
            return;

        if (SelectedCategory?.Id == category.Id)
            return;

        SelectedCategory = category;

        SubCategories = [];
        Services = [];

        IsSubCategoriesVisible = false;
        IsServicesVisible = false;
        IsSubCategoriesLoading = true;

        try
        {
            var result =
                await _categoryApiService.GetChildrenAsync(
                    category.Code,
                    CancellationToken.None);

            if (!result.IsSuccess)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"Failed to load sub categories for: {category.Code}");

                return;
            }

            var subCategories = result.Payload?.Categories;

            // =====================================================
            // Category دارای زیرمجموعه است
            // =====================================================

            if (subCategories is { Count: > 0 })
            {
                IsSubCategoriesVisible = true;
                SubCategories = subCategories;

                System.Diagnostics.Debug.WriteLine(
                    $"SubCategories Count: {SubCategories.Count}");

                return;
            }

            // =====================================================
            // زیرمجموعه ندارد
            // مستقیماً Services را می‌خوانیم
            // =====================================================

            System.Diagnostics.Debug.WriteLine(
                $"No sub categories found for: {category.Code}");

            if (_serviceCache.TryGet(
                    category.Id,
                    out var cachedServices))
            {
                Services = cachedServices;
                IsServicesVisible = true;

                return;
            }

            var servicesResult =
                await _serviceApiService.GetByCategoryAsync(
                    category.Id,
                    CancellationToken.None);

            if (!servicesResult.IsSuccess ||
                servicesResult.Payload?.Services is null)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"Failed to load services for category: {category.Id}");

                return;
            }

            Services = servicesResult.Payload.Services;

            _serviceCache.Set(
                category.Id,
                Services);

            IsServicesVisible = true;

            System.Diagnostics.Debug.WriteLine(
                $"Services Count: {Services.Count}");
        }
        finally
        {
            IsSubCategoriesLoading = false;
        }
    }

    #endregion

    #region SubCategory Selection

    [RelayCommand]
    private async Task SelectSubCategoryAsync(
        CategoryDto category)
    {
        if (IsSubCategoriesLoading)
            return;

        if (category is null)
            return;

        Services = [];
        IsServicesVisible = false;
        IsSubCategoriesLoading = true;

        try
        {
            if (_serviceCache.TryGet(
                    category.Id,
                    out var cachedServices))
            {
                Services = cachedServices;
                IsServicesVisible = true;

                return;
            }

            var result =
                await _serviceApiService.GetByCategoryAsync(
                    category.Id,
                    CancellationToken.None);

            if (!result.IsSuccess ||
                result.Payload?.Services is null)
            {
                return;
            }

            Services = result.Payload.Services;

            _serviceCache.Set(
                category.Id,
                Services);

            IsServicesVisible = true;
        }
        finally
        {
            IsSubCategoriesLoading = false;
        }
    }

    #endregion

    #region Service Selection

    [RelayCommand]
    private Task SelectServiceAsync(
        ServiceDto service)
    {
        if (service is null)
            return Task.CompletedTask;

        // ذخیره سرویس انتخاب‌شده در Booking State
        _bookingSelectionState.Current.Service = service;

        return _navigationService
            .GoToBranchSelectionAsync();
    }

    #endregion

    #region Navigation

    [RelayCommand]
    private Task GoBackAsync()
    {
        return _navigationService.GoBackAsync();
    }

    #endregion
}