using BeautySalonBooking.Application.Scheduling.DTOs;
using BeautySalonBooking.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Scheduling.Interfaces
{
    public interface IBranchScheduleService
    {
        Task<ApiResponse_New<BranchScheduleDto>> CreateAsync(CreateBranchScheduleDto dto, CancellationToken ct = default);
        Task<ApiResponse_New<BranchScheduleDto>> UpdateAsync(UpdateBranchScheduleDto dto, CancellationToken ct = default);
        Task<ApiResponse_New<bool>> DeleteAsync(long id, CancellationToken ct = default);
        Task<ApiResponse_New<List<BranchScheduleDto>>> GetByBranchIdAsync(long branchId, CancellationToken ct = default);
    }
}
