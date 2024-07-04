using System.Net.Http.Json;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.UI.Services;

public class RoleService : UsersInterface<AspNetRoles>
{
    private readonly HttpClient _httpClient;

    public RoleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<AspNetRoles>> GetAllAsync()
    {
        return (await _httpClient.GetFromJsonAsync<List<AspNetRoles>>("https://localhost:7097/api/Roles"))!;
    }

    public async Task<AspNetRoles> GetByIdAsync(int id)
    {
        return (await _httpClient.GetFromJsonAsync<AspNetRoles>($"api/Users/{id}"))!;
    }

    public async Task<AspNetRoles> AddAsync(AspNetRoles entity)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7097/api/Roles", entity);
        return (await response.Content.ReadFromJsonAsync<AspNetRoles>())!;
    }

    public async Task<bool> UpdateAsync(AspNetRoles entity)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Users/{entity.Id}", entity);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/Users/{id}");
        return response.IsSuccessStatusCode;
    }
}
