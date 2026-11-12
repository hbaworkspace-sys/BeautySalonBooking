using BeautySalonBooking.Domain.RequestAggregate.Entities;
using BeautySalonBooking.Domain.RequestAggregate.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories
{
    public class RequestRepository : Repository<Request, long>, IRequestRepository
    {
        public RequestRepository(BeautyDbContext context) : base(context) { }
    }
}
