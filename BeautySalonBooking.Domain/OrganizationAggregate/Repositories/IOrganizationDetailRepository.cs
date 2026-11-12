using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.OrganizationAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Domain.OrganizationAggregate.Repositories
{
    public interface IOrganizationDetailRepository : IRepository<OrganizationDetail, long> { }
}
