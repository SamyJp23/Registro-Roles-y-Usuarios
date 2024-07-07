using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    // GET: Role
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var roles = await _context.AspNetRoles.ToListAsync();
        return Ok(roles);
    }
    [HttpPost]
    public async Task<ActionResult<RolResponse>> AddUser(RolRequest rolRequest)
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


}
