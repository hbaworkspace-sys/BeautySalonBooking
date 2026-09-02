using BeautySalonBooking.Contracts.Category.Dtos;
using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Category.Cache;
using BeautySalonBooking.Maui.Features.Category.Constants;
using BeautySalonBooking.Maui.Features.Category.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeautySalonBooking.Maui.Features.Home.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly INavigationService _navigationService;
    private readonly ICategoryApiService _categoryApiService;
    private readonly ICategoryCache _categoryCache;

    public HomeViewModel(
        IDialogService dialogService,
        INavigationService navigationService,
        ICategoryApiService categoryApiService,
        ICategoryCache categoryCache)
    {
        _dialogService = dialogService;
        _navigationService = navigationService;
        _categoryApiService = categoryApiService;
        _categoryCache = categoryCache;
    }

    [ObservableProperty]
    private IReadOnlyList<CategoryDto> categories = [];

    [ObservableProperty]
    private bool isLoading;

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
            var result = await _categoryApiService.GetChildrenAsync(
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
}