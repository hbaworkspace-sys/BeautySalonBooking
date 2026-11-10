using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Application.Authentication.Services;
using BeautySalonBooking.Application.Branch.BranchService.Interfaces;
using BeautySalonBooking.Contracts.Branch.BranchService.Dtos;
using BeautySalonBooking.Contracts.Branch.BranchService.Enums;
using BeautySalonBooking.Contracts.Branch.BranchService.Requests;
using BeautySalonBooking.Contracts.Branch.BranchService.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Domain.Base.UnitOfWork;
using BeautySalonBooking.Domain.BranchAggregate.Repositories;

namespace BeautySalonBooking.Application.Branch.BranchService.Services;

public sealed class BranchServiceService : IBranchServiceService
{
    private readonly IBranchServiceRepository _branchServiceRepository;

    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public BranchServiceService(
        IBranchServiceRepository branchServiceRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _branchServiceRepository = branchServiceRepository;
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse_New<GetBranchesByServiceResponse>>
        GetBranchesByServiceAsync(
            GetBranchesByServiceRequest request,
            CancellationToken cancellationToken = default)
    {
        var results =
            await _branchServiceRepository
                .GetBranchesByServiceIdAsync(
                    request.ServiceId,
                    cancellationToken);

        var response = new GetBranchesByServiceResponse
        {
            Organizations = results
                .Select(x => new BranchServiceOrganizationDto
                {
                    BranchServiceId = x.BranchServiceId,

                    BranchId = x.BranchId,
                    BranchTitle = x.BranchTitle,
                    BranchDescription = x.BranchDescription,

                    OrganizationId = x.OrganizationId,
                    OrganizationTitle = x.OrganizationTitle,
                    OrganizationType = (OrganizationType)x.OrganizationType,

                    Price = x.Price,
                    Duration = x.Duration,
                    DisplayOrder = x.DisplayOrder,
                    Media = x.Media
                    .Select(m => new BranchMediaDto
                    {
                        FileName = m.FileName,
                        ContentType = m.ContentType,
                        FileSize = m.FileSize,
                        Content = m.Content,
                        DisplayOrder = m.DisplayOrder
                    })
                    .ToList()
                })
                .ToList()
        };

        return new ApiResponse_New<GetBranchesByServiceResponse>
        {
            IsSuccess = true,
            Code = 200,
            Payload = response
        };
    }

    // 👇 متد جدید: لیست با فیلتر
    public async Task<ApiResponse_New<GetBranchServicesResponse>> GetFilteredAsync(
        GetBranchServicesRequest request, CancellationToken ct = default)
    {
        var userId = _currentUserService.GetCurrentUserId();
        var (items, totalCount) = await _branchServiceRepository.GetFilteredAsync((long)userId,
            request.BranchId,
            request.ServiceId,
            request.MinPrice,
            request.MaxPrice,
            request.SearchTerm,
            request.IsActive,
            request.PageNumber,
            request.PageSize,
            request.SortBy,
            request.SortDescending,
            ct);

        var response = new GetBranchServicesResponse
        {
            Items = items.Select(x => new BranchServiceDto
            {
                Id = x.Id,
                BranchId = x.BranchId,
                BranchTitle = x.Branch?.Title ?? "",
                ServiceId = x.ServiceId,
                ServiceTitle = x.Service?.Title ?? "",
                Price = x.Price,
                Duration = x.Duration,
                Description = x.Description,
                DisplayOrder = x.DisplayOrder,
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                IsActive = x.IsActive,
                CreatedAt = x.CreatedAt
            }).ToList(),
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        return new ApiResponse_New<GetBranchServicesResponse>
        {
            IsSuccess = true,
            Code = 200,
            Payload = response
        };
    }

    // 👇 متد جدید: درج
    public async Task<ApiResponse_New<long>> CreateAsync(
        CreateBranchServiceRequest request, CancellationToken ct = default)
    {
        try
        {
            var entity = new BeautySalonBooking.Domain.BranchAggregate.Entities.BranchService(
                branchId: request.BranchId,
                serviceId: request.ServiceId,
                price: request.Price,
                duration: request.Duration,
                fromDate: request.FromDate,
                toDate: request.ToDate,
                description: request.Description,
                displayOrder: request.DisplayOrder);

            await _branchServiceRepository.AddAsync(entity, ct);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponse_New<long>
            {
                IsSuccess = true,
                Code = 201,
                Message = "سرویس شعبه با موفقیت ایجاد شد.",
                Payload = entity.Id
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse_New<long>
            {
                IsSuccess = false,
                Code = 400,
                Message = ex.InnerException?.Message ?? ex.Message
            };
        }
    }

    // 👇 متد جدید: ویرایش
    public async Task<ApiResponse_New<bool>> UpdateAsync(
        UpdateBranchServiceRequest request, CancellationToken ct = default)
    {
        try
        {
            var entity = await _branchServiceRepository.GetByIdAsync(request.Id, ct);
            if (entity == null)
            {
                return new ApiResponse_New<bool>
                {
                    IsSuccess = false,
                    Code = 404,
                    Message = "سرویس شعبه یافت نشد."
                };
            }

            entity.Update(
                price: request.Price,
                duration: request.Duration,
                fromDate: request.FromDate,
                toDate: request.ToDate,
                description: request.Description,
                displayOrder: request.DisplayOrder
                );

            _branchServiceRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponse_New<bool>
            {
                IsSuccess = true,
                Code = 200,
                Message = "سرویس شعبه با موفقیت ویرایش شد.",
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

    // 👇 متد جدید: حذف (Soft Delete)
    public async Task<ApiResponse_New<bool>> DeleteAsync(long id, CancellationToken ct = default)
    {
        try
        {
            var entity = await _branchServiceRepository.GetByIdAsync(id, ct);
            if (entity == null)
            {
                return new ApiResponse_New<bool>
                {
                    IsSuccess = false,
                    Code = 404,
                    Message = "سرویس شعبه یافت نشد."
                };
            }

            _branchServiceRepository.Delete(entity); // Soft Delete
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponse_New<bool>
            {
                IsSuccess = true,
                Code = 200,
                Message = "سرویس شعبه با موفقیت حذف شد.",
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
}