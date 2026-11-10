using BeautySalonBooking.Application.Request.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Request.Interfaces
{
    public interface IRequestService
    {
        Task<long> CreateFullRequestAsync(CreateFullRequestDto dto, CancellationToken ct = default);
        Task ChangeStatusAsync(ChangeRequestStatusDto dto, CancellationToken ct = default);
        Task<List<RequestListDto>> GetAllAsync(CancellationToken ct = default);

        Task<long> CreateOrganizationUpgradeRequestAsync(CreateOrganizationUpgradeRequestDto dto, CancellationToken ct = default);
        Task ApproveOrganizationUpgradeRequestAsync(ApproveOrganizationUpgradeRequestDto dto, CancellationToken ct = default);

    }
}
