using BeautySalonBooking.Application.Scheduling.DTOs;
using BeautySalonBooking.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Scheduling.Interfaces
{
    public interface IBranchMemberScheduleService
    {
        Task<ApiResponse_New<BranchMemberScheduleDto>> CreateAsync(
            CreateBranchMemberScheduleDto dto, CancellationToken ct = default);

        Task<ApiResponse_New<BranchMemberScheduleDto>> UpdateAsync(
            UpdateBranchMemberScheduleDto dto, CancellationToken ct = default);

        Task<ApiResponse_New<bool>> DeleteAsync(long id, CancellationToken ct = default);

        Task<ApiResponse_New<BranchMemberScheduleDto>> GetByIdAsync(long id, CancellationToken ct = default);

        Task<ApiResponse_New<List<BranchMemberScheduleDto>>> GetByBranchMemberIdAsync(
            long branchMemberId, CancellationToken ct = default);
    }
}
