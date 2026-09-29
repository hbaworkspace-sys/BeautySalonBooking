using BeautySalonBooking.Contracts.Category.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeautySalonBooking.Maui.Features.Category.Models;

public sealed partial class CategoryModel : ObservableObject
{
    private readonly CategoryDto _dto;

    public CategoryModel(CategoryDto dto)
    {
        _dto = dto;
    }

    public CategoryDto Dto => _dto;

    public int Id => _dto.Id;
    public string Title => _dto.Title;
    public string Code => _dto.Code;
    public IReadOnlyList<CategoryMediaDto> Media => _dto.Media;

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

    [ObservableProperty]
    private bool isSelected;
}
