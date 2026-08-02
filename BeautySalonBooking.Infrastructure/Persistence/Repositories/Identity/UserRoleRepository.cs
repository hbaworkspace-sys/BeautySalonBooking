using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Base.Enums;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;
using BeautySalonBooking.Domain.Identity.UserAggregate.Repositories;
using BeautySalonBooking.Domain.Repositories;
using Microsoft.EntityFrameworkCore;


namespace BeautySalonBooking.Infrastructure.Persistence.Repositories;


public class UserRoleRepository
    : Repository<UserRole, long>, IUserRoleRepository
{


    public UserRoleRepository(
        BeautyDbContext context)
        : base(context)
    {

    }


    public async Task<bool> UserNameExistsAsync(
    string userName,
    CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    public async Task<bool> ExistsAsync(
    long userId,
    int roleId,
    CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserRole>> GetByRoleIdAsync(
        int roleId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserRole>> GetByUserIdAsync(
        long userId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<UserRole?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        return await base.GetByIdAsync(
            id,
            cancellationToken);
    }



    public async Task<UserRole> UpdateAsync(
        UserRole entity,
        CancellationToken cancellationToken = default)
    {

        Update(entity);

        return entity;

    }

}