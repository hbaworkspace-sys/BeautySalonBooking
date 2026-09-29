using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Branch.Cache;
using BeautySalonBooking.Maui.Features.Category.Cache;
using BeautySalonBooking.Maui.Features.Category.Constants;
using BeautySalonBooking.Maui.Features.Category.Models;
using BeautySalonBooking.Maui.Features.Category.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Home.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly INavigationService _navigationService;
    private readonly ICategoryApiService _categoryApiService;
    //    private readonly IBranchAPiService _branchAPiService;
    private readonly ICategoryCache _categoryCache;
    private readonly IBranchCache _branchCache;

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
    private IReadOnlyList<CategoryModel> categories = [];

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private bool isBusy;

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
    public async Task LoadBranchStoryAsync(
        CancellationToken cancellationToken = default)
    {
        //if (IsBusy)
        //    return;

        //if (_branchCache.TryGet(
        //        BranchCacheKeys.Story,
        //        out var cachedBranches))
        //{
        //    Branches = cachedBranches
        //        .Select(x => new BranchModel(x))
        //        .ToList();

        //    return;
        //}

        //IsBusy = true;

        //try
        //{
        //    var result =
        //        await _branchApiService.GetBranchesAsync(
        //            cancellationToken);

        //    if (!result.IsSuccess ||
        //        result.Payload?.Branches is null)
        //    {
        //        return;
        //    }

        //    var branchDtos = result.Payload.Branches;

        //    Branches = branchDtos
        //        .Select(x => new BranchModel(x))
        //        .ToList();

        //    _branchCache.Set(
        //        BranchCacheKeys.Story,
        //        branchDtos);
        //}
        //finally
        //{
        //    IsBusy = false;
        //}
    }
    [RelayCommand]
    private Task GoToServiceSelectionAsync()
    {
        return _navigationService.GoToServiceSelectionAsync();
    }
}