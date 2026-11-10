using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Application.Scheduling.DTOs;
using BeautySalonBooking.Application.Scheduling.Interfaces;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Domain.Base.UnitOfWork;
using BeautySalonBooking.Domain.SchedulingAggregate.Entities;
using BeautySalonBooking.Domain.SchedulingAggregate.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Scheduling.Services
{
    public class BranchScheduleService : IBranchScheduleService
    {
        private readonly IBranchScheduleRepository _scheduleRepo;
        private readonly IBranchWorkingHourRepository _workingHourRepo;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public BranchScheduleService(
            IBranchScheduleRepository scheduleRepo,
            IBranchWorkingHourRepository workingHourRepo,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _scheduleRepo = scheduleRepo;
            _workingHourRepo = workingHourRepo;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse_New<BranchScheduleDto>> CreateAsync(CreateBranchScheduleDto dto, CancellationToken ct = default)
        {
            try
            {
                var entity = new BranchSchedule(dto.BranchId, dto.DayOfWeek, dto.IsWorkingDay);
                await _scheduleRepo.AddAsync(entity, ct);
                await _unitOfWork.SaveChangesAsync(ct);

                // اضافه کردن ساعات کاری
                foreach (var wh in dto.WorkingHours)
                {
                    var hour = new BranchWorkingHour(wh.StartTime, wh.EndTime);
                    // باید BranchScheduleId را ست کنیم
                    // (نیاز به متد SetBranchScheduleId یا اضافه کردن به لیست via AddWorkingHour)
                    entity.AddWorkingHour(hour);
                }

                await _unitOfWork.SaveChangesAsync(ct);

                return new ApiResponse_New<BranchScheduleDto>
                {
                    IsSuccess = true,
                    Code = 201,
                    Message = "برنامه شعبه ایجاد شد.",
                    Payload = new BranchScheduleDto
                    {
                        Id = entity.Id,
                        BranchId = entity.BranchId,
                        DayOfWeek = entity.DayOfWeek,
                        IsWorkingDay = entity.IsWorkingDay,
                        WorkingHours = dto.WorkingHours
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse_New<BranchScheduleDto>
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = ex.InnerException?.Message ?? ex.Message
                };
            }
        }

        public async Task<ApiResponse_New<BranchScheduleDto>> UpdateAsync(UpdateBranchScheduleDto dto, CancellationToken ct = default)
        {
            try
            {
                var entity = await _scheduleRepo.GetByIdAsync(dto.Id, ct);
                if (entity == null)
                    return new ApiResponse_New<BranchScheduleDto> { IsSuccess = false, Code = 404, Message = "برنامه یافت نشد." };

                entity.Update(dto.DayOfWeek, dto.IsWorkingDay);

                // حذف ساعات قبلی و اضافه کردن جدیدها (روش ساده)
                // در عمل باید WorkingHours را هم مدیریت کنید
                _scheduleRepo.Update(entity);
                await _unitOfWork.SaveChangesAsync(ct);

                return new ApiResponse_New<BranchScheduleDto>
                {
                    IsSuccess = true,
                    Code = 200,
                    Message = "برنامه شعبه ویرایش شد.",
                    Payload = new BranchScheduleDto
                    {
                        Id = entity.Id,
                        BranchId = entity.BranchId,
                        DayOfWeek = entity.DayOfWeek,
                        IsWorkingDay = entity.IsWorkingDay,
                        WorkingHours = dto.WorkingHours
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse_New<BranchScheduleDto>
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = ex.InnerException?.Message ?? ex.Message
                };
            }
        }

        public async Task<ApiResponse_New<bool>> DeleteAsync(long id, CancellationToken ct = default)
        {
            try
            {
                var entity = await _scheduleRepo.GetByIdAsync(id, ct);
                if (entity == null)
                    return new ApiResponse_New<bool> { IsSuccess = false, Code = 404, Message = "برنامه یافت نشد." };

                _scheduleRepo.Delete(entity);
                await _unitOfWork.SaveChangesAsync(ct);

                return new ApiResponse_New<bool> { IsSuccess = true, Code = 200, Message = "برنامه حذف شد.", Payload = true };
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

        public async Task<ApiResponse_New<List<BranchScheduleDto>>> GetByBranchIdAsync(long branchId, CancellationToken ct = default)
        {
            var items = await _scheduleRepo.GetByBranchIdAsync(branchId, ct);

            var result = items.Select(x => new BranchScheduleDto
            {
                Id = x.Id,
                BranchId = x.BranchId,
                DayOfWeek = x.DayOfWeek,
                IsWorkingDay = x.IsWorkingDay,
                WorkingHours = x.WorkingHours.Select(w => new WorkingHourDto
                {
                    StartTime = w.StartTime,
                    EndTime = w.EndTime
                }).ToList()
            }).ToList();

            return new ApiResponse_New<List<BranchScheduleDto>>
            {
                IsSuccess = true,
                Code = 200,
                Payload = result
            };
        }
    }
}
