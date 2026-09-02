using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Repositories;
using BeautySalonBooking.Domain.PersonAggregate.Repositories;
using BeautySalonBooking.Domain.Repositories;
using System.Data;

namespace BeautySalonBooking.Domain.Base.UnitOfWork;

public interface IUnitOfWork
{
    IOtpRepository OtpRepository { get; }
    IPersonRepository PersonRepository { get; }
    IRoleRepository RoleRepository { get; }
    IUserRepository UserRepository { get; }
    IUserRoleRepository UserRoleRepository { get; }
    IRefreshTokenRepository RefreshTokenRepository { get; }
    IPermissionRepository PermissionRepository { get; }

    Task<int> SaveChangesAsync(
       CancellationToken cancellationToken = default);

    Task BeginTransactionAsync(
        CancellationToken cancellationToken = default);

    Task CommitAsync(
      CancellationToken cancellationToken = default);

    Task RollbackAsync(
      CancellationToken cancellationToken = default);
}
