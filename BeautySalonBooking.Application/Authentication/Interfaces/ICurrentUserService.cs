using BeautySalonBooking.Contracts.Authentication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Authentication.Interfaces
{
    public interface ICurrentUserService
    {
        Task<UserDto?> GetCurrentUserAsync(CancellationToken cancellationToken = default);
        long? GetCurrentUserId();
        Task<bool> IsCurrentUserInRoleAsync(string role, CancellationToken cancellationToken = default);
        Task<bool> HasPermissionAsync(string permissionCode, CancellationToken cancellationToken = default);
    }
}
