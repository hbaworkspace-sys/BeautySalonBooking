using BeautySalonBooking.Contracts.Service.Dtos;

namespace BeautySalonBooking.Maui.Features.Service.Cache;

public interface IServiceCache
{
    bool TryGet(
        int categoryId,
        out IReadOnlyList<ServiceDto> services);

    void Set(
        int categoryId,
        IReadOnlyList<ServiceDto> services);

    void Clear(int categoryId);

    void ClearAll();
}