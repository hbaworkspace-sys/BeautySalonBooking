using BeautySalonBooking.Contracts.Category.Dtos;
namespace BeautySalonBooking.Maui.Features.Category.Cache;

public interface ICategoryCache
{
    bool TryGet(
        string parentCode,
        out IReadOnlyList<CategoryDto> categories);

    void Set(
        string parentCode,
        IReadOnlyList<CategoryDto> categories);

    void Clear(
        string parentCode);

    void ClearAll();
}