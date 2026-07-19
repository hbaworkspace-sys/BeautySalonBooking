namespace BeautySalonBooking.Contracts.Region;

public class RegionsResponse
{
    public IReadOnlyCollection<RegionDto> Regions { get; init; } = [];
}