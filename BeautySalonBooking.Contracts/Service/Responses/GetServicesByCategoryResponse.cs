using BeautySalonBooking.Contracts.Service.Dtos;

namespace BeautySalonBooking.Contracts.Service.Responses;

public sealed class GetServicesByCategoryResponse
{
    public IReadOnlyList<ServiceDto> Services { get; init; } = [];
}