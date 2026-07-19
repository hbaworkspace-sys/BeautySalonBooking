using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Contracts.Common.Enums;
using BeautySalonBooking.Contracts.UserRoles.DTOs;
using BeautySalonBooking.Contracts.UserRoles.Requests;
using BeautySalonBooking.Contracts.UserRoles.Responses;
using BeautySalonBooking.Domain.UserAggregate.Entities;
using BeautySalonBooking.Domain.UserAggregate.Enums;
using BeautySalonBooking.Domain.UserAggregate.Repositories;

namespace BeautySalonBooking.Application.UserRoles
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task<ApiResponse> AssignRoleAsync(Guid userId, AssignRoleRequest request)
        {
            var role = (RoleType)request.Role;

            var existingRole = await _userRoleRepository.GetAsync(userId, role);

            if (existingRole != null)
            {
                return new ApiResponse
                {
                    //خطایی در ذخیره سازی نداریم چون قبلا ثبت شده مقدار true را برمیگردانیم
                    IsSuccess = true,
                    Message = "این نقش قبلاً برای کاربر ثبت شده است"
                };
            }

            var userRole = UserRole.Create(userId, role);

            await _userRoleRepository.AddAsync(userRole);
            await _userRoleRepository.SaveChangesAsync();

            return new ApiResponse
            {
                IsSuccess = true,
                Message = "نقش با موفقیت ثبت شد"
            };
        }

        public async Task<ApiResponse<UserRolesResponse>> GetUserRolesAsync(Guid userId)
        {
            var roles = await _userRoleRepository.GetUserRolesAsync(userId);

            return new ApiResponse<UserRolesResponse>
            {
                IsSuccess = true,
                Data = new UserRolesResponse
                {
                    Roles = roles.Select(x => new UserRoleDto
                    {
                        Role = (UserTypeDto)x.Role,
                        IsActive = x.IsActive,
                        CreatedAt = x.CreatedAt
                    }).ToList()
                }
            };
        }

        public async Task<ApiResponse<ActiveRoleResponse>> GetActiveRoleAsync(Guid userId)
        {
            var role = await _userRoleRepository.GetActiveRoleAsync(userId);

            if (role == null)
            {
                return new ApiResponse<ActiveRoleResponse>
                {
                    IsSuccess = false,
                    Message = "نقش فعالی برای کاربر یافت نشد"
                };
            }

            return new ApiResponse<ActiveRoleResponse>
            {
                IsSuccess = true,
                Data = new ActiveRoleResponse
                {
                    Role = (UserTypeDto)role.Role
                }
            };
        }

        public async Task<ApiResponse> SwitchRoleAsync(Guid userId, SwitchRoleRequest request)
        {
            var role = (RoleType)request.Role;

            var roles = await _userRoleRepository.GetUserRolesAsync(userId);

            foreach (var item in roles.Where(x => x.IsActive))
            {
                item.Deactivate();
            }

            var targetRole = roles.FirstOrDefault(x => x.Role == role);

            if (targetRole == null)
            {
                targetRole = UserRole.Create(userId, role);
                await _userRoleRepository.AddAsync(targetRole);
            }
            else
            {
                targetRole.Activate();
            }

            await _userRoleRepository.SaveChangesAsync();

            return new ApiResponse
            {
                IsSuccess = true,
                Message = "نقش فعال با موفقیت تغییر یافت"
            };
        }
    }
}