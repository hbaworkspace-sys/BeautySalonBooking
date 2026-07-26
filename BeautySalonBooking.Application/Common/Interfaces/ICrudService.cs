using BeautySalonBooking.Contracts.Common;
using Core.DTOs;

namespace BeautySalonBooking.Application.Common.Interfaces;

public interface ICrudService<TEntity, TId, TDto, TCreateRequest, TUpdateRequest>
{
    Task<ApiResponse_New<TDto?>> GetByIdAsync(TId id);
    Task<ApiResponse_New<List<TDto>>> GetAllAsync();
    Task<ApiResponse_New<PagedResult<TDto>>> GetPagedAsync(PaginationRequest request);
    Task<ApiResponse_New<TDto>> CreateAsync(TCreateRequest request);
    Task<ApiResponse_New<TDto?>> UpdateAsync(TId id, TUpdateRequest request);
    Task<ApiResponse_New<bool>> DeleteAsync(TId id);
    Task<ApiResponse_New<bool>> DeactivateAsync(TId id);
}