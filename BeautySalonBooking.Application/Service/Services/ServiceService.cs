
using BeautySalonBooking.Application.Service.Interfaces;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Contracts.Service.Dtos;
using BeautySalonBooking.Contracts.Service.Requests;
using BeautySalonBooking.Contracts.Service.Responses;
using BeautySalonBooking.Domain.ServiceAggregate.Repositories;


namespace BeautySalonBooking.Application.Service.Services;

public sealed class ServiceService : IServiceService
{
    private readonly IServiceRepository _serviceRepository;

    public ServiceService(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public async Task<ApiResponse_New<GetServicesByCategoryResponse>> GetByCategoryAsync(
        GetServicesByCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var services = await _serviceRepository.GetByCategoryIdAsync(
            request.CategoryId,
            cancellationToken);

        var response = new GetServicesByCategoryResponse
        {
            Services = services
                .Select(serviceItem => new ServiceDto
                {
                    Id = serviceItem.Id,
                    CategoryId = serviceItem.CategoryId,
                    Title = serviceItem.Title,
                    Code = serviceItem.Code,
                    Description = serviceItem.Description,
                    BasePrice = serviceItem.BasePrice,
                    BaseDuration = serviceItem.BaseDuration
                })
                .ToList()
        };

        return new ApiResponse_New<GetServicesByCategoryResponse>
        {
            IsSuccess = true,
            Code = 200,
            Payload = response
        };
    }
}