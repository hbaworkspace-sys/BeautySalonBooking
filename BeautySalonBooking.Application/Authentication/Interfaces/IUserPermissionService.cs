using BeautySalonBooking.Contracts.Authentication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Authentication.Interfaces
{
    public interface IUserPermissionService
    {
        Task<List<PermissionDto>> GetPermissionsAsync(
            long userId,
            CancellationToken cancellationToken = default);
        Task<List<PermissionDto>> GetMenusAsync(
    long userId,
    CancellationToken cancellationToken);

        Task<List<string>> GetRoleNamesAsync(
            long userId,
            CancellationToken cancellationToken);
    }
}
