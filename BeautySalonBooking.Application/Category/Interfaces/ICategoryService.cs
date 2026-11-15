using BeautySalonBooking.Contracts.Category.Requests;
using BeautySalonBooking.Contracts.Category.Responses;
using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.Application.Category.Interfaces;

public interface ICategoryService
{

    Task<ApiResponse_New<GetRootCategoriesResponse>> GetRootsAsync(
       GetRootCategoriesRequest request, CancellationToken ct = default);

    Task<ApiResponse_New<GetChildCategoriesResponse>> GetChildrenAsync(
        GetChildCategoriesRequest request, CancellationToken ct = default);

    Task<ApiResponse_New<int>> CreateAsync(
        CreateCategoryRequest request, CancellationToken ct = default);

    Task<ApiResponse_New<bool>> UpdateAsync(
        UpdateCategoryRequest request, CancellationToken ct = default);

    Task<ApiResponse_New<bool>> DeleteAsync(int id, CancellationToken ct = default);
}