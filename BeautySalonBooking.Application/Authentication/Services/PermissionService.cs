using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Domain.Base.UnitOfWork;
using BeautySalonBooking.Domain.Identity.PermissionAggregate.Enums;

using BeautySalonBooking.Domain.Identity.PermissionAggregate.Entities;

namespace BeautySalonBooking.Application.Authentication.Services;


public sealed class PermissionService
    : IPermissionService
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;


    public PermissionService(
        IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<ApiResponse_New<PermissionTreeDto>> GetChildrenAsync(int parentId, CancellationToken cancellationToken)
    {
        ApiResponse_New<PermissionTreeDto> apiResponse_New = new ApiResponse_New<PermissionTreeDto>();
        try
        {
            var permissions =
           await _unitOfWork.PermissionRepository
           .GetChildrenAsync(
               parentId,
               cancellationToken);



            var result =
                permissions.Select(x => new PermissionTreeDto
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    Title = x.Title,
                    Code = x.Code,
                    Type = (int)x.Type,
                    HasChildren = x.Children.Any()
                })
                .ToList();

            apiResponse_New.IsSuccess = true;
            apiResponse_New.Code = ApiResponceConst.SuccseCode;
            apiResponse_New.Message = ApiResponceConst.SuccseMessage;
            apiResponse_New.ListPayload = new List<PermissionTreeDto>();
            apiResponse_New.ListPayload = result;

            return apiResponse_New;
        }
        catch (Exception)
        {
            apiResponse_New.IsSuccess = true;
            apiResponse_New.Code = ApiResponceConst.ErrorCode;
            apiResponse_New.Message = ApiResponceConst.ErrorMessage;
            return apiResponse_New;
        }




    }
    public async Task<ApiResponse_New<PermissionTreeDto>> GetRootPermissionsAsync(CancellationToken cancellationToken)
    {

        ApiResponse_New<PermissionTreeDto> apiResponse_New = new ApiResponse_New<PermissionTreeDto>();
        try
        {

            var permissions = await _unitOfWork.PermissionRepository.GetRootAsync(cancellationToken);



            var result =
                permissions.Select(x => new PermissionTreeDto
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    Title = x.Title,
                    Code = x.Code,
                    Type = (int)x.Type,
                    HasChildren = x.Children.Any()
                })
                .ToList();


            apiResponse_New.IsSuccess = true;
            apiResponse_New.Code = ApiResponceConst.SuccseCode;
            apiResponse_New.Message = ApiResponceConst.SuccseMessage;
            apiResponse_New.ListPayload = new List<PermissionTreeDto>();
            apiResponse_New.ListPayload = result;

            return apiResponse_New;
        }
        catch (Exception)
        {
            apiResponse_New.IsSuccess = true;
            apiResponse_New.Code = ApiResponceConst.ErrorCode;
            apiResponse_New.Message = ApiResponceConst.ErrorMessage;
            return apiResponse_New;
        }





    }

    public async Task<List<string>> GetUserRolesAsync(long userId, CancellationToken cancellationToken)
    {

        var user =
            await _unitOfWork.UserRepository
            .GetByIdAsync(
                userId,
                cancellationToken);



        if (user == null)
            return new List<string>();



        return user.UserRole == null
            ? new List<string>()
            :
            new List<string>
            {
                user.UserRole.Role.Code.ToString()
            };

    }



    public async Task<List<string>> GetUserPermissionsAsync(
        long userId,
        CancellationToken cancellationToken)
    {


        var user =
            await _unitOfWork.UserRepository
            .GetByIdAsync(
                userId,
                cancellationToken);



        if (user == null)
            return new List<string>();



        if (user.UserRole == null)
            return new List<string>();



        var permissions =
            user.UserRole
            .Role
            .RolePermissions
            .Select(x =>
                x.Permission.Code)
            .ToList();



        return permissions;

    }


    public async Task<ApiResponse_New<bool>> UpdateAsync(
    PermissionUpdateDto dto,
    CancellationToken cancellationToken)
    {
        var response = new ApiResponse_New<bool>();

        try
        {

            var permission =
                await _unitOfWork.PermissionRepository
                .GetByIdAsync(dto.Id, cancellationToken);


            if (permission == null)
            {
                response.Errors =
                [
                    "Permission not found"
                ];

                return response;
            }
            PermissionType type = dto.Type == 1 ? PermissionType.Menu : PermissionType.Permission;

            permission.Update(
                dto.Title,
                dto.Code,
                type,
                dto.Description,
                dto.ParentId);



            _unitOfWork.PermissionRepository.Update(permission);


            await _unitOfWork.SaveChangesAsync(cancellationToken);


            response.IsSuccess = true;
            response.Payload = true;

        }
        catch (Exception ex)
        {
            response.Errors =
            [
                ex.Message
            ];
        }


        return response;
    }
    public async Task<ApiResponse_New<int>> CreateAsync(
    PermissionCreateDto dto,
    CancellationToken cancellationToken)
    {
        var response = new ApiResponse_New<int>();

        try
        {
            var permission = Permission.Create(
    dto.Title,
    dto.Code,
   (PermissionType)dto.Type,
    dto.Description,
    dto.ParentId);


            await _unitOfWork.PermissionRepository
                .AddAsync(permission, cancellationToken);


            await _unitOfWork.SaveChangesAsync(cancellationToken);


            response.IsSuccess = true;
            response.Payload = permission.Id;

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Errors = new List<string>
        {
            ex.Message
        };
        }


        return response;
    }

    public async Task<ApiResponse_New<bool>> DeleteAsync(
    int id,
    CancellationToken cancellationToken)
    {
        var response = new ApiResponse_New<bool>();

        try
        {
            var curentUser = await _currentUserService.GetCurrentUserAsync();
            await _unitOfWork.PermissionRepository
                .DeletePermissiByIdAsync(
                    id,
                    curentUser.Id,
                    cancellationToken);


            await _unitOfWork.SaveChangesAsync(cancellationToken);


            response.IsSuccess = true;
            response.Payload = true;

        }
        catch (Exception ex)
        {
            response.Errors =
            [
                ex.Message
            ];
        }


        return response;
    }
}