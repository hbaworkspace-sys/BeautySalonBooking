using BeautySalonBooking.Contracts.Category.Responses;
using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.Maui.Features.Category.Services;

public interface ICategoryApiService
{
    Task<ApiResponse_New<GetChildCategoriesResponse>> GetChildrenAsync(
        string parentCode,
        CancellationToken cancellationToken = default);
}
