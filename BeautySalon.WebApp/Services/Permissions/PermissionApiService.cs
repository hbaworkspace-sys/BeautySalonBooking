using System.Net.Http.Json;
using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.WebApp.Interfaces.Permissions;


namespace BeautySalonBooking.WebApp.Services.Permissions;


public class PermissionApiService
    : IPermissionApiService
{

    private readonly HttpClient _httpClient;


    public PermissionApiService(
        HttpClient httpClient)
    {
        _httpClient = httpClient;
    }



    public async Task<List<PermissionTreeDto>> GetTreeAsync(
        CancellationToken cancellationToken = default)
    {

        var result =
            await _httpClient
            .GetFromJsonAsync<List<PermissionTreeDto>>(
                "api/permissions/tree",
                cancellationToken);


        return result ?? new List<PermissionTreeDto>();
    }



    public async Task<PermissionTreeDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {

        return await _httpClient
            .GetFromJsonAsync<PermissionTreeDto>(
                $"api/permissions/{id}",
                cancellationToken);

    }



    public async Task<bool> CreateAsync(
        PermissionTreeDto model,
        CancellationToken cancellationToken = default)
    {

        var response =
            await _httpClient
            .PostAsJsonAsync(
                "api/permissions",
                model,
                cancellationToken);


        return response.IsSuccessStatusCode;
    }



    public async Task<bool> UpdateAsync(
        PermissionTreeDto model,
        CancellationToken cancellationToken = default)
    {

        var response =
            await _httpClient
            .PutAsJsonAsync(
                $"api/permissions/{model.Id}",
                model,
                cancellationToken);


        return response.IsSuccessStatusCode;

    }



    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {

        var response =
            await _httpClient
            .DeleteAsync(
                $"api/permissions/{id}",
                cancellationToken);


        return response.IsSuccessStatusCode;

    }

}