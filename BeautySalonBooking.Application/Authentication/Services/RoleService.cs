using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Domain.Base.UnitOfWork;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Entities;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Enums;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Repositories;
using BeautySalonBooking.Infrastructure;

namespace BeautySalonBooking.Application.Authentication.Services;

public sealed class RoleService : IRoleService
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    public RoleService(IUnitOfWork unitOfWork, IAuthService authService)
    {

        _unitOfWork = unitOfWork;
        _authService = authService;
    }

    public async Task<Role> GetDefaultCustomerRoleAsync(CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.RoleRepository.GetByCodeAsync(RoleCode.Customer, cancellationToken);

        if (role is null)
            throw new InvalidOperationException("نقش پیش‌فرض Customer در سیستم یافت نشد.");

        return role;
    }

    // متدهای جدید
    public async Task<RoleListResponse> GetRolesAsync(RoleSearchRequest request, CancellationToken cancellationToken)
    {
        // ساخت شرط جستجو
        var predicate = string.IsNullOrWhiteSpace(request.SearchTerm)
            ? null
            : (System.Linq.Expressions.Expression<Func<Role, bool>>)(r =>
                r.Title.Contains(request.SearchTerm) ||
                r.Code.ToString().Contains(request.SearchTerm) ||
                (r.Description != null && r.Description.Contains(request.SearchTerm)));

        // دریافت داده‌های پیج‌بندی شده
        var pagedResult = await _unitOfWork.RoleRepository.GetPagedAsync(
            request.PageNumber,
            request.PageSize,
            predicate,
            q => q.OrderBy(r => r.Id), // مرتب‌سازی بر اساس Id
            cancellationToken);

        // تبدیل به DTO
        var roleDtos = pagedResult.Items.Select(r => new RoleDto
        {
            Id = r.Id,
            Code = r.Code.ToString(),
            Title = r.Title,
            Description = r.Description
        });

        return new RoleListResponse
        {
            Roles = roleDtos,
            TotalCount = pagedResult.TotalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }

    public async Task<RoleDto> GetRoleByIdAsync(int id, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.RoleRepository.GetByIdAsync(id, cancellationToken);

        if (role is null)
            throw new KeyNotFoundException($"نقش با شناسه {id} یافت نشد.");

        return new RoleDto
        {
            Id = role.Id,
            Code = role.Code.ToString(),
            Title = role.Title,
            Description = role.Description
        };
    }

    public async Task<RoleDto> CreateRoleAsync(CreateRoleRequest request, CancellationToken cancellationToken)
    {
        // بررسی وجود نقش با کد مشابه
        if (!Enum.TryParse<RoleCode>(request.Code, true, out var roleCode))
            throw new ArgumentException($"کد نقش '{request.Code}' معتبر نیست.");

        var existingRole = await _unitOfWork.RoleRepository.GetByCodeAsync(roleCode, cancellationToken);
        if (existingRole is not null)
            throw new InvalidOperationException($"نقش با کد '{request.Code}' قبلاً در سیستم ثبت شده است.");

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            // ایجاد نقش جدید
            var newRole = Role.Create(
                request.Title,
                roleCode,
                request.Description);

            await _unitOfWork.RoleRepository.AddAsync(newRole, cancellationToken);


            await _unitOfWork.CommitAsync(cancellationToken);


            // ذخیره تغییرات در دیتابیس (فرض بر این است که UnitOfWork در جای دیگری مدیریت می‌شود)
            // اگر از UnitOfWork استفاده نمی‌کنید، باید SaveChangesAsync را فراخوانی کنید

            return new RoleDto
            {
                Id = newRole.Id,
                Code = newRole.Code.ToString(),
                Title = newRole.Title,
                Description = newRole.Description
            };
        }
        catch (Exception)
        {

            throw;
        }

    }

    public async Task<RoleDto> UpdateRoleAsync(UpdateRoleRequest request, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.RoleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (role is null)
            throw new KeyNotFoundException($"نقش با شناسه {request.Id} یافت نشد.");

        // بررسی کد نقش
        if (!Enum.TryParse<RoleCode>(request.Code, true, out var roleCode))
            throw new ArgumentException($"کد نقش '{request.Code}' معتبر نیست.");

        // بررسی عدم تکراری بودن کد (به جز خود نقش)
        var existingRole = await _unitOfWork.RoleRepository.GetByCodeAsync(roleCode, cancellationToken);
        if (existingRole is not null && existingRole.Id != request.Id)
            throw new InvalidOperationException($"نقش با کد '{request.Code}' قبلاً در سیستم ثبت شده است.");

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            // به‌روزرسانی نقش
            role.ChangeTitle(request.Title);
            role.ChangeDescription(request.Description);
            // توجه: کد نقش قابل تغییر نیست چون شناسه اصلی نقش است
            // اگر نیاز به تغییر کد دارید، باید منطق خاصی پیاده‌سازی شود

            _unitOfWork.RoleRepository.Update(role);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new RoleDto
            {
                Id = role.Id,
                Code = role.Code.ToString(),
                Title = role.Title,
                Description = role.Description
            };

        }
        catch (Exception)
        {

            throw;
        }

    }

    public async Task DeleteRoleAsync(int id, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.RoleRepository.GetByIdAsync(id, cancellationToken);

        if (role is null)
            throw new KeyNotFoundException($"نقش با شناسه {id} یافت نشد.");

        // بررسی اینکه آیا نقش قابل حذف است (مثلاً نقش سیستمی نباشد)
        if (role.Code == RoleCode.SystemAdmin || role.Code == RoleCode.Customer)
            throw new InvalidOperationException($"نقش '{role.Title}' یک نقش سیستمی است و قابل حذف نمی‌باشد.");

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var user = await _authService.GetCurrentUserAsync();
            role.Delete(user.Id);
            //_unitOfWork.RoleRepository.Delete(role);

            await _unitOfWork.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {

            throw;
        }

    }
}