using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Contracts.Region;
using BeautySalonBooking.Domain.SharedKernel;

namespace BeautySalonBooking.Application.Regions
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository _regionRepository;

        public RegionService(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<ApiResponse<RegionsResponse>> GetRegionsAsync()
        {
            var regions = await _regionRepository.GetRootRegionsAsync();

            return new ApiResponse<RegionsResponse>
            {
                IsSuccess = true,
                Data = new RegionsResponse
                {
                    Regions = regions.Select(ToDto).ToList()
                }
            };
        }

        public async Task<ApiResponse<RegionsResponse>> GetChildRegionsAsync(GetChildRegionsRequest request)
        {
            var regions = await _regionRepository.GetChildRegionsAsync(request.ParentId);

            return new ApiResponse<RegionsResponse>
            {
                IsSuccess = true,
                Data = new RegionsResponse
                {
                    Regions = regions.Select(ToDto).ToList()
                }
            };
        }

        private static RegionDto ToDto(Region region)
        {
            return new RegionDto
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code
            };
        }
    }
}