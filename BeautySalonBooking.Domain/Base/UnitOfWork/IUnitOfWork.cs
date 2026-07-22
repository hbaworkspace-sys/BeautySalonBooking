using BeautySalonBooking.Domain.Identity.RoleAggregate.Repositories;
using BeautySalonBooking.Domain.Identity.UserAggregate.Repositories;
using BeautySalonBooking.Domain.OTP;
using BeautySalonBooking.Domain.PersonAggregate.Repositories;
using BeautySalonBooking.Domain.UserAggregate.Repositories;

namespace BeautySalonBooking.Domain.Base.UnitOfWork;

public interface IUnitOfWork
{
    IOtpRepository OtpRepository { get; }
    IPersonRepository PersonRepository { get; }
    IRoleRepository RoleRepository { get; }
    IUserRepository UserRepository { get; }
    IUserRoleRepository UserRoleRepository { get; }

    Task<int> SaveChangesAsync(
       CancellationToken cancellationToken = default);

    Task BeginTransactionAsync(
      CancellationToken cancellationToken = default);

    Task CommitAsync(
      CancellationToken cancellationToken = default);

    Task RollbackAsync(
      CancellationToken cancellationToken = default);
}
