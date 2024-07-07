using System.Net.Http.Json;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.UI.Services;

public class UserRoleService : UsersInterface<AspNetUserRoles>
{
    private readonly HttpClient _httpClient;

    public UserRoleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<AspNetUserRoles>> GetAllAsync()
    {
        return (await _httpClient.GetFromJsonAsync<List<AspNetUserRoles>>("https://localhost:7097/api/UserRole"))!;
    }

    public async Task<AspNetUserRoles> GetByIdAsync(int id)
    {
        return (await _httpClient.GetFromJsonAsync<AspNetUserRoles>($"api/Users/{id}"))!;
    }

    public async Task<AspNetUserRoles> AddAsync(AspNetUserRoles entity)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7097/api/UserRole", entity);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<AspNetUserRoles>();
    }

    public async Task<bool> UpdateAsync(AspNetUserRoles entity)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Users/{entity.UserId}", entity);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/Users/{id}");
        return response.IsSuccessStatusCode;
    }
  

}
