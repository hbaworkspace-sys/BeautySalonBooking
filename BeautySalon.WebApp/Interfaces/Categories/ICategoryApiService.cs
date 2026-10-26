using BeautySalonBooking.Contracts.Category.Responses;
using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.WebApp.Interfaces.Categories
{
    public interface ICategoryApiService
    {
        Task<ApiResponse_New<GetChildCategoriesResponse>> GetChildrenAsync(
            string parentCode,
            CancellationToken cancellationToken = default);
    }
}
