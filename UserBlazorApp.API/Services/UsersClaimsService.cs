using Microsoft.EntityFrameworkCore;
using UserBlazorApp.API.Context;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Services;

public class UsersClaimsService(UsersDbContext Contexto) : ApiInterface<AspNetUserClaims>
{
    public async Task<List<AspNetUserClaims>> GetAllAsync()
    {
        return await Contexto.AspNetUserClaims.ToListAsync();
    }

    public async Task<AspNetUserClaims> GetByIdAsync(int id)
    {
        return (await Contexto.AspNetUserClaims.FindAsync(id))!;
    }

    public async Task<AspNetUserClaims> AddAsync(AspNetUserClaims claim)
    {
        Contexto.AspNetUserClaims.Add(claim);
        await Contexto.SaveChangesAsync();
        return claim;
    }

    public async Task<bool> UpdateAsync(AspNetUserClaims claim)
    {
        Contexto.Entry(claim).State = EntityState.Modified;
        return await Contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var rol = await Contexto.AspNetUserClaims.FindAsync(id);
        if (rol == null)
            return false;

        Contexto.AspNetUserClaims.Remove(rol);
        return await Contexto.SaveChangesAsync() > 0;
    }
}

