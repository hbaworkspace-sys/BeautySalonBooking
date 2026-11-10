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
    public class RequestRequiredDocumentRepository : Repository<RequestRequiredDocument, int>, IRequestRequiredDocumentRepository
    {
        public RequestRequiredDocumentRepository(BeautyDbContext context) : base(context) { }

        public async Task<List<RequestRequiredDocument>> GetByRequestTypeIdAsync(int requestTypeId, CancellationToken ct = default)
        {
            return await _context.RequestRequiredDocuments
                .Include(x => x.DocumentType)
                .Where(x => x.RequestTypeId == requestTypeId && !x.IsDeleted)
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync(ct);
        }

        public async Task<RequestRequiredDocument?> GetAsync(int requestTypeId, int documentTypeId, CancellationToken ct = default)
        {
            return await _context.RequestRequiredDocuments
                .FirstOrDefaultAsync(x => x.RequestTypeId == requestTypeId
                                       && x.DocumentTypeId == documentTypeId
                                       && !x.IsDeleted, ct);
        }
    }
}
