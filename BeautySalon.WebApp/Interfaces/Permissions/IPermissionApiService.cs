using BeautySalonBooking.Contracts.Authentication.Dtos;

namespace BeautySalonBooking.WebApp.Interfaces.Permissions;

public interface IPermissionApiService
{
    Task<List<PermissionTreeDto>> GetTreeAsync(
        CancellationToken cancellationToken = default);


    Task<PermissionTreeDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);


    Task<bool> CreateAsync(
        PermissionTreeDto model,
        CancellationToken cancellationToken = default);


    Task<bool> UpdateAsync(
        PermissionTreeDto model,
        CancellationToken cancellationToken = default);


    Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);
}