using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.RequestAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Domain.RequestAggregate.Repositories
{
    public interface IRequestRepository : IRepository<Request, long>
    {
    }
}
