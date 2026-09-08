using BeautySalonBooking.Contracts.Service.Dtos;

namespace BeautySalonBooking.Maui.Features.Service.Cache;

public sealed class MemoryServiceCache : IServiceCache
{
    private readonly Dictionary<int, IReadOnlyList<ServiceDto>> _cache = new();

    public bool TryGet(
        int categoryId,
        out IReadOnlyList<ServiceDto> services)
    {
        return _cache.TryGetValue(categoryId, out services!);
    }

    public void Set(
        int categoryId,
        IReadOnlyList<ServiceDto> services)
    {
        ArgumentNullException.ThrowIfNull(services);

        _cache[categoryId] = services;
    }

    public void Clear(int categoryId)
    {
        _cache.Remove(categoryId);
    }

    public void ClearAll()
    {
        _cache.Clear();
    }
}