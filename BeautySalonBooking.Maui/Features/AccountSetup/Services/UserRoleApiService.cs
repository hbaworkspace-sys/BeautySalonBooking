using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Contracts.UserRoles.Requests;
using BeautySalonBooking.Contracts.UserRoles.Responses;
using BeautySalonBooking.Maui.Common.Services;
using BeautySalonBooking.Maui.Features.AccountSetup.Constants;

namespace BeautySalonBooking.Maui.Features.UserRoles
{
    public class UserRoleApiService : BaseApiService
    {
        public UserRoleApiService(HttpClient httpClient)
            : base(httpClient)
        {
        }

        public Task<ApiResponse<UserRolesResponse>> GetUserRolesAsync(Guid userId)
        {
            return SendAsync<object, UserRolesResponse>(
                HttpMethod.Get,
                UserRoleRoutes.GetUserRoles(userId),
                null);
        }

        public Task<ApiResponse<ActiveRoleResponse>> GetActiveRoleAsync(Guid userId)
        {
            return SendAsync<object, ActiveRoleResponse>(
                HttpMethod.Get,
                UserRoleRoutes.GetActiveRole(userId),
                null);
        }

        public async Task<ApiResponse> SwitchRoleAsync(Guid userId, SwitchRoleRequest request)
        {
            var result = await SendAsync<SwitchRoleRequest, object>(
                HttpMethod.Put,
                UserRoleRoutes.SwitchRole(userId),   
                request);

            return new ApiResponse
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                Errors = result.Errors
            };
        }

        public async Task<ApiResponse> AssignRoleAsync(Guid userId, AssignRoleRequest request)
        {
            var result = await SendAsync<AssignRoleRequest, object>(
                HttpMethod.Post,
                UserRoleRoutes.AssignRole(userId),  
                request);

            return new ApiResponse
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                Errors = result.Errors
            };
        }
    }
}