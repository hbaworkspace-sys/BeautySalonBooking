using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Contracts.Branch.BranchMemberService.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeautySalonBooking.Maui.Features.Branch.Models;

public sealed partial class BranchMemberServiceModel : ObservableObject
{
    private readonly BranchMemberServiceDto _dto;

    public BranchMemberServiceModel(BranchMemberServiceDto dto)
    {
        _dto = dto;
    }

    public BranchMemberServiceDto Dto => _dto;

    public long BranchMemberId => _dto.BranchMemberId;
    public long BranchMemberServiceId => _dto.BranchMemberServiceId;
    public long PersonId => _dto.PersonId;

    public string FirstName => _dto.FirstName;
    public string LastName => _dto.LastName;
    public string FullName => _dto.FullName;
    public decimal Price => _dto.Price;
    public TimeSpan Duration => _dto.Duration;

    public IReadOnlyList<UserMediaDto> Media => _dto.Media;

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