using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.RequestAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Domain.RequestAggregate.Repositories
{
    public interface IRequestRequiredDocumentRepository : IRepository<RequestRequiredDocument, int>
    {
        Task<List<RequestRequiredDocument>> GetByRequestTypeIdAsync(int requestTypeId, CancellationToken ct = default);
        Task<RequestRequiredDocument?> GetAsync(int requestTypeId, int documentTypeId, CancellationToken ct = default);
    }
}
