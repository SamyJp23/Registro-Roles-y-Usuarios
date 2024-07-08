using System.ComponentModel.DataAnnotations;

namespace UserBlazorApp.UI.Dto;

public class RolResponse
{
     public int Id { get; set; }
    [Required(ErrorMessage = "El nombre del rol es obligatorio.")]
    public string? Name { get; set; }
}
