using BeautySalonBooking.Application.Request.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Request.Interfaces
{
    public interface IRequestRequiredDocumentService
    {
        Task<int> CreateAsync(CreateRequestRequiredDocumentDto dto, CancellationToken ct = default);
        Task UpdateAsync(UpdateRequestRequiredDocumentDto dto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
        Task<List<RequestRequiredDocumentResponseDto>> GetByRequestTypeIdAsync(int requestTypeId, CancellationToken ct = default);
    }
}
