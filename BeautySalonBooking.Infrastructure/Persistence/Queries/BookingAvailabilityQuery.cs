using BeautySalonBooking.Domain.SchedulingAggregate.Queries;
using BeautySalonBooking.Domain.SchedulingAggregate.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonBooking.Infrastructure.Persistence.Queries;

public sealed class BookingAvailabilityQuery
    : IBookingAvailabilityQuery
{
    private readonly BeautyDbContext _context;

    public BookingAvailabilityQuery(
        BeautyDbContext context)
    {
        _context = context;
    }

    public async Task<BookingAvailabilityReadModel?>
        GetAsync(
            long branchMemberServiceId,
            DateOnly fromDate,
            DateOnly toDate,
            CancellationToken cancellationToken = default)
    {
        if (branchMemberServiceId <= 0)
            return null;

        if (fromDate > toDate)
            return null;

        // =====================================================
        // BranchMemberService
        // =====================================================

        var memberService =
            await _context.BranchMemberServices
                .AsNoTracking()
                .Where(x =>
                    x.Id == branchMemberServiceId &&
                    x.IsActive &&
                    !x.IsDeleted &&
                    x.BranchMember.IsActive &&
                    !x.BranchMember.IsDeleted &&
                    x.BranchService.IsActive &&
                    !x.BranchService.IsDeleted &&
                    x.BranchService.Branch.IsActive &&
                    !x.BranchService.Branch.IsDeleted)
                .Select(x => new
                {
                    BranchId = x.BranchService.BranchId,

                    BranchMemberId = x.BranchMemberId,

                    ServiceId = x.BranchService.ServiceId,

                    ServiceTitle =
                        x.BranchService.Service.Title,

                    OrganizationTitle =
                        x.BranchService.Branch.Organization.Title,

                    BranchTitle =
                        x.BranchService.Branch.Title,

                    BranchDescription =
                        x.BranchService.Branch.Description,

                    StylistName =
                        x.BranchMember.Person.FirstName +
                        " " +
                        x.BranchMember.Person.LastName,

                    ServicePrice =
                        x.Price ??
                        x.BranchService.Price,

                    ServiceDuration =
                        x.Duration ??
                        x.BranchService.Duration
                })
                .FirstOrDefaultAsync(cancellationToken);

        if (memberService is null)
            return null;

        // =====================================================
        // Member Working Shifts
        // =====================================================

        var memberWorkingShifts =
            await _context.WorkingShifts
                .AsNoTracking()
                .Where(x =>
                    x.BranchMemberSchedule.BranchMemberId ==
                        memberService.BranchMemberId &&
                    x.BranchMemberSchedule.IsActive &&
                    !x.BranchMemberSchedule.IsDeleted &&
                    x.IsActive &&
                    !x.IsDeleted)
                .Select(x => new WorkingIntervalReadModel
                {
                    DayOfWeek = x.DayOfWeek,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime
                })
                .OrderBy(x => x.DayOfWeek)
                .ThenBy(x => x.StartTime)
                .ToListAsync(cancellationToken);

        // =====================================================
        // Branch Working Hours
        // =====================================================

        var branchWorkingHours =
            await _context.BranchWorkingHours
                .AsNoTracking()
                .Where(x =>
                    x.BranchSchedule.BranchId ==
                        memberService.BranchId &&
                    x.BranchSchedule.IsActive &&
                    !x.BranchSchedule.IsDeleted &&
                    x.BranchSchedule.IsWorkingDay &&
                    x.IsActive &&
                    !x.IsDeleted)
                .Select(x => new WorkingIntervalReadModel
                {
                    DayOfWeek = x.BranchSchedule.DayOfWeek,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime
                })
                .OrderBy(x => x.DayOfWeek)
                .ThenBy(x => x.StartTime)
                .ToListAsync(cancellationToken);

        // =====================================================
        // Schedule Exceptions
        // =====================================================

        var scheduleExceptions =
            await _context.ScheduleExceptions
                .AsNoTracking()
                .Where(x =>
                    x.BranchMemberSchedule.BranchMemberId ==
                        memberService.BranchMemberId &&
                    x.Date >= fromDate &&
                    x.Date <= toDate &&
                    x.IsActive &&
                    !x.IsDeleted &&
                    x.BranchMemberSchedule.IsActive &&
                    !x.BranchMemberSchedule.IsDeleted)
                .Select(x => new ScheduleExceptionReadModel
                {
                    Date = x.Date,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    IsWorkingDay = x.IsWorkingDay
                })
                .OrderBy(x => x.Date)
                .ThenBy(x => x.StartTime)
                .ToListAsync(cancellationToken);

        // =====================================================
        // TimeOffs
        // =====================================================

        var timeOffs =
            await _context.TimeOffs
                .AsNoTracking()
                .Where(x =>
                    x.BranchMemberSchedule.BranchMemberId ==
                        memberService.BranchMemberId &&
                    x.EndDate >= fromDate &&
                    x.StartDate <= toDate &&
                    x.IsActive &&
                    !x.IsDeleted &&
                    x.BranchMemberSchedule.IsActive &&
                    !x.BranchMemberSchedule.IsDeleted)
                .Select(x => new TimeOffReadModel
                {
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime
                })
                .OrderBy(x => x.StartDate)
                .ThenBy(x => x.StartTime)
                .ToListAsync(cancellationToken);

        // =====================================================
        // Branch Holidays
        // =====================================================

        var branchHolidays =
            await _context.BranchHolidays
                .AsNoTracking()
                .Where(x =>
                    x.BranchId == memberService.BranchId &&
                    x.Date >= fromDate &&
                    x.Date <= toDate &&
                    x.IsActive &&
                    !x.IsDeleted)
                .Select(x => x.Date)
                .ToHashSetAsync(cancellationToken);

        // =====================================================
        // Appointments
        // =====================================================

        var appointments =
            await _context.Appointments
                .AsNoTracking()
                .Where(x =>
                    x.BranchMemberServiceId ==
                        branchMemberServiceId &&
                    x.Date >= fromDate &&
                    x.Date <= toDate &&
                    x.IsActive &&
                    !x.IsDeleted)
                .Select(x => new AppointmentSlotReadModel
                {
                    Date = x.Date,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    Status = x.Status
                })
                .OrderBy(x => x.Date)
                .ThenBy(x => x.StartTime)
                .ToListAsync(cancellationToken);

        // =====================================================
        // Final Read Model
        // =====================================================

        return new BookingAvailabilityReadModel
        {
            BranchId = memberService.BranchId,

            BranchMemberId = memberService.BranchMemberId,

            BranchMemberServiceId = branchMemberServiceId,

            ServiceId = memberService.ServiceId,

            ServiceTitle = memberService.ServiceTitle,

            OrganizationTitle = memberService.OrganizationTitle,

            BranchTitle = memberService.BranchTitle,

            BranchDescription = memberService.BranchDescription,

            StylistName = memberService.StylistName,

            ServicePrice = memberService.ServicePrice,

            ServiceDuration = memberService.ServiceDuration,

            MemberWorkingShifts = memberWorkingShifts,

            BranchWorkingHours = branchWorkingHours,

            ScheduleExceptions = scheduleExceptions,

            TimeOffs = timeOffs,

            BranchHolidays = branchHolidays,

            Appointments = appointments
        };
    }
}