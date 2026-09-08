using BeautySalonBooking.Contracts.Category.Requests;
using BeautySalonBooking.Contracts.Category.Responses;
using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.Application.Category.Interfaces;

public interface ICategoryService
{
    /// <summary>
    /// دریافت دسته‌بندی‌های خدمات بر اساس کد دسته‌بندی والد.
    /// </summary>
    Task<ApiResponse_New<GetChildCategoriesResponse>> GetChildrenAsync(
        GetChildCategoriesRequest request,
        CancellationToken cancellationToken);
}