using BeautySalonBooking.Domain.GeographyAggregate.Entities;

namespace BeautySalonBooking.Domain.GeographyAggregate.Repositories;

public interface IRegionRepository
{
    Task<List<Region>> GetRootRegionsAsync();

    Task<List<Region>> GetChildRegionsAsync(int parentId);
}