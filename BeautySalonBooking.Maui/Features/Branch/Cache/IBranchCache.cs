using BeautySalonBooking.Contracts.Branch.Branch.Dtos;

namespace BeautySalonBooking.Maui.Features.Branch.Cache;

public interface IBranchCache
{
    bool TryGet(
        string key,
        out IReadOnlyList<BranchDto> branches);

    void Set(
        string key,
        IReadOnlyList<BranchDto> branches);

    void Clear(
        string key);

    void ClearAll();
}