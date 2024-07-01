using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersBlazorApp.Data.Context;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Controllers;

public class RolesController(UsersDbContext Contexto) : ControllerBase
{
    [HttpPost("Agregar")]
    public async Task<IActionResult> Agregar(AspNetRoles rol)
    {
        var nuevo = new AspNetRoles
        {
            Id = rol.Id,
            Name = rol.Name,
            AspNetRoleClaims = rol.AspNetRoleClaims.Select(c => new AspNetRoleClaims
            {
                ClaimType = c.ClaimType,
                ClaimValue = c.ClaimValue
            }).ToList()
        };
        Contexto.AspNetRoles.Add(nuevo);
        await Contexto.SaveChangesAsync();

        return Ok(nuevo);
    }
    [HttpGet("Listar")]
    public async Task<IActionResult> Listar()
    {
        var roles = await Contexto.AspNetRoles.ToListAsync();
        return Ok(roles);
    }
}