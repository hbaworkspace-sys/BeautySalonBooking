using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Domain.Identity.UserAggregate.Repositories
{
    public interface IUserRoleRepository
    {
        Task<UserRole?> GetByIdAsync(long id, CancellationToken cancellationToken);
        Task AddAsync(UserRole userRole, CancellationToken cancellationToken);
        Task UpdateAsync(UserRole userRole, CancellationToken cancellationToken);
    }
}
