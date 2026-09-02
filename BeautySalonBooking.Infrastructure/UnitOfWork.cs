using BeautySalonBooking.Domain.Base.UnitOfWork;
using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Repositories;
using BeautySalonBooking.Domain.PersonAggregate.Repositories;
using BeautySalonBooking.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace BeautySalonBooking.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        protected BeautyDbContext context;
        private IDbContextTransaction? _transaction;

        public IOtpRepository OtpRepository { get; }
        public IPersonRepository PersonRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IUserRepository UserRepository { get; }
        public IUserRoleRepository UserRoleRepository { get; }
        public IRefreshTokenRepository RefreshTokenRepository { get; }
        public IPermissionRepository PermissionRepository { get; }


        public UnitOfWork(BeautyDbContext _context,
                IOtpRepository _otpRepository,
                IPermissionRepository _permissionRepository,
                IRefreshTokenRepository _refreshTokenRepository,
                IPersonRepository _personRepository,
                IRoleRepository _roleRepository,
                IUserRepository _userRepository,
                IUserRoleRepository _userRoleRepository)
        {
            context = _context;
            OtpRepository = _otpRepository;
            PersonRepository = _personRepository;
            RoleRepository = _roleRepository;
            UserRepository = _userRepository;
            PermissionRepository = _permissionRepository;
            UserRoleRepository = _userRoleRepository;
            RefreshTokenRepository = _refreshTokenRepository;
        }

        public Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
        {
            return context.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync(
            CancellationToken cancellationToken = default)
        {
            if (_transaction is not null)
                return;

            _transaction = await context.Database
                .BeginTransactionAsync(
                    IsolationLevel.ReadCommitted,
                    cancellationToken);
        }
        public async Task CommitAsync(
            CancellationToken cancellationToken = default)
        {
            if (_transaction is null)
                return;

            try
            {
                await context.SaveChangesAsync(cancellationToken);
                await _transaction.CommitAsync(cancellationToken);
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackAsync(
            CancellationToken cancellationToken = default)
        {
            if (_transaction is null)
                return;

            try
            {
                await _transaction.RollbackAsync(cancellationToken);
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}