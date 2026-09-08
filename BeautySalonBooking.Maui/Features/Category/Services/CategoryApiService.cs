using BeautySalonBooking.Contracts.Category.Requests;
using BeautySalonBooking.Contracts.Category.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Maui.Common.Services;
using BeautySalonBooking.Maui.Features.Category.Constants;
using Microsoft.Extensions.Logging;

namespace BeautySalonBooking.Maui.Features.Category.Services;

public sealed class CategoryApiService : BaseApiService, ICategoryApiService
{
    public CategoryApiService(
        HttpClient httpClient,
        ILogger<CategoryApiService> logger)
        : base(httpClient, logger)
    {
    }

    public Task<ApiResponse_New<GetChildCategoriesResponse>> GetChildrenAsync(
        string parentCode,
        CancellationToken cancellationToken = default)
        => GetAsync<GetChildCategoriesResponse>(
            $"{CategoryRoutes.Children}?parentCode={Uri.EscapeDataString(parentCode)}",
            cancellationToken);
}