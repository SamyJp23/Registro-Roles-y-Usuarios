using System.Net.Http.Json;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.UI.Services;

public class UserService : UsersInterface<AspNetUsers>
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<AspNetUsers>> GetAllAsync()
    {
        return (await _httpClient.GetFromJsonAsync<List<AspNetUsers>>("https://localhost:7097/api/User"))!;
    }

    public async Task<AspNetUsers> GetByIdAsync(int id)
    {
        return (await _httpClient.GetFromJsonAsync<AspNetUsers>($"api/Users/{id}"))!;
    }

    public async Task<AspNetUsers> AddAsync(AspNetUsers entity)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7097/api/User", entity);
        return (await response.Content.ReadFromJsonAsync<AspNetUsers>())!;
    }

    public async Task<bool> UpdateAsync(AspNetUsers entity)
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
