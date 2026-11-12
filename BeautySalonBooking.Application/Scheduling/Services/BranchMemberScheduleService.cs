using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Application.Scheduling.DTOs;
using BeautySalonBooking.Application.Scheduling.Interfaces;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Domain.Base.UnitOfWork;
using BeautySalonBooking.Domain.BranchAggregate.Repositories;
using BeautySalonBooking.Domain.SchedulingAggregate.Entities;
using BeautySalonBooking.Domain.SchedulingAggregate.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Scheduling.Services
{
    public class BranchMemberScheduleService : IBranchMemberScheduleService
    {
        private readonly IBranchMemberScheduleRepository _scheduleRepo;
        private readonly IWorkingShiftRepository _workingShiftRepo;
        private readonly IScheduleExceptionRepository _exceptionRepo;
        private readonly ITimeOffRepository _timeOffRepo;
        private readonly IBranchMemberRepository _branchMemberRepo;
        private readonly IBranchMemberRepository _memberRepo; // برای چک دسترسی
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public BranchMemberScheduleService(
            IBranchMemberScheduleRepository scheduleRepo,
            IWorkingShiftRepository workingShiftRepo,
            IScheduleExceptionRepository exceptionRepo,
            ITimeOffRepository timeOffRepo,
            IBranchMemberRepository branchMemberRepo,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _scheduleRepo = scheduleRepo;
            _workingShiftRepo = workingShiftRepo;
            _exceptionRepo = exceptionRepo;
            _timeOffRepo = timeOffRepo;
            _branchMemberRepo = branchMemberRepo;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        // ==================== Create ====================
        public async Task<ApiResponse_New<BranchMemberScheduleDto>> CreateAsync(
            CreateBranchMemberScheduleDto dto, CancellationToken ct = default)
        {
            try
            {
                // 1. بررسی وجود BranchMember
                var branchMember = await _branchMemberRepo.GetByIdAsync(dto.BranchMemberId, ct);
                if (branchMember == null)
                    return new ApiResponse_New<BranchMemberScheduleDto>
                    {
                        IsSuccess = false,
                        Code = 404,
                        Message = "عضو شعبه یافت نشد."
                    };

                // 2. بررسی اینکه آیا برای این BranchMember قبلاً Schedule ثبت شده؟
                var existing = await _scheduleRepo.GetByBranchMemberIdAsync(dto.BranchMemberId, ct);
                if (existing != null && existing.Any())
                    return new ApiResponse_New<BranchMemberScheduleDto>
                    {
                        IsSuccess = false,
                        Code = 400,
                        Message = "برای این عضو قبلاً برنامه ثبت شده است."
                    };

                // 3. ایجاد Schedule
                var schedule = new BranchMemberSchedule(dto.BranchMemberId);
                await _scheduleRepo.AddAsync(schedule, ct);
                await _unitOfWork.SaveChangesAsync(ct); // 👈 Id تولید شد

                // 4. افزودن WorkingShifts
                foreach (var ws in dto.WorkingShifts)
                {
                    var shift = new WorkingShift(ws.DayOfWeek, ws.StartTime, ws.EndTime);
                    shift.SetBranchMemberScheduleId(schedule.Id); // نیاز به متد Set
                    await _workingShiftRepo.AddAsync(shift, ct);
                }

                // 5. افزودن Exceptions
                foreach (var ex in dto.Exceptions)
                {
                    var exception = new ScheduleException(ex.Date, ex.StartTime, ex.EndTime, ex.IsWorkingDay, ex.Description);
                    exception.SetBranchMemberScheduleId(schedule.Id);
                    await _exceptionRepo.AddAsync(exception, ct);
                }

                // 6. افزودن TimeOffs
                foreach (var to in dto.TimeOffs)
                {
                    var timeOff = new TimeOff(to.StartDate, to.EndDate, to.StartTime, to.EndTime, to.Reason);
                    timeOff.SetBranchMemberScheduleId(schedule.Id);
                    await _timeOffRepo.AddAsync(timeOff, ct);
                }

                await _unitOfWork.SaveChangesAsync(ct);

                // 7. ساخت DTO خروجی
                var result = await BuildDtoAsync(schedule.Id, ct);
                return new ApiResponse_New<BranchMemberScheduleDto>
                {
                    IsSuccess = true,
                    Code = 201,
                    Message = "برنامه عضو شعبه با موفقیت ایجاد شد.",
                    Payload = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse_New<BranchMemberScheduleDto>
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
        public async Task<ApiResponse_New<BranchMemberScheduleDto>> UpdateAsync(
            UpdateBranchMemberScheduleDto dto, CancellationToken ct = default)
        {
            try
            {
                var schedule = await _scheduleRepo.GetByIdAsync(dto.Id, ct);
                if (schedule == null)
                    return new ApiResponse_New<BranchMemberScheduleDto>
                    {
                        IsSuccess = false,
                        Code = 404,
                        Message = "برنامه یافت نشد."
                    };

                // 1. حذف تمام WorkingShiftهای قبلی
                var oldShifts = await _workingShiftRepo.FindAsync(x => x.BranchMemberScheduleId == schedule.Id, ct);
                foreach (var s in oldShifts) _workingShiftRepo.Delete(s);

                // 2. حذف Exceptions قبلی
                var oldExceptions = await _exceptionRepo.FindAsync(x => x.BranchMemberScheduleId == schedule.Id, ct);
                foreach (var e in oldExceptions) _exceptionRepo.Delete(e);

                // 3. حذف TimeOffs قبلی
                var oldTimeOffs = await _timeOffRepo.FindAsync(x => x.BranchMemberScheduleId == schedule.Id, ct);
                foreach (var t in oldTimeOffs) _timeOffRepo.Delete(t);

                await _unitOfWork.SaveChangesAsync(ct);

                // 4. افزودن جدیدها
                foreach (var ws in dto.WorkingShifts)
                {
                    var shift = new WorkingShift(ws.DayOfWeek, ws.StartTime, ws.EndTime);
                    shift.SetBranchMemberScheduleId(schedule.Id);
                    await _workingShiftRepo.AddAsync(shift, ct);
                }

                foreach (var ex in dto.Exceptions)
                {
                    var exception = new ScheduleException(ex.Date, ex.StartTime, ex.EndTime, ex.IsWorkingDay, ex.Description);
                    exception.SetBranchMemberScheduleId(schedule.Id);
                    await _exceptionRepo.AddAsync(exception, ct);
                }

                foreach (var to in dto.TimeOffs)
                {
                    var timeOff = new TimeOff(to.StartDate, to.EndDate, to.StartTime, to.EndTime, to.Reason);
                    timeOff.SetBranchMemberScheduleId(schedule.Id);
                    await _timeOffRepo.AddAsync(timeOff, ct);
                }

                await _unitOfWork.SaveChangesAsync(ct);

                var result = await BuildDtoAsync(schedule.Id, ct);
                return new ApiResponse_New<BranchMemberScheduleDto>
                {
                    IsSuccess = true,
                    Code = 200,
                    Message = "برنامه عضو شعبه با موفقیت ویرایش شد.",
                    Payload = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse_New<BranchMemberScheduleDto>
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = ex.InnerException?.InnerException?.Message
                              ?? ex.InnerException?.Message
                              ?? ex.Message
                };
            }
        }

        // ==================== Delete ====================
        public async Task<ApiResponse_New<bool>> DeleteAsync(long id, CancellationToken ct = default)
        {
            try
            {
                var schedule = await _scheduleRepo.GetByIdAsync(id, ct);
                if (schedule == null)
                    return new ApiResponse_New<bool> { IsSuccess = false, Code = 404, Message = "برنامه یافت نشد." };

                // حذف وابستگی‌ها
                var shifts = await _workingShiftRepo.FindAsync(x => x.BranchMemberScheduleId == schedule.Id, ct);
                foreach (var s in shifts) _workingShiftRepo.Delete(s);

                var exceptions = await _exceptionRepo.FindAsync(x => x.BranchMemberScheduleId == schedule.Id, ct);
                foreach (var e in exceptions) _exceptionRepo.Delete(e);

                var timeOffs = await _timeOffRepo.FindAsync(x => x.BranchMemberScheduleId == schedule.Id, ct);
                foreach (var t in timeOffs) _timeOffRepo.Delete(t);

                _scheduleRepo.Delete(schedule);
                await _unitOfWork.SaveChangesAsync(ct);

                return new ApiResponse_New<bool>
                {
                    IsSuccess = true,
                    Code = 200,
                    Message = "برنامه حذف شد.",
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
        public async Task<ApiResponse_New<BranchMemberScheduleDto>> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var result = await BuildDtoAsync(id, ct);
            if (result == null)
                return new ApiResponse_New<BranchMemberScheduleDto>
                {
                    IsSuccess = false,
                    Code = 404,
                    Message = "برنامه یافت نشد."
                };

            return new ApiResponse_New<BranchMemberScheduleDto>
            {
                IsSuccess = true,
                Code = 200,
                Payload = result
            };
        }

        // ==================== GetByBranchMemberId ====================
        public async Task<ApiResponse_New<List<BranchMemberScheduleDto>>> GetByBranchMemberIdAsync(
            long branchMemberId, CancellationToken ct = default)
        {
            var schedule = await _scheduleRepo.GetByBranchMemberIdAsync(branchMemberId, ct);
            if (schedule == null || !schedule.Any())
                return new ApiResponse_New<List<BranchMemberScheduleDto>>
                {
                    IsSuccess = true,
                    Code = 200,
                    Payload = new List<BranchMemberScheduleDto>()
                };

            var result = new List<BranchMemberScheduleDto>();
            foreach (var s in schedule)
            {
                var dto = await BuildDtoAsync(s.Id, ct);
                if (dto != null) result.Add(dto);
            }

            return new ApiResponse_New<List<BranchMemberScheduleDto>>
            {
                IsSuccess = true,
                Code = 200,
                Payload = result
            };
        }

        // ==================== Helper: ساخت DTO ====================
        private async Task<BranchMemberScheduleDto?> BuildDtoAsync(long id, CancellationToken ct)
        {
            var schedule = await _scheduleRepo.GetWithDetailsAsync(id, ct);
            if (schedule == null) return null;

            // گرفتن اطلاعات عضو
            var member = await _branchMemberRepo.GetWithDetailsAsync(schedule.BranchMemberId, ct);

            return new BranchMemberScheduleDto
            {
                Id = schedule.Id,
                BranchMemberId = schedule.BranchMemberId,
                PersonFullName = member?.Person != null
                    ? $"{member.Person.FirstName} {member.Person.LastName}"
                    : null,
                BranchTitle = member?.Branch?.Title,
                BranchRoleTitle = member?.BranchRole?.Title,
                WorkingShifts = schedule.WorkingShifts.Select(w => new WorkingShiftDto
                {
                    DayOfWeek = w.DayOfWeek,
                    StartTime = w.StartTime,
                    EndTime = w.EndTime
                }).ToList(),
                Exceptions = schedule.Exceptions.Select(e => new ScheduleExceptionDto
                {
                    Date = e.Date,
                    StartTime = e.StartTime,
                    EndTime = e.EndTime,
                    IsWorkingDay = e.IsWorkingDay,
                    Description = e.Description
                }).ToList(),
                TimeOffs = schedule.TimeOffs.Select(t => new TimeOffDto
                {
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    StartTime = t.StartTime,
                    EndTime = t.EndTime,
                    Reason = t.Reason
                }).ToList()
            };
        }
    }
}
