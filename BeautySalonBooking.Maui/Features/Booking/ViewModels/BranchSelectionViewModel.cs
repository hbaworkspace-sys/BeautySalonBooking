using BeautySalonBooking.Contracts.Branch.BranchService.Dtos;
using BeautySalonBooking.Contracts.Service.Dtos;
using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Booking.Services;
using BeautySalonBooking.Maui.Features.Branch.BranchService.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Booking.ViewModels;

public partial class BranchSelectionViewModel :
    ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IBranchServiceApiService _branchServiceApiService;
    private readonly IBookingSelectionState _bookingSelectionState;

    public BranchSelectionViewModel(
        INavigationService navigationService,
        IBranchServiceApiService branchServiceApiService,
        IBookingSelectionState bookingSelectionState)
    {
        _navigationService = navigationService;
        _branchServiceApiService = branchServiceApiService;
        _bookingSelectionState = bookingSelectionState;
    }

    #region Selected Service

    public ServiceDto? SelectedService =>
        _bookingSelectionState.Current.Service;
    #endregion

    #region Branches

    [ObservableProperty]
    private IReadOnlyList<BranchServiceOrganizationDto> branches = [];

    [ObservableProperty]
    private bool isBranchesVisible;

    [ObservableProperty]
    private bool isLoading;

    #endregion

    #region Selected Branch

    public BranchServiceOrganizationDto? SelectedBranch =>
        _bookingSelectionState.Current.Branch;

    #endregion

    #region Load Organizations

    public async Task LoadOrganizationsAsync(
        CancellationToken cancellationToken = default)
    {
        var service = _bookingSelectionState.Current.Service;

        if (service is null)
            return;

        if (service.Id <= 0)
            return;

        IsLoading = true;
        IsBranchesVisible = false;

        try
        {
            var result =
                await _branchServiceApiService
                    .GetOrganizationsByServiceAsync(
                        service.Id,
                        cancellationToken);

            if (!result.IsSuccess ||
                result.Payload?.Organizations is null)
            {
                Branches = [];
                return;
            }

            Branches = result.Payload.Organizations;

            IsBranchesVisible =
                Branches.Count > 0;
        }
        finally
        {
            IsLoading = false;
        }
    }

    #endregion

    #region Branch Selection

    [RelayCommand]
    private async Task SelectBranchAsync(
        BranchServiceOrganizationDto branch)
    {
        if (branch is null)
            return;

        // ذخیره شعبه انتخاب‌شده در Booking State
        _bookingSelectionState.Current.Branch = branch;

        // برای UI
        OnPropertyChanged(nameof(SelectedBranch));

        await _navigationService
            .GoToStylistSelectionAsync();
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