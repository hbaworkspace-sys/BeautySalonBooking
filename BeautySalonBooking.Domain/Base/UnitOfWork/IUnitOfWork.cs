using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Repositories;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Repositories;
using BeautySalonBooking.Domain.Identity.UserAggregate.Repositories;
using BeautySalonBooking.Domain.PersonAggregate.Repositories;

namespace BeautySalonBooking.Domain.Base.UnitOfWork;

public interface IUnitOfWork
{
    IOtpRepository OtpRepository { get; }
    IPersonRepository PersonRepository { get; }
    IRoleRepository RoleRepository { get; }
    IUserRepository UserRepository { get; }
    IUserRoleRepository UserRoleRepository { get; }
    IRefreshTokenRepository RefreshTokenRepository { get; }

    Task<int> SaveChangesAsync(
       CancellationToken cancellationToken);

    Task BeginTransactionAsync(
      CancellationToken cancellationToken);

    Task CommitAsync(
      CancellationToken cancellationToken);

    Task RollbackAsync(
      CancellationToken cancellationToken);
}
