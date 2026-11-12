using BeautySalonBooking.Contracts.Branch.Branch.Dtos;
using BeautySalonBooking.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Branch.Branch.Interfaces
{
    public interface IBranchMemberAppService
    {
        Task<ApiResponse_New<BranchMemberDto>> CreateAsync(CreateBranchMemberDto dto, CancellationToken ct = default);
        Task<ApiResponse_New<BranchMemberDto>> UpdateAsync(UpdateBranchMemberDto dto, CancellationToken ct = default);
        Task<ApiResponse_New<bool>> DeleteAsync(long id, CancellationToken ct = default);
        Task<ApiResponse_New<BranchMemberDto>> GetByIdAsync(long id, CancellationToken ct = default);
        Task<ApiResponse_New<List<BranchMemberDto>>> GetFilteredAsync(GetBranchMembersRequest request, CancellationToken ct = default);
    }
}
