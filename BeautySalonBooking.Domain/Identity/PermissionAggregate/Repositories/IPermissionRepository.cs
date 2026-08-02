using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.Identity.PermissionAggregate.Entities;
using BeautySalonBooking.Domain.Identity.PermissionAggregate.Enums;

namespace BeautySalonBooking.Domain.Repositories;


public interface IPermissionRepository
    : IRepository<Permission, int>
{

    Task<List<Permission>> GetRootAsync(
CancellationToken cancellationToken);


    Task<List<Permission>> GetChildrenAsync(
    int parentId,
    CancellationToken cancellationToken);
    Task<List<Permission>> GetRootPermissionsAsync(
        CancellationToken cancellationToken = default);




    Task<IEnumerable<Permission>> GetPermissionsByUserIdAsync(long userId, CancellationToken cancellationToken = default);


    Task<Permission?> GetByIdWithChildrenAsync(
        int id,
        CancellationToken cancellationToken = default);



    Task<bool> HasChildrenAsync(
        int id,
        CancellationToken cancellationToken = default);



}