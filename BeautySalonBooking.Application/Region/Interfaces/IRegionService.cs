using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Contracts.Region;

namespace BeautySalonBooking.Application.Regions
{
    public interface IRegionService
    {
        Task<ApiResponse<RegionsResponse>> GetRegionsAsync();
        Task<ApiResponse<RegionsResponse>> GetChildRegionsAsync(GetChildRegionsRequest request);
    }
}