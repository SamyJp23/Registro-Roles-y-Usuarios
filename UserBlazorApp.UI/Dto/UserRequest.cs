namespace UserBlazorApp.UI.Dto;

public class UserRequest
{

    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public List<int> RoleIds { get; set; } = new List<int>();
}
