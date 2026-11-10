using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.RequestAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Domain.RequestAggregate.Repositories
{
    public interface IRequestTypeRepository : IRepository<RequestType, int>
    {
        Task<RequestType?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    }

}
