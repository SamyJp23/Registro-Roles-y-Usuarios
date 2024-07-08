using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using UserBlazorApp.UI.Dto;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;


namespace UserBlazorApp.UI.Services;

public class RoleService : UsersInterface<RolResponse>
{
    private readonly HttpClient _httpClient;

    public RoleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<RolResponse>> GetAllAsync()
    {
        return (await _httpClient.GetFromJsonAsync<List<RolResponse>>("https://localhost:7097/api/Role"))!;
    }

    public async Task<RolResponse> GetByIdAsync(int id)
    {
        return (await _httpClient.GetFromJsonAsync<RolResponse>($"https://localhost:7097/api/Role/{id}"))!;
    }

    public async Task<RolResponse> AddAsync(RolResponse entity)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7097/api/Role", entity);
        return (await response.Content.ReadFromJsonAsync<RolResponse>())!;
    }

    public async Task<bool> UpdateAsync(RolResponse entity)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Users/{entity.Id}", entity);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"https://localhost:7097/api/Role/{id}");
        return response.IsSuccessStatusCode;
    }
  
    }
