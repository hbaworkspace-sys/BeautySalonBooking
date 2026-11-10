using BeautySalonBooking.Domain.SchedulingAggregate.Entities;
using BeautySalonBooking.Domain.SchedulingAggregate.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories
{
    public class TimeOffRepository : Repository<TimeOff, long>, ITimeOffRepository
    {
        public TimeOffRepository(BeautyDbContext context) : base(context) { }
    }
}
