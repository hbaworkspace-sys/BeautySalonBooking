using BeautySalonBooking.Domain.RequestAggregate.Entities;
using BeautySalonBooking.Domain.RequestAggregate.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories
{
    public class RequestTypeRepository : Repository<RequestType, int>, IRequestTypeRepository
    {
        public RequestTypeRepository(BeautyDbContext context) : base(context) { }

        public async Task<RequestType?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            // 👇 استفاده از _context به جای _dbContext
            return await _context.RequestTypes.FirstOrDefaultAsync(x => x.Code == code, cancellationToken);
        }
    }
}
