using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UserBlazorApp.API.Context;
using UsersBlazorApp.Data.Models;
using UserBlazorApp.API.Dto;

namespace UserBlazorApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserRoleController : ControllerBase
{
    private readonly UsersDbContext _context;

    public UserRoleController(UsersDbContext context)
    {
        _context = context;
    }

    // GET: UserRole
    [HttpGet]
    [HttpGet]
    public async Task<ActionResult<List<UserRoleDto>>> GetUserRoles()
    {
        var userRoles = await _context.AspNetUserRoles
                                      .Select(ur => new UserRoleDto
                                      {
                                          UserId = ur.UserId,
                                          RoleId = ur.RoleId
                                      }).ToListAsync();
        return Ok(userRoles);
    }


    [HttpPost]
    [HttpPost]
    public async Task<ActionResult<UserRoleDto>> PostUserRole(UserRoleDto userRoleDto)
    {
        var userRole = new AspNetUserRoles
        {
            UserId = userRoleDto.UserId,
            RoleId = userRoleDto.RoleId
        };

        _context.AspNetUserRoles.Add(userRole);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUserRoles), new { userId = userRole.UserId, roleId = userRole.RoleId }, userRoleDto);
    }

}
