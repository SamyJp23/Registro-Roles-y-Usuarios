using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UserBlazorApp.API.Context;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleClaimsController : ControllerBase
    {
        private readonly UsersDbContext _context;

        public RoleClaimsController(UsersDbContext context)
        {
            _context = context;
        }

        // GET: RoleClaims
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var usersDbContext = _context.AspNetRoleClaims.Include(a => a.Role);
            return Ok(await usersDbContext.ToListAsync());
        }

      

        
        

        

      
    }
}
