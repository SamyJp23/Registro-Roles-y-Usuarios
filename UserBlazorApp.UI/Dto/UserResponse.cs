using System.ComponentModel.DataAnnotations;
namespace UserBlazorApp.UI.Dto;

public class UserResponse
{
    public int Id { get; set; }
  
    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    public string? UserName { get; set; }
    [Required(ErrorMessage = "El email de usuario es obligatorio.")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "El numero de telefono del usuario es obligatorio.")]
    public string? PhoneNumber { get; set; }
    public List<RolResponse> Roles { get; set; } = new List<RolResponse>();
}
