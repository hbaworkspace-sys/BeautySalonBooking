using BeautySalonBooking.Domain.SchedulingAggregate.Entities;
using BeautySalonBooking.Domain.SchedulingAggregate.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories
{
    public class WorkingShiftRepository : Repository<WorkingShift, long>, IWorkingShiftRepository
    {
        public WorkingShiftRepository(BeautyDbContext context) : base(context) { }
    }
}
