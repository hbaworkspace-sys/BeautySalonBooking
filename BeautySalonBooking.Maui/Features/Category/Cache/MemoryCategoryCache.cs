using BeautySalonBooking.Contracts.Category.Dtos;

namespace BeautySalonBooking.Maui.Features.Category.Cache;

public sealed class MemoryCategoryCache : ICategoryCache
{
    private readonly Dictionary<string, IReadOnlyList<CategoryDto>> _cache =
        new(StringComparer.OrdinalIgnoreCase);

    public bool TryGet(
        string parentCode,
        out IReadOnlyList<CategoryDto> categories)
    {
        return _cache.TryGetValue(parentCode, out categories!);
    }

    public void Set(
        string parentCode,
        IReadOnlyList<CategoryDto> categories)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(parentCode);
        ArgumentNullException.ThrowIfNull(categories);

        _cache[parentCode] = categories;
    }

    public void Clear(string parentCode)
    {
        if (string.IsNullOrWhiteSpace(parentCode))
            return;

        _cache.Remove(parentCode);
    }

    public void ClearAll()
    {
        _cache.Clear();
    }
}