using System.Net.Http.Json;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;
using UserBlazorApp.UI.Dto;

namespace UserBlazorApp.UI.Services;

public class UserService : UsersInterface<UserResponse>
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<UserResponse>> GetAllAsync()
    {
        var users = await _httpClient.GetFromJsonAsync<List<AspNetUsers>>("https://localhost:7097/api/User");
        return users.Select(user => new UserResponse
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Roles = user.Role.Select(role => new RolResponse
            {
                Id = role.Id,
                Name = role.Name
            }).ToList()
        }).ToList();
    }

    public async Task<UserResponse> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetFromJsonAsync<UserResponse>($"https://localhost:7097/api/User/{id}");
        return response;
    }

    public async Task<UserResponse> AddAsync(UserResponse entity)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7097/api/User", entity);
        return (await response.Content.ReadFromJsonAsync<UserResponse>())!;
    }


    public async Task<bool> UpdateAsync(UserResponse entity)
    {
        var response = await _httpClient.PutAsJsonAsync($"https://localhost:7097/api/User/{entity.Id}", entity);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"https://localhost:7097/api/User/{id}");
        return response.IsSuccessStatusCode;
    }
    public async Task<bool> UpdateUserRequestAsync(UserRequest entity)
    {
        var response = await _httpClient.PutAsJsonAsync($"https://localhost:7097/api/User/{entity.Id}", entity);
        return response.IsSuccessStatusCode;
    }

}
