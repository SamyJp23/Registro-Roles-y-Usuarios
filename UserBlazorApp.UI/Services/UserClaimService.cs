using System.Net.Http.Json;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.UI.Services;

public class UserClaimService : UsersInterface<AspNetRoleClaims>
{
    private readonly HttpClient _httpClient;

    public UserClaimService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<AspNetRoleClaims>> GetAllAsync()
    {
        return (await _httpClient.GetFromJsonAsync<List<AspNetRoleClaims>>("https://localhost:7097/api/Roles"))!;
    }

    public async Task<AspNetRoleClaims> GetByIdAsync(int id)
    {
        return (await _httpClient.GetFromJsonAsync<AspNetRoleClaims>($"api/Users/{id}"))!;
    }

    public async Task<AspNetRoleClaims> AddAsync(AspNetRoleClaims entity)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7097/api/Roles", entity);
        return (await response.Content.ReadFromJsonAsync<AspNetRoleClaims>())!;
    }

    public async Task<bool> UpdateAsync(AspNetRoleClaims entity)
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
