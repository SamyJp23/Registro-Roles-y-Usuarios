using Microsoft.EntityFrameworkCore;
using UserBlazorApp.API.Context;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Services;

public class UserRolesServices(UsersDbContext Contexto) : ApiInterface<AspNetUserRoles>
{

    public async Task<List<AspNetUserRoles>> GetAllAsync()
    {
        return await Contexto.AspNetUserRoles.ToListAsync();
    }

    public async Task<AspNetUserRoles> GetByIdAsync(int id)
    {
        return (await Contexto.AspNetUserRoles.FindAsync(id))!;
    }

    public async Task<AspNetUserRoles> AddAsync(AspNetUserRoles rol)
    {
        Contexto.AspNetUserRoles.Add(rol);
        await Contexto.SaveChangesAsync();
        return rol;
    }

    public async Task<bool> UpdateAsync(AspNetUserRoles rol)
    {
        Contexto.Entry(rol).State = EntityState.Modified;
        return await Contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var rol = await Contexto.AspNetRoles.FindAsync(id);
        if (rol == null)
            return false;

        Contexto.AspNetRoles.Remove(rol);
        return await Contexto.SaveChangesAsync() > 0;
    }

}
