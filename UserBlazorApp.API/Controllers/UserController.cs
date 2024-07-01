using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersBlazorApp.Data.Context;
using UsersBlazorApp.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserBlazorApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(UsersDbContext Context) : ControllerBase
{


    // GET api/<UserController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        /*        var claims = await Context.AspNetUsers
                    .Where(x => x.Id == id)
                    .Select(u =>
                             u.Role.SelectMany(r => r.AspNetRoleClaims
                                .Select(c => new ClaimDto(c.ClaimType))
                            )
                    )
                    .ToListAsync();*/


        var query = from user in Context.AspNetUsers
                where user.Id == id
                from r in user.Role
                from c in r.AspNetRoleClaims
                select new ClaimDto(c.ClaimType);

        var claims = await query.ToListAsync();

        return Ok(claims);
    }
    [HttpPost("Agregar")]
    public async Task<IActionResult> Agregar(AspNetUsers user)
    {
  
        var nuevo = new AspNetUsers
        {
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            EmailConfirmed = user.EmailConfirmed,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            TwoFactorEnabled = user.TwoFactorEnabled,
           
        };

        Context.AspNetUsers.Add(nuevo);
        await Context.SaveChangesAsync();

        return Ok(nuevo);
    }
    [HttpGet("Listar")]
    public async Task<IActionResult> GetRoleClaims()
    {
        var user = await Context.AspNetUsers.ToListAsync();
        return Ok(user);
    }

    public record UserDto1(int UsuarioId, List<ClaimDto> Claims) { }

    public class UserDto
    {
        public int UsuarioId { get; set; }
        public List<ClaimDto> Claims { get; set; } = [];
    }

    public record ClaimDto(string? Name) { }

}
