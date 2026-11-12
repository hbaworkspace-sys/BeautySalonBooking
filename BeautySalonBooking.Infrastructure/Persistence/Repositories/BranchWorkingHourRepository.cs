using BeautySalonBooking.Domain.SchedulingAggregate.Entities;
using BeautySalonBooking.Domain.SchedulingAggregate.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories
{
    // BranchWorkingHourRepository.cs
    public class BranchWorkingHourRepository : Repository<BranchWorkingHour, long>, IBranchWorkingHourRepository
    {
        public BranchWorkingHourRepository(BeautyDbContext context) : base(context) { }
    }
}
