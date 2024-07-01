using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersBlazorApp.Data.Context;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Controllers
{
    public class UserClaimsController(UsersDbContext Contexto) : ControllerBase
    {

        [HttpPost("Agregar")]
        public async Task<IActionResult> AddClaim(AspNetUserClaims claim)
        {

            var user = await Contexto.AspNetUsers.FindAsync(claim.UserId);
            
            var nuevo = new AspNetUserClaims
            {
                UserId = claim.UserId,
                ClaimType = claim.ClaimType,
                ClaimValue = claim.ClaimValue
            };

            Contexto.AspNetUserClaims.Add(nuevo);
            await Contexto.SaveChangesAsync();

            return Ok(nuevo);
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> GetRoleClaims()
        {
            var claims = await Contexto.AspNetUserClaims.ToListAsync();
            return Ok(claims);
        }
    }
}
