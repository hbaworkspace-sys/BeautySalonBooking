using BeautySalonBooking.Application.Category.Interfaces;
using BeautySalonBooking.Contracts.Category.Dtos;
using BeautySalonBooking.Contracts.Category.Requests;
using BeautySalonBooking.Contracts.Category.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Domain.Base.UnitOfWork;
using BeautySalonBooking.Domain.CategoryAggregate.Entities;
using BeautySalonBooking.Domain.CategoryAggregate.Repositories;

namespace BeautySalonBooking.Application.Category.Services;

public sealed class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    // ==================== لیست ریشه‌ها ====================
    public async Task<ApiResponse_New<GetRootCategoriesResponse>> GetRootsAsync(
        GetRootCategoriesRequest request, CancellationToken ct = default)
    {
        var categories = await _categoryRepository.GetRootsAsync(request.Type, ct);

        var result = new List<CategoryDto>();
        foreach (var c in categories)
        {
            var hasChildren = await _categoryRepository.HasChildrenAsync(c.Id, ct);
            result.Add(MapToDto(c, hasChildren));
        }

        return new ApiResponse_New<GetRootCategoriesResponse>
        {
            IsSuccess = true,
            Code = 200,
            Payload = new GetRootCategoriesResponse { Categories = result }
        };
    }

    // ==================== لیست فرزندان ====================
    public async Task<ApiResponse_New<GetChildCategoriesResponse>> GetChildrenAsync(
        GetChildCategoriesRequest request, CancellationToken ct = default)
    {
        var parent = await _categoryRepository.GetByCodeAsync(request.ParentCode, ct);
        if (parent is null)
        {
            return new ApiResponse_New<GetChildCategoriesResponse>
            {
                IsSuccess = false,
                Code = 404,
                Message = "دسته‌بندی والد یافت نشد."
            };
        }

        var categories = await _categoryRepository.GetChildrenAsync(parent.Id, ct);

        var result = new List<CategoryDto>();
        foreach (var c in categories)
        {
            var hasChildren = await _categoryRepository.HasChildrenAsync(c.Id, ct);
            result.Add(MapToDto(c, hasChildren));
        }

        return new ApiResponse_New<GetChildCategoriesResponse>
        {
            IsSuccess = true,
            Code = 200,
            Payload = new GetChildCategoriesResponse { Categories = result }
        };
    }

    // ==================== درج ====================
    public async Task<ApiResponse_New<int>> CreateAsync(
    CreateCategoryRequest request, CancellationToken ct = default)
    {
        try
        {
            if (await _categoryRepository.CodeExistsAsync(request.Code, null, ct))
            {
                return new ApiResponse_New<int>
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = "کد دسته‌بندی تکراری است."
                };
            }

            BeautySalonBooking.Domain.CategoryAggregate.Entities.Category entity;

            if (request.ParentId.HasValue)
            {
                var parent = await _categoryRepository.GetByIdAsync(request.ParentId.Value, ct);
                if (parent == null)
                {
                    return new ApiResponse_New<int>
                    {
                        IsSuccess = false,
                        Code = 404,
                        Message = "دسته‌بندی والد یافت نشد."
                    };
                }

                entity = new BeautySalonBooking.Domain.CategoryAggregate.Entities.Category(
                    title: request.Title,
                    code: request.Code,
                    type: (BeautySalonBooking.Domain.CategoryAggregate.Enums.CategoryType)request.Type,
                    parentId: parent.Id,
                    parentLevel: parent.Level,
                    description: request.Description,
                    displayOrder: request.DisplayOrder,
                    glyph: request.Glyph);
            }
            else
            {
                entity = new BeautySalonBooking.Domain.CategoryAggregate.Entities.Category(
                    title: request.Title,
                    code: request.Code,
                    type: (BeautySalonBooking.Domain.CategoryAggregate.Enums.CategoryType)request.Type,
                    description: request.Description,
                    displayOrder: request.DisplayOrder,
                    glyph: request.Glyph);
            }

            // 👇 پردازش تصاویر (این بخش قبلاً وجود نداشت)
            if (request.Media != null)
            {
                foreach (var media in request.Media)
                {
                    byte[] content;
                    try
                    {
                        var base64 = media.Base64Content.Contains(",")
                            ? media.Base64Content.Split(',')[1]
                            : media.Base64Content;
                        content = Convert.FromBase64String(base64);
                    }
                    catch (FormatException)
                    {
                        return new ApiResponse_New<int>
                        {
                            IsSuccess = false,
                            Code = 400,
                            Message = $"فرمت Base64 برای فایل {media.FileName} نامعتبر است."
                        };
                    }

                    var categoryMedia = new CategoryMedia(
                        fileName: media.FileName,
                        contentType: media.ContentType,
                        content: content,
                        displayOrder: media.DisplayOrder);

                    entity.AddMedia(categoryMedia);
                }
            }

            await _categoryRepository.AddAsync(entity, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return new ApiResponse_New<int>
            {
                IsSuccess = true,
                Code = 201,
                Message = "دسته‌بندی با موفقیت ایجاد شد.",
                Payload = entity.Id
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse_New<int>
            {
                IsSuccess = false,
                Code = 400,
                Message = ex.InnerException?.Message ?? ex.Message
            };
        }
    }
    // ==================== ویرایش ====================
    public async Task<ApiResponse_New<bool>> UpdateAsync(
     UpdateCategoryRequest request, CancellationToken ct = default)
    {
        try
        {
            // 👈 استفاده از متد جدید که Media را Include می‌کند
            var entity = await _categoryRepository.GetWithMediaAsync(request.Id, ct);

            if (entity == null)
            {
                return new ApiResponse_New<bool> { IsSuccess = false, Code = 404, Message = "دسته‌بندی یافت نشد." };
            }

            if (await _categoryRepository.CodeExistsAsync(request.Code, request.Id, ct))
            {
                return new ApiResponse_New<bool> { IsSuccess = false, Code = 400, Message = "کد دسته‌بندی تکراری است." };
            }

            // 1. آپدیت فیلدهای متنی
            entity.Update(
                title: request.Title,
                code: request.Code,
                type: (BeautySalonBooking.Domain.CategoryAggregate.Enums.CategoryType)request.Type,
                description: request.Description,
                displayOrder: request.DisplayOrder,
                glyph: request.Glyph);

            // 👈 2. حذف تصاویری که در MediaIdsToKeep نیستند
            // نکته: اگر request.MediaIdsToKeep خالی باشد، یعنی همه تصاویر باید حذف شوند (یا هیچکدام؟)
            // اگر خالی باشد یعنی کاربر همه را حذف کرده است.
            var mediaToRemove = entity.Media
                .Where(m => !request.MediaIdsToKeep.Contains(m.Id))
                .ToList();

            foreach (var media in mediaToRemove)
            {
                entity.RemoveMedia(media);
            }

            // 👈 3. اضافه کردن تصاویر جدید
            foreach (var media in request.Media) // توجه: نام فیلد را به Media تغییر دادیم
            {
                byte[] content;
                try
                {
                    var base64 = media.Base64Content.Contains(",")
                        ? media.Base64Content.Split(',')[1]
                        : media.Base64Content;
                    content = Convert.FromBase64String(base64);
                }
                catch (FormatException)
                {
                    return new ApiResponse_New<bool>
                    {
                        IsSuccess = false,
                        Code = 400,
                        Message = $"فرمت Base64 برای فایل {media.FileName} نامعتبر است."
                    };
                }

                var categoryMedia = new CategoryMedia(
                    fileName: media.FileName,
                    contentType: media.ContentType,
                    content: content,
                    displayOrder: media.DisplayOrder);

                entity.AddMedia(categoryMedia);
            }

            _categoryRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync(ct);

            return new ApiResponse_New<bool>
            {
                IsSuccess = true,
                Code = 200,
                Message = "دسته‌بندی با موفقیت ویرایش شد.",
                Payload = true
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse_New<bool>
            {
                IsSuccess = false,
                Code = 400,
                Message = ex.InnerException?.Message ?? ex.Message
            };
        }
    }

    // ==================== حذف ====================
    public async Task<ApiResponse_New<bool>> DeleteAsync(int id, CancellationToken ct = default)
    {
        try
        {
            var entity = await _categoryRepository.GetByIdAsync(id, ct);
            if (entity == null)
            {
                return new ApiResponse_New<bool>
                {
                    IsSuccess = false,
                    Code = 404,
                    Message = "دسته‌بندی یافت نشد."
                };
            }

            // 👈 چک کردن وجود فرزند
            if (await _categoryRepository.HasChildrenAsync(id, ct))
            {
                return new ApiResponse_New<bool>
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = "این دسته‌بندی دارای فرزند است و قابل حذف نیست. ابتدا فرزندان را حذف یا منتقل کنید."
                };
            }

            // 👈 چک کردن وجود سرویس
            if (await _categoryRepository.HasServicesAsync(id, ct))
            {
                return new ApiResponse_New<bool>
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = "این دسته‌بندی دارای سرویس است و قابل حذف نیست. ابتدا سرویس‌ها را منتقل یا حذف کنید."
                };
            }

            _categoryRepository.Delete(entity);
            await _unitOfWork.SaveChangesAsync(ct);

            return new ApiResponse_New<bool>
            {
                IsSuccess = true,
                Code = 200,
                Message = "دسته‌بندی با موفقیت حذف شد.",
                Payload = true
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse_New<bool>
            {
                IsSuccess = false,
                Code = 400,
                Message = ex.InnerException?.Message ?? ex.Message
            };
        }
    }

    // ==================== Helper ====================
    private static CategoryDto MapToDto(BeautySalonBooking.Domain.CategoryAggregate.Entities.Category category, bool hasChildren)
    {
        return new CategoryDto
        {
            Id = category.Id,
            ParentId = category.ParentId,
            Title = category.Title,
            Code = category.Code,
            Level = category.Level,
            Type = (int)category.Type,
            Description = category.Description,
            DisplayOrder = category.DisplayOrder,
            Glyph = category.Glyph,
            HasChildren = hasChildren,
            Media = category.Media
                .OrderBy(m => m.DisplayOrder)
                .Select(m => new CategoryMediaDto
                {
                    Id = m.Id,
                    FileName = m.FileName,
                    ContentType = m.ContentType,
                    FileSize = m.FileSize,
                    Content = m.Content,
                    DisplayOrder = m.DisplayOrder
                }).ToList()
        };
    }
}
