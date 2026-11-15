using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.WebApp.Interfaces.Categories;
using BeautySalonBooking.WebApp.Interfaces.Common;
using BeautySalonBooking.WebApp.Models.Category;
using BeautySalonBooking.WebApp.Settings;
using Microsoft.Extensions.Options;
namespace BeautySalonBooking.WebApp.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IApiClient _apiClient;
        private readonly ApiSettings _settings;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(
            IApiClient apiClient,
            IOptions<ApiSettings> settings,
            ILogger<CategoryService> logger)
        {
            _apiClient = apiClient;
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task<List<CategoryDto>> GetRootsAsync(int? type = null)
        {
            try
            {
                var endpoint = $"{_settings.Endpoints.Category.GetRoots}";
                if (type.HasValue)
                    endpoint += $"?type={type.Value}";

                var response = await _apiClient.GetAsync<ApiResponse_New<GetRootCategoriesResponse>>(endpoint);
                return response?.Payload?.Categories ?? new List<CategoryDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting roots");
                return new List<CategoryDto>();
            }
        }

        public async Task<List<CategoryDto>> GetChildrenAsync(string parentCode)
        {
            try
            {
                var endpoint = $"{_settings.Endpoints.Category.GetChildren}?parentCode={parentCode}";
                var response = await _apiClient.GetAsync<ApiResponse_New<GetChildCategoriesResponse>>(endpoint);
                return response?.Payload?.Categories ?? new List<CategoryDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting children for {Code}", parentCode);
                return new List<CategoryDto>();
            }
        }

        public async Task<int> CreateAsync(CreateCategoryRequest request)
        {
            var endpoint = _settings.Endpoints.Category.Create;
            var response = await _apiClient.PostAsync<CreateCategoryRequest, ApiResponse_New<int>>(endpoint, request);

            if (response?.IsSuccess == true)
                return response.Payload;

            throw new Exception(response?.Message ?? "خطا در ایجاد دسته‌بندی");
        }

        public async Task<bool> UpdateAsync(UpdateCategoryRequest request)
        {
            var endpoint = _settings.Endpoints.Category.Update;
            var response = await _apiClient.PutAsync<UpdateCategoryRequest, ApiResponse_New<bool>>(endpoint, request);
            return response?.IsSuccess == true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var endpoint = $"{_settings.Endpoints.Category.Delete}/{id}";
            var response = await _apiClient.DeleteAsync<ApiResponse_New<bool>>(endpoint);
            return response?.IsSuccess == true;
        }
    }
}
