using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Application.Branch.Branch.Interfaces;
using BeautySalonBooking.Contracts.Branch.Branch.Dtos;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Domain.Base.UnitOfWork;
using BeautySalonBooking.Domain.BranchAggregate.Entities;
using BeautySalonBooking.Domain.BranchAggregate.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Branch.Branch.Services
{
    public class BranchMemberAppService : IBranchMemberAppService
    {
        private readonly IBranchMemberRepository _memberRepo;
        private readonly IBranchMemberServiceRepository _memberServiceRepo;
        private readonly IBranchServiceRepository _branchServiceRepo;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public BranchMemberAppService(
            IBranchMemberRepository memberRepo,
            IBranchMemberServiceRepository memberServiceRepo,
            IBranchServiceRepository branchServiceRepo,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _memberRepo = memberRepo;
            _memberServiceRepo = memberServiceRepo;
            _branchServiceRepo = branchServiceRepo;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        // ==================== Create ====================
        public async Task<ApiResponse_New<BranchMemberDto>> CreateAsync(
            CreateBranchMemberDto dto, CancellationToken ct = default)
        {
            try
            {
                var entity = new BranchMember(
                    branchId: dto.BranchId,
                    personId: dto.PersonId,
                    branchRoleId: dto.BranchRoleId,
                    startDate: dto.StartDate);

                if (dto.EndDate.HasValue)
                    entity.Update(dto.BranchRoleId, dto.StartDate, dto.EndDate);

                await _memberRepo.AddAsync(entity, ct);
                await _unitOfWork.SaveChangesAsync(ct);

                // 👇 افزودن سرویس‌ها
                foreach (var s in dto.Services)
                {
                    // چک کردن وجود BranchService
                    var branchService = await _branchServiceRepo.GetByIdAsync(s.BranchServiceId, ct);
                    if (branchService == null)
                        throw new Exception($"سرویس شعبه با شناسه {s.BranchServiceId} یافت نشد.");

                    var memberService = new BeautySalonBooking.Domain.BranchAggregate.Entities.BranchMemberService(
                        branchServiceId: s.BranchServiceId,
                        price: s.Price,
                        duration: s.Duration);

                    memberService.SetBranchMemberId(entity.Id);
                    await _memberServiceRepo.AddAsync(memberService, ct);
                }

                await _unitOfWork.SaveChangesAsync(ct);

                var result = await BuildDtoAsync(entity.Id, ct);
                return new ApiResponse_New<BranchMemberDto>
                {
                    IsSuccess = true,
                    Code = 201,
                    Message = "عضو شعبه با موفقیت ایجاد شد.",
                    Payload = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse_New<BranchMemberDto>
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = ex.InnerException?.InnerException?.Message
                              ?? ex.InnerException?.Message
                              ?? ex.Message
                };
            }
        }

        // ==================== Update ====================
        public async Task<ApiResponse_New<BranchMemberDto>> UpdateAsync(
            UpdateBranchMemberDto dto, CancellationToken ct = default)
        {
            try
            {
                var entity = await _memberRepo.GetByIdAsync(dto.Id, ct);
                if (entity == null)
                    return new ApiResponse_New<BranchMemberDto>
                    {
                        IsSuccess = false,
                        Code = 404,
                        Message = "عضو شعبه یافت نشد."
                    };

                entity.Update(dto.BranchRoleId, dto.StartDate, dto.EndDate);
                _memberRepo.Update(entity);
                await _unitOfWork.SaveChangesAsync(ct);

                var result = await BuildDtoAsync(entity.Id, ct);
                return new ApiResponse_New<BranchMemberDto>
                {
                    IsSuccess = true,
                    Code = 200,
                    Message = "عضو شعبه با موفقیت ویرایش شد.",
                    Payload = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse_New<BranchMemberDto>
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = ex.InnerException?.Message ?? ex.Message
                };
            }
        }

        // ==================== Delete ====================
        public async Task<ApiResponse_New<bool>> DeleteAsync(long id, CancellationToken ct = default)
        {
            try
            {
                var entity = await _memberRepo.GetByIdAsync(id, ct);
                if (entity == null)
                    return new ApiResponse_New<bool>
                    {
                        IsSuccess = false,
                        Code = 404,
                        Message = "عضو شعبه یافت نشد."
                    };

                // حذف سرویس‌های عضو
                var services = await _memberServiceRepo.GetByBranchMemberIdAsync(id, ct);
                foreach (var s in services) _memberServiceRepo.Delete(s);

                _memberRepo.Delete(entity);
                await _unitOfWork.SaveChangesAsync(ct);

                return new ApiResponse_New<bool>
                {
                    IsSuccess = true,
                    Code = 200,
                    Message = "عضو شعبه حذف شد.",
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

        // ==================== GetById ====================
        public async Task<ApiResponse_New<BranchMemberDto>> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var result = await BuildDtoAsync(id, ct);
            if (result == null)
                return new ApiResponse_New<BranchMemberDto>
                {
                    IsSuccess = false,
                    Code = 404,
                    Message = "عضو شعبه یافت نشد."
                };

            return new ApiResponse_New<BranchMemberDto>
            {
                IsSuccess = true,
                Code = 200,
                Payload = result
            };
        }

        // ==================== GetFiltered ====================
        public async Task<ApiResponse_New<List<BranchMemberDto>>> GetFilteredAsync(
     GetBranchMembersRequest request, CancellationToken ct = default)
        {
            var (items, totalCount) = await _memberRepo.GetFilteredAsync(
                request.BranchId,
                request.BranchRoleId,
                request.SearchTerm,
                request.IsActive,
                request.PageNumber,
                request.PageSize,
                ct);

            var result = items.Select(x => new BranchMemberDto
            {
                Id = x.Id,
                BranchId = x.BranchId,
                BranchTitle = x.Branch?.Title,
                PersonId = x.PersonId,
                PersonFullName = x.Person != null
                    ? $"{x.Person.FirstName} {x.Person.LastName}"
                    : null,

                PersonPhoneNumber = x.Person?.Users?
                    .SelectMany(u => u.PhoneNumbers)
                    .FirstOrDefault(p => p.IsDefault)?.Number,
                BranchRoleId = x.BranchRoleId,
                BranchRoleTitle = x.BranchRole?.Title,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Services = x.Services.Select(s => new BranchMemberServiceDto
                {
                    Id = s.Id,
                    BranchMemberId = s.BranchMemberId,
                    BranchServiceId = s.BranchServiceId,
                    BranchServiceTitle = s.BranchService?.Service?.Title,
                    Price = s.Price,
                    Duration = s.Duration
                }).ToList()
            }).ToList();

            return new ApiResponse_New<List<BranchMemberDto>>
            {
                IsSuccess = true,
                Code = 200,
                Payload = result
            };
        }

        // ==================== Helper ====================
        private async Task<BranchMemberDto?> BuildDtoAsync(long id, CancellationToken ct)
        {
            var entity = await _memberRepo.GetWithDetailsAsync(id, ct);
            if (entity == null) return null;

            return new BranchMemberDto
            {
                Id = entity.Id,
                BranchId = entity.BranchId,
                BranchTitle = entity.Branch?.Title,
                PersonId = entity.PersonId,
                PersonFullName = $"{entity.Person.FirstName} {entity.Person.LastName}",

                PersonPhoneNumber = entity.Person?.Users?
                    .SelectMany(u => u.PhoneNumbers)
                    .FirstOrDefault(p => p.IsDefault)?.Number,
                BranchRoleId = entity.BranchRoleId,
                BranchRoleTitle = entity.BranchRole?.Title,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Services = entity.Services.Select(s => new BranchMemberServiceDto
                {
                    Id = s.Id,
                    BranchMemberId = s.BranchMemberId,
                    BranchServiceId = s.BranchServiceId,
                    BranchServiceTitle = s.BranchService?.Service?.Title,
                    Price = s.Price,
                    Duration = s.Duration
                }).ToList()
            };
        }
    }
}
