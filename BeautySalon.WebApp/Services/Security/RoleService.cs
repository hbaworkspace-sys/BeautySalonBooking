// WebApp/Services/Roles/RoleService.cs
using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Contracts.Permission.Dtos;
using BeautySalonBooking.WebApp.Interfaces.Common;
using BeautySalonBooking.WebApp.Interfaces.Roles;
using BeautySalonBooking.WebApp.Settings;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace BeautySalonBooking.WebApp.Services.Roles
{
    public class RoleService : IRoleService
    {
        private readonly IApiClient _apiClient;
        private readonly ILogger<RoleService> _logger;
        private readonly ApiSettings _settings;

        public RoleService(
            IApiClient apiClient,
            IOptions<ApiSettings> settings,
            ILogger<RoleService> logger)
        {
            _apiClient = apiClient;
            _settings = settings.Value;
            _logger = logger;
        }


        public async Task<RoleListResponse> GetRolesAsync(string? searchTerm = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Getting roles with search: {SearchTerm}, page: {PageNumber}", searchTerm, pageNumber);

                var endpoint = $"{_settings.Endpoints.Roles.GetAll}?searchTerm={searchTerm}&pageNumber={pageNumber}&pageSize={pageSize}";
                var result = await _apiClient.GetAsync<RoleListResponse>(endpoint);

                return result ?? new RoleListResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetRolesAsync");
                return new RoleListResponse();
            }
        }

        public async Task<RoleDto> GetRoleByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Getting role by id: {Id}", id);

                var endpoint = $"{_settings.Endpoints.Roles.GetById}/{id}";
                var result = await _apiClient.GetAsync<RoleDto>(endpoint);

                return result ?? throw new Exception("نقش مورد نظر یافت نشد.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetRoleByIdAsync for id: {Id}", id);
                throw;
            }
        }

        public async Task<RoleDto> CreateRoleAsync(CreateRoleRequest request)
        {
            try
            {
                _logger.LogInformation("Creating new role: {Title}", request.Title);

                var endpoint = _settings.Endpoints.Roles.Create;
                var result = await _apiClient.PostAsync<CreateRoleRequest, RoleDto>(endpoint, request);

                return result ?? throw new Exception("خطا در ایجاد نقش جدید.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateRoleAsync");
                throw;
            }
        }

        public async Task<RoleDto> UpdateRoleAsync(UpdateRoleRequest request)
        {
            try
            {
                _logger.LogInformation("Updating role: {Id}, {Title}", request.Id, request.Title);

                var endpoint = $"{_settings.Endpoints.Roles.Update}/{request.Id}";
                var result = await _apiClient.PutAsync<UpdateRoleRequest, RoleDto>(endpoint, request);

                return result ?? throw new Exception("خطا در ویرایش نقش.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateRoleAsync for id: {Id}", request.Id);
                throw;
            }
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting role: {Id}", id);

                var endpoint = $"{_settings.Endpoints.Roles.Delete}/{id}";
                await _apiClient.DeleteAsync<object>(endpoint);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteRoleAsync for id: {Id}", id);
                return false;
            }
        }
    }
}