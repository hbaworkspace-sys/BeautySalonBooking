using BeautySalonBooking.Contracts.Category.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.WebApp.Constants;
using BeautySalonBooking.WebApp.Interfaces.Categories;
using BeautySalonBooking.WebApp.Interfaces.Common;

namespace BeautySalonBooking.WebApp.Services.Categories;

public class CategoryApiService : ICategoryApiService
{
    private readonly IApiClient _apiClient;
    private readonly ILogger<CategoryApiService> _logger;

    public CategoryApiService(IApiClient apiClient, ILogger<CategoryApiService> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    public async Task<ApiResponse_New<GetChildCategoriesResponse>> GetChildrenAsync(
      string parentCode,
      CancellationToken cancellationToken = default)
    {
        try
        {
            var endpoint = $"{CategoryRoutes.Children}?parentCode={Uri.EscapeDataString(parentCode)}";

            // ✅ اندپوینت public — نیازی به لاگین ندارد
            return await _apiClient.GetPublicAsync<ApiResponse_New<GetChildCategoriesResponse>>(
                endpoint, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting children for parentCode: {ParentCode}", parentCode);
            return new ApiResponse_New<GetChildCategoriesResponse>
            {
                IsSuccess = false,
                Message = "خطا در دریافت دسته‌بندی‌ها"
            };
        }
    }
}