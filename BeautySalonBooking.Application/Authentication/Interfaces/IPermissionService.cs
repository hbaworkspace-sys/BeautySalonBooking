using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.Application.Authentication.Interfaces;

public interface IPermissionService
{
    Task<List<string>> GetUserPermissionsAsync(long userId, CancellationToken cancellationToken);


    Task<List<string>> GetUserRolesAsync(long userId, CancellationToken cancellationToken);



    Task<ApiResponse_New<PermissionTreeDto>> GetRootPermissionsAsync(CancellationToken cancellationToken);



    Task<ApiResponse_New<PermissionTreeDto>> GetChildrenAsync(int parentId, CancellationToken cancellationToken);



    Task<ApiResponse_New<int>> CreateAsync(
        PermissionCreateDto dto,
        CancellationToken cancellationToken);



    Task<ApiResponse_New<bool>> UpdateAsync(
        PermissionUpdateDto dto,
        CancellationToken cancellationToken);



    Task<ApiResponse_New<bool>> DeleteAsync(
        int id,

        CancellationToken cancellationToken);
}