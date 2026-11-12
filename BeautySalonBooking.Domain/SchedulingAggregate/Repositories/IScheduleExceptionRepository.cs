using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.SchedulingAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Domain.SchedulingAggregate.Repositories
{
    public interface IScheduleExceptionRepository : IRepository<ScheduleException, long> { }

}
