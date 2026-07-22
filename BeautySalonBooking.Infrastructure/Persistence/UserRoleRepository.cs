using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;
using BeautySalonBooking.Domain.Identity.UserAggregate.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Infrastructure.Persistence
{
    public sealed class UserRoleRepository : IUserRoleRepository
    {
        private readonly BeautyDbContext _context;

        public UserRoleRepository(BeautyDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserRole userRole, CancellationToken cancellationToken)
        {
            await _context.UserRoles.AddAsync(userRole, cancellationToken);
        }

        public Task<UserRole?> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(UserRole userRole, CancellationToken cancellationToken)
        {
            _context.UserRoles.Update(userRole);
        }
    }
}
