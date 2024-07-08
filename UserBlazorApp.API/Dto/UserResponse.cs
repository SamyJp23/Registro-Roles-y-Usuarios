
namespace UserBlazorApp.API.Dto;

public class UserResponse
{
     public int Id { get; set; }
  
    public string? UserName { get; set; }
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
    public List<RolResponse> Roles { get; set; } = new List<RolResponse>();
}
