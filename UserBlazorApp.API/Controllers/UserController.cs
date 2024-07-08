using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UserBlazorApp.API.Context;
using UserBlazorApp.API.Dto;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UsersDbContext _context;

        public UserController(UsersDbContext context)
        {
            _context = context;
        }

        // GET: User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
        {
            var users = await _context.AspNetUsers
                                .Include(u => u.AspNetUserRoles)
                                .ThenInclude(ur => ur.Role)
                                .ToListAsync();

            var userResponses = users.Select(u => new UserResponse
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Roles = u.AspNetUserRoles.Select(ur => new RolResponse
                {
                    Id = ur.Role.Id,
                    Name = ur.Role.Name
                }).ToList()
            }).ToList();

            return Ok(userResponses);
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> AddUser(UserRequest userRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new AspNetUsers
            {
                UserName = userRequest.UserName,
                Email = userRequest.Email,
                PhoneNumber = userRequest.PhoneNumber,


            };

            _context.AspNetUsers.Add(user);
            await _context.SaveChangesAsync();

            var userResponse = new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return Ok(userResponse);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUserById(int id)
        {
            var user = await _context.AspNetUsers
                                     .Include(u => u.Role)
                                     .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var userResponse = new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = user.Role.Select(r => new RolResponse
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToList()
            };

            return Ok(userResponse);
        }

    }
}
