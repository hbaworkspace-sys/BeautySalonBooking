using BeautySalonBooking.Contracts.Branch.BranchService.Dtos;
using BeautySalonBooking.Contracts.Branch.BranchService.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeautySalonBooking.Maui.Features.Branch.Models;

public sealed partial class BranchServiceOrganizationModel : ObservableObject
{
    private readonly BranchServiceOrganizationDto _dto;

    public BranchServiceOrganizationModel(BranchServiceOrganizationDto dto)
    {
        _dto = dto;
    }

    public BranchServiceOrganizationDto Dto => _dto;

    public long BranchServiceId => _dto.BranchServiceId;
    public long BranchId => _dto.BranchId;
    public string BranchTitle => _dto.BranchTitle;
    public string? BranchDescription => _dto.BranchDescription;

    public long OrganizationId => _dto.OrganizationId;
    public string OrganizationTitle => _dto.OrganizationTitle;
    public OrganizationType OrganizationType => _dto.OrganizationType;

    public decimal Price => _dto.Price;
    public TimeSpan Duration => _dto.Duration;
    public int DisplayOrder => _dto.DisplayOrder;

    public IReadOnlyList<BranchMediaDto> Media => _dto.Media;

    public ImageSource? ImageSource
    {
        get
        {
            var media = _dto.Media
                .OrderBy(x => x.DisplayOrder)
                .FirstOrDefault();

            if (media?.Content is null || media.Content.Length == 0)
                return null;

            return ImageSource.FromStream(
                () => new MemoryStream(media.Content));
        }
    }
}