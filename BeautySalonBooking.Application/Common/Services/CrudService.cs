using AutoMapper;
using BeautySalonBooking.Application.Common.Interfaces;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Base.Repositories;
using Core.DTOs;
using Microsoft.Extensions.Logging;

namespace BeautySalonBooking.Application.Common.Services;

//public class CrudService<TEntity, TId, TDto, TCreateRequest, TUpdateRequest>
//    : ICrudService<TEntity, TId, TDto, TCreateRequest, TUpdateRequest>
//    where TEntity : BaseEntity<TId>
//    where TDto : class
//    where TCreateRequest : class
//    where TUpdateRequest : class
//{
//    protected readonly IRepository<TEntity, TId> _repository;
//    protected readonly IMapper _mapper;
//    protected readonly ILogger<CrudService<TEntity, TId, TDto, TCreateRequest, TUpdateRequest>> _logger;

//    public CrudService(
//        IRepository<TEntity, TId> repository,
//        IMapper mapper,
//        ILogger<CrudService<TEntity, TId, TDto, TCreateRequest, TUpdateRequest>> logger)
//    {
//        _repository = repository;
//        _mapper = mapper;
//        _logger = logger;
//    }

//    public virtual async Task<ApiResponse_New<TDto?>> GetByIdAsync(TId id)
//    {
//        try
//        {
//            var entity = await _repository.GetByIdAsync(id);

//            if (entity == null)
//            {
//                return ApiResponse_New<TDto?>.NotFoundResponse($"{typeof(TEntity).Name} with id {id} not found");
//            }

//            var dto = _mapper.Map<TDto>(entity);
//            return ApiResponse_New<TDto?>.SuccessResponse(dto, $"{typeof(TEntity).Name} retrieved successfully");
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "Error getting {EntityName} by id {Id}", typeof(TEntity).Name, id);
//            return ApiResponse_New<TDto?>.FailureResponse($"Error retrieving {typeof(TEntity).Name}", 500);
//        }
//    }

//    public virtual async Task<ApiResponse_New<List<TDto>>> GetAllAsync()
//    {
//        try
//        {
//            var entities = await _repository.GetAllAsync();
//            var dtos = _mapper.Map<List<TDto>>(entities);

//            return ApiResponse_New<List<TDto>>.SuccessResponse(
//                dtos,
//                $"{entities.Count} {typeof(TEntity).Name}(s) retrieved successfully"
//            );
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "Error getting all {EntityName}", typeof(TEntity).Name);
//            return ApiResponse_New<List<TDto>>.FailureResponse($"Error retrieving {typeof(TEntity).Name}s", 500);
//        }
//    }

//    public virtual async Task<ApiResponse_New<PagedResult<TDto>>> GetPagedAsync(PaginationRequest request)
//    {
//        try
//        {
//            var pagedResult = await _repository.GetPagedAsync(request);
//            var items = _mapper.Map<List<TDto>>(pagedResult.Items);

//            var result = new PagedResult<TDto>(
//                items,
//                pagedResult.TotalCount,
//                pagedResult.Page,
//                pagedResult.PageSize
//            );

//            return ApiResponse_New<PagedResult<TDto>>.SuccessResponse(
//                result,
//                $"{typeof(TEntity).Name} page {request.Page} retrieved successfully"
//            );
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "Error getting paged {EntityName}", typeof(TEntity).Name);
//            return ApiResponse_New<PagedResult<TDto>>.FailureResponse($"Error retrieving {typeof(TEntity).Name}s", 500);
//        }
//    }

//    public virtual async Task<ApiResponse_New<TDto>> CreateAsync(TCreateRequest request)
//    {
//        try
//        {
//            // اعتبارسنجی اختصاصی قبل از ایجاد
//            await ValidateCreateRequestAsync(request);

//            var entity = _mapper.Map<TEntity>(request);
//            await _repository.AddAsync(entity);
//            await _repository.SaveChangesAsync();

//            _logger.LogInformation("{EntityName} created with ID: {Id}", typeof(TEntity).Name, entity.Id);

//            var dto = _mapper.Map<TDto>(entity);

//            // ✅ حالا درست کار می‌کنه
//            return ApiResponse_New<TDto>.CreatedResponse(
//                dto,
//                $"{typeof(TEntity).Name} created successfully"
//            );
//        }
//        catch (AppException ex)
//        {
//            _logger.LogWarning(ex, "Business rule violation while creating {EntityName}", typeof(TEntity).Name);
//            return ApiResponse_New<TDto>.FailureResponse(ex.Message, ex.StatusCode, ex.Errors);
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "Error creating {EntityName}", typeof(TEntity).Name);
//            return ApiResponse_New<TDto>.FailureResponse($"Error creating {typeof(TEntity).Name}", 500);
//        }
//    }

//    public virtual async Task<ApiResponse_New<TDto?>> UpdateAsync(TId id, TUpdateRequest request)
//    {
//        try
//        {
//            var entity = await _repository.GetByIdAsync(id);
//            if (entity == null)
//            {
//                return ApiResponse_New<TDto?>.NotFoundResponse($"{typeof(TEntity).Name} with id {id} not found");
//            }

//            // اعتبارسنجی اختصاصی قبل از آپدیت
//            await ValidateUpdateRequestAsync(id, request);

//            _mapper.Map(request, entity);
//            _repository.Update(entity);
//            await _repository.SaveChangesAsync();

//            var dto = _mapper.Map<TDto>(entity);
//            return ApiResponse_New<TDto?>.SuccessResponse(dto, $"{typeof(TEntity).Name} updated successfully");
//        }
//        catch (AppException ex)
//        {
//            _logger.LogWarning(ex, "Business rule violation while updating {EntityName} with id {Id}", typeof(TEntity).Name, id);
//            return ApiResponse_New<TDto?>.FailureResponse(ex.Message, ex.StatusCode, ex.Errors);
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "Error updating {EntityName} with id {Id}", typeof(TEntity).Name, id);
//            return ApiResponse_New<TDto?>.FailureResponse($"Error updating {typeof(TEntity).Name}", 500);
//        }
//    }

//    public virtual async Task<ApiResponse_New<bool>> DeleteAsync(TId id)
//    {
//        try
//        {
//            var entity = await _repository.GetByIdAsync(id);
//            if (entity == null)
//            {
//                return ApiResponse_New<bool>.NotFoundResponse($"{typeof(TEntity).Name} with id {id} not found");
//            }

//            var result = await _repository.DeleteAsync(id);
//            if (result)
//            {
//                await _repository.SaveChangesAsync();
//                return ApiResponse_New<bool>.SuccessResponse(true, $"{typeof(TEntity).Name} deleted successfully");
//            }

//            return ApiResponse_New<bool>.FailureResponse($"Error deleting {typeof(TEntity).Name}", 500);
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "Error deleting {EntityName} with id {Id}", typeof(TEntity).Name, id);
//            return ApiResponse_New<bool>.FailureResponse($"Error deleting {typeof(TEntity).Name}", 500);
//        }
//    }

//    public virtual async Task<ApiResponse_New<bool>> DeactivateAsync(TId id)
//    {
//        try
//        {
//            var entity = await _repository.GetByIdAsync(id);
//            if (entity == null)
//            {
//                return ApiResponse_New<bool>.NotFoundResponse($"{typeof(TEntity).Name} with id {id} not found");
//            }

//            entity.IsDeleted = true;
//            _repository.Update(entity);
//            await _repository.SaveChangesAsync();

//            return ApiResponse_New<bool>.SuccessResponse(true, $"{typeof(TEntity).Name} deactivated successfully");
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "Error deactivating {EntityName} with id {Id}", typeof(TEntity).Name, id);
//            return ApiResponse_New<bool>.FailureResponse($"Error deactivating {typeof(TEntity).Name}", 500);
//        }
//    }

//    // متدهای virtual برای اعتبارسنجی - قابل override در کلاس‌های مشتق شده
//    protected virtual Task ValidateCreateRequestAsync(TCreateRequest request)
//    {
//        // پیاده‌سازی در کلاس‌های مشتق شده
//        return Task.CompletedTask;
//    }

//    protected virtual Task ValidateUpdateRequestAsync(TId id, TUpdateRequest request)
//    {
//        // پیاده‌سازی در کلاس‌های مشتق شده
//        return Task.CompletedTask;
//    }
//}