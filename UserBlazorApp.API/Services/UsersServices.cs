﻿using Microsoft.EntityFrameworkCore;
using UserBlazorApp.API.Context;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Services;

public class UsersServices(UsersDbContext Contexto) : ApiInterface<AspNetUsers>
{
    public async Task<List<AspNetUsers>> GetAllAsync()
    {
        return await Contexto.AspNetUsers.ToListAsync();
    }

    public async Task<AspNetUsers> GetByIdAsync(int id)
    {
        return (await Contexto.AspNetUsers.FindAsync(id))!;
    }

    public async Task<AspNetUsers> AddAsync(AspNetUsers user)
    {
        Contexto.AspNetUsers.Add(user);
        await Contexto.SaveChangesAsync();
        return user;
    }

    public async Task<bool> UpdateAsync(AspNetUsers user)
    {
        Contexto.Entry(user).State = EntityState.Modified;
        return await Contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var usuario = await Contexto.AspNetUsers.FindAsync(id);
        if (usuario == null)
            return false;

        Contexto.AspNetUsers.Remove(usuario);
        return await Contexto.SaveChangesAsync() > 0;
    }
}
