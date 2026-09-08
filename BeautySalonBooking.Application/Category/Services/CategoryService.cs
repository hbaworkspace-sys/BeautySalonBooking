using BeautySalonBooking.Application.Category.Interfaces;
using BeautySalonBooking.Contracts.Category.Dtos;
using BeautySalonBooking.Contracts.Category.Requests;
using BeautySalonBooking.Contracts.Category.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Domain.CategoryAggregate.Repositories;

namespace BeautySalonBooking.Application.Category.Services;

public sealed class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ApiResponse_New<GetChildCategoriesResponse>> GetChildrenAsync(
        GetChildCategoriesRequest request,
        CancellationToken cancellationToken)
    {
        var parent = await _categoryRepository.GetByCodeAsync(
                  request.ParentCode,
                  cancellationToken);

        if (parent is null)
        {
            return new ApiResponse_New<GetChildCategoriesResponse>
            {
                IsSuccess = false,
                Code = 404,
                Message = "دسته‌بندی والد یافت نشد."
            };
        }

        var categories = await _categoryRepository.GetChildrenAsync(
            parent.Id,
            cancellationToken);

        var response = new GetChildCategoriesResponse
        {
            Categories = categories
                .Select(category => new CategoryDto
                {
                    Id = category.Id,
                    Title = category.Title,
                    Code = category.Code,
                    Description = category.Description,
                    DisplayOrder = category.DisplayOrder
                })
                .ToList()
        };

        return new ApiResponse_New<GetChildCategoriesResponse>
        {
            IsSuccess = true,
            Code = 200,
            Payload = response
        };
    }
}