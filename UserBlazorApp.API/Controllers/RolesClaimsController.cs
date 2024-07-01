using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersBlazorApp.Data.Context;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Controllers
{
    public class RolesClaimsController(UsersDbContext Contexto) : ControllerBase
    {
        [HttpPost("Agregar")]
        public async Task<IActionResult> AddRoleClaim(AspNetRoleClaims claim)
        {

            var role = await Contexto.AspNetRoles.FindAsync(claim.RoleId);

            var nuevo = new AspNetRoleClaims
            {
                RoleId = claim.RoleId,
                ClaimType = claim.ClaimType,
                ClaimValue = claim.ClaimValue
            };

            Contexto.AspNetRoleClaims.Add(nuevo);
            await Contexto.SaveChangesAsync();

            return Ok(nuevo);
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> Listar()
        {
            var claims = await Contexto.AspNetRoleClaims.ToListAsync();
            return Ok(claims);
        }
    }
}
