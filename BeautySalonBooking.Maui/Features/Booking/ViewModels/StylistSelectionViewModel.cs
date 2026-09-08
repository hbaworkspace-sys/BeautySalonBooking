using BeautySalonBooking.Contracts.Branch.BranchMemberService.Dtos;
using BeautySalonBooking.Contracts.Branch.BranchMemberService.Requests;
using BeautySalonBooking.Contracts.Branch.BranchService.Dtos;
using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Booking.Services;
using BeautySalonBooking.Maui.Features.Branch.BranchMemberService.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Booking.ViewModels;

public partial class StylistSelectionViewModel :
    ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IBranchMemberServiceApiService _branchMemberServiceApiService;
    private readonly IBookingSelectionState _bookingSelectionState;

    public StylistSelectionViewModel(
        INavigationService navigationService,
        IBranchMemberServiceApiService branchMemberServiceApiService,
        IBookingSelectionState bookingSelectionState)
    {
        _navigationService = navigationService;
        _branchMemberServiceApiService = branchMemberServiceApiService;
        _bookingSelectionState = bookingSelectionState;
    }

    #region Selected Service

    public long ServiceId =>
        _bookingSelectionState.Current.Service?.Id ?? 0;

    #endregion

    #region Selected Branch

    public BranchServiceOrganizationDto? SelectedBranch =>
        _bookingSelectionState.Current.Branch;

    #endregion

    #region Members

    [ObservableProperty]
    private IReadOnlyList<BranchMemberServiceDto> members = [];

    [ObservableProperty]
    private bool isMembersVisible;

    [ObservableProperty]
    private bool isLoading;

    #endregion

    #region Selected Member

    public BranchMemberServiceDto? SelectedMember =>
        _bookingSelectionState.Current.Stylist;

    #endregion

    #region Load Members

    public async Task LoadMembersAsync(
        CancellationToken cancellationToken = default)
    {
        var branch = _bookingSelectionState.Current.Branch;
        var service = _bookingSelectionState.Current.Service;

        if (branch is null)
            return;

        if (branch.BranchId <= 0)
            return;

        if (service is null)
            return;

        if (service.Id <= 0)
            return;

        IsLoading = true;
        IsMembersVisible = false;

        try
        {
            var request = new GetMembersByBranchAndServiceRequest
            {
                BranchId = branch.BranchId,
                ServiceId = service.Id
            };

            var result =
                await _branchMemberServiceApiService.GetAsync(
                    request,
                    cancellationToken);

            if (!result.IsSuccess ||
                result.Payload?.Members is null)
            {
                Members = [];
                return;
            }

            Members = result.Payload.Members;

            IsMembersVisible =
                Members.Count > 0;
        }
        finally
        {
            IsLoading = false;
        }
    }

    #endregion

    #region Member Selection

    [RelayCommand]
    private async Task SelectMemberAsync(
        BranchMemberServiceDto member)
    {
        if (member is null)
            return;

        // ذخیره استایلیست انتخاب‌شده
        _bookingSelectionState.Current.Stylist = member;

        // اطلاع‌رسانی به UI
        OnPropertyChanged(nameof(SelectedMember));

        await _navigationService
            .GoToBookingDateTimeSelectionAsync();
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