using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using UserBlazorApp.API.Context;
using UserBlazorApp.API.Dto;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly UsersDbContext _context;

    public RoleController(UsersDbContext context)
    {
        _context = context;
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<RolResponse>> GetRolById(int id)
    {
        var rol = await _context.AspNetRoles
                                .Include(r => r.AspNetRoleClaims)
                                .FirstOrDefaultAsync(r => r.Id == id);

        if (rol == null)
        {
            return NotFound();
        }

        var rolResponse = rol.Adapt<RolResponse>();
        return Ok(rolResponse);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RolResponse>>> GetRoles()
    {
        var roles = await _context.AspNetRoles.ToListAsync();

        var rolResponse = roles.Adapt<List<RolResponse>>();
        return Ok(rolResponse);
    }
    [HttpPost]
    public async Task<ActionResult<RolResponse>> AddRol(RolRequest rolRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var rol = new AspNetRoles
        {
            Name = rolRequest.Name,
        };

        _context.AspNetRoles.Add(rol);
        await _context.SaveChangesAsync();

        var rolResponse = new RolResponse
        {
            Id = rol.Id,
            Name = rol.Name,
        };

        return Ok(rolResponse);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRol(int id)
    {
        var rol = await _context.AspNetRoles
                                .Include(r => r.AspNetUserRoles)
                                .FirstOrDefaultAsync(r => r.Id == id);

        if (rol == null)
        {
            return NotFound();
        }

        _context.AspNetUserRoles.RemoveRange(rol.AspNetUserRoles);
        _context.AspNetRoles.Remove(rol);

        await _context.SaveChangesAsync();

        var rolResponse = rol.Adapt<RolResponse>();
        return Ok(rolResponse);
    }


}
