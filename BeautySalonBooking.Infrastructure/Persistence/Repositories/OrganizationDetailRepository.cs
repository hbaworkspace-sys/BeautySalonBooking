using BeautySalonBooking.Domain.OrganizationAggregate.Entities;
using BeautySalonBooking.Domain.OrganizationAggregate.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories
{
    public class OrganizationDetailRepository : Repository<OrganizationDetail, long>, IOrganizationDetailRepository
    {
        public OrganizationDetailRepository(BeautyDbContext context) : base(context) { }
    }
}
