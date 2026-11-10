using BeautySalonBooking.Application.Request.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Request.Interfaces
{
    public interface IDocumentTypeService
    {
        Task<int> CreateAsync(CreateDocumentTypeDto dto, CancellationToken ct = default);
        Task UpdateAsync(UpdateDocumentTypeDto dto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
        Task<List<DocumentTypeResponseDto>> GetAllAsync(CancellationToken ct = default);
    }
}
