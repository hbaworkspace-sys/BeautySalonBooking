using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.WebApp.Interfaces.Categories;
using BeautySalonBooking.WebApp.Interfaces.Common;
using BeautySalonBooking.WebApp.Models.Category;
using BeautySalonBooking.WebApp.Settings;
using Microsoft.Extensions.Options;

namespace BeautySalonBooking.WebApp.Interfaces.Categories
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetRootsAsync(int? type = null);
        Task<List<CategoryDto>> GetChildrenAsync(string parentCode);
        Task<int> CreateAsync(CreateCategoryRequest request);
        Task<bool> UpdateAsync(UpdateCategoryRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
