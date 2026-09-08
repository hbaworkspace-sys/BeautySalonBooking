using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Contracts.Service.Requests;
using BeautySalonBooking.Contracts.Service.Responses;

namespace BeautySalonBooking.Application.Service.Interfaces;

public interface IServiceService
{
    /// <summary>
    /// دریافت لیست خدمات بر اساس شناسه دسته‌بندی.
    /// </summary>
    Task<ApiResponse_New<GetServicesByCategoryResponse>> GetByCategoryAsync(
        GetServicesByCategoryRequest request,
        CancellationToken cancellationToken);
}