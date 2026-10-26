using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Contracts.Service.Responses;

namespace BeautySalonBooking.WebApp.Interfaces.Services
{
    public interface IServiceApiService
    {
        Task<ApiResponse_New<GetServicesByCategoryResponse>> GetByCategoryAsync(
            int categoryId,
            CancellationToken cancellationToken = default);
    }
}
