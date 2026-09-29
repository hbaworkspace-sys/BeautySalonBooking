using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Booking.Services;
using BeautySalonBooking.Maui.Features.Category.Cache;
using BeautySalonBooking.Maui.Features.Category.Constants;
using BeautySalonBooking.Maui.Features.Category.Models;
using BeautySalonBooking.Maui.Features.Category.Services;
using BeautySalonBooking.Maui.Features.Service.Cache;
using BeautySalonBooking.Maui.Features.Service.Models;
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
    private IReadOnlyList<CategoryModel> categories = [];

    [ObservableProperty]
    private IReadOnlyList<CategoryModel> subCategories = [];

    [ObservableProperty]
    private bool isSubCategoriesVisible;

    [ObservableProperty]
    private CategoryModel? selectedCategory;

    [ObservableProperty]
    private CategoryModel? selectedSubCategory;

    [ObservableProperty]
    private bool isBusy;
    #endregion

    #region Services

    [ObservableProperty]
    private IReadOnlyList<ServiceModel> services = [];

    [ObservableProperty]
    private bool isServicesVisible;

    #endregion

    #region Load Categories

    public async Task LoadCategoriesAsync(
        CancellationToken cancellationToken = default)
    {
        if (IsBusy)
            return;

        if (_categoryCache.TryGet(
                CategoryCodes.Service,
                out var cachedCategories))
        {
            Categories = cachedCategories
                .Select(x => new CategoryModel(x))
                .ToList();

            return;
        }
        IsBusy = true;

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

            var categoryDtos = result.Payload.Categories;

            Categories = categoryDtos
                .Select(x => new CategoryModel(x))
                .ToList();

            // Cache با DTO کار می‌کند، نه CategoryModel
            _categoryCache.Set(
                CategoryCodes.Service,
                categoryDtos);
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion

    #region Category Selection

    [RelayCommand]
    private async Task SelectCategoryAsync(
        CategoryModel category)
    {
        if (IsBusy)
            return;

        if (category is null)
            return;

        if (SelectedCategory?.Id == category.Id)
            return;

        IsBusy = true;
        // قبلی را از حالت انتخاب خارج کن
        if (SelectedCategory is not null)
            SelectedCategory.IsSelected = false;

        // جدید را انتخاب کن
        category.IsSelected = true;
        SelectedCategory = category;

        SubCategories = [];
        Services = [];

        IsSubCategoriesVisible = false;
        IsServicesVisible = false;

        try
        {
            var result =
                await _categoryApiService.GetChildrenAsync(
                    category.Code,
                    CancellationToken.None);

            if (!result.IsSuccess)
                return;

            var subCategoryDtos = result.Payload?.Categories;

            // Category دارای زیرمجموعه است
            if (subCategoryDtos is { Count: > 0 })
            {
                SubCategories = subCategoryDtos
                    .Select(x => new CategoryModel(x))
                    .ToList();

                IsSubCategoriesVisible = true;

                return;
            }

            // Category زیرمجموعه ندارد
            // مستقیماً Services را می‌خوانیم

            if (_serviceCache.TryGet(
                    category.Id,
                    out var cachedServices))
            {
                Services = cachedServices
                    .Select(x => new ServiceModel(x))
                    .ToList();
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
                return;
            }

            var serviceDtos = servicesResult.Payload.Services;

            Services = serviceDtos
                .Select(x => new ServiceModel(x))
                .ToList();

            _serviceCache.Set(
                category.Id,
                serviceDtos);

            IsServicesVisible = true;
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion

    #region SubCategory Selection
    [RelayCommand]
    private async Task SelectSubCategoryAsync(
        CategoryModel category)
    {
        if (IsBusy)
            return;

        if (category is null)
            return;

        if (SelectedSubCategory is not null)
            SelectedSubCategory.IsSelected = false;

        IsBusy = true;
        category.IsSelected = true;
        SelectedSubCategory = category;

        Services = [];
        IsServicesVisible = false;
        try
        {
            if (_serviceCache.TryGet(
                    category.Id,
                    out var cachedServices))
            {
                Services = cachedServices
                     .Select(x => new ServiceModel(x))
                     .ToList();
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

            var serviceDtos = result.Payload.Services;

            Services = serviceDtos
                .Select(x => new ServiceModel(x))
                .ToList();

            _serviceCache.Set(
                category.Id,
                serviceDtos);

            IsServicesVisible = true;
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion

    #region Service Selection

    [RelayCommand]
    private Task SelectServiceAsync(ServiceModel service)
    {
        if (service is null)
            return Task.CompletedTask;

        _bookingSelectionState.Current.Service = service;

        return _navigationService.GoToBranchSelectionAsync();
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