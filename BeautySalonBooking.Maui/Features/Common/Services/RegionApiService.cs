using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Contracts.Region;
using BeautySalonBooking.Maui.Common.Services;
using BeautySalonBooking.Maui.Features.Region.Constants;

namespace BeautySalonBooking.Maui.Features.Region
{
    public class RegionApiService : BaseApiService
    {
        public RegionApiService(HttpClient httpClient)
            : base(httpClient)
        {
        }

        public Task<ApiResponse<RegionsResponse>> GetRegionsAsync()
        {
            return SendAsync<object, RegionsResponse>(
                HttpMethod.Get,
                RegionRoutes.GetRegions,
                null);
        }
        public Task<ApiResponse<RegionsResponse>> GetChildRegionsAsync(GetChildRegionsRequest request)
        {
            var url = $"{RegionRoutes.GetChildRegions}?ParentId={request.ParentId}";

            return SendAsync<object, RegionsResponse>(
                HttpMethod.Get,
                url,
                null);
        }
    }
}