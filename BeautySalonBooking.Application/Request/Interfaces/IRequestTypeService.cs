using BeautySalonBooking.Application.Request.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Request.Interfaces
{
    public interface IRequestTypeService
    {
        Task<int> CreateAsync(CreateRequestTypeDto dto, CancellationToken ct = default);
        Task<List<RequestTypeResponseDto>> GetAllAsync(CancellationToken ct = default);
    }
}
