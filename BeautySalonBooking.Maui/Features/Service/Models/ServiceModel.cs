using BeautySalonBooking.Contracts.Service.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeautySalonBooking.Maui.Features.Service.Models;

public sealed partial class ServiceModel : ObservableObject
{
    private readonly ServiceDto _dto;

    public ServiceModel(ServiceDto dto)
    {
        _dto = dto;
    }

    public ServiceDto Dto => _dto;

    public long Id => _dto.Id;
    public int CategoryId => _dto.CategoryId;
    public string Title => _dto.Title;
    public string Code => _dto.Code;
    public string? Description => _dto.Description;
    public decimal BasePrice => _dto.BasePrice;
    public TimeSpan BaseDuration => _dto.BaseDuration;
    public IReadOnlyList<ServiceMediaDto> Media => _dto.Media;

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