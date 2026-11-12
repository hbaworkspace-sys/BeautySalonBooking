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
    public class DocumentTypeRepository : Repository<DocumentType, int>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(BeautyDbContext context) : base(context) { }

        public async Task<DocumentType?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            // 👇 استفاده از _context
            return await _context.DocumentTypes.FirstOrDefaultAsync(x => x.Code == code, cancellationToken);
        }
    }
}

