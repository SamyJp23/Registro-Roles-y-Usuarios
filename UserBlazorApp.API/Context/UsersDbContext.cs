using System;
using System.Collections.Generic;
using UsersBlazorApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace UserBlazorApp.API.Context;

public partial class UsersDbContext : DbContext
{
    public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
    {
    }



    public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }

    public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }

    public DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetUsers>(entity =>
        {
            entity.HasMany(d => d.Role)
                  .WithMany(p => p.User)
                  .UsingEntity<AspNetUserRoles>(
                      j => j.HasOne(ur => ur.Role)
                            .WithMany(r => r.AspNetUserRoles)
                            .HasForeignKey(ur => ur.RoleId),
                      j => j.HasOne(ur => ur.User)
                            .WithMany(u => u.AspNetUserRoles)
                            .HasForeignKey(ur => ur.UserId),
                      j =>
                      {
                          j.HasKey(ur => new { ur.UserId, ur.RoleId });
                      });
        });

        modelBuilder.Entity<AspNetRoles>(entity =>
        {
            entity.HasMany(r => r.AspNetRoleClaims)
                  .WithOne(rc => rc.Role)
                  .HasForeignKey(rc => rc.RoleId);
        });

        modelBuilder.Entity<AspNetUserRoles>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId });
        });

        modelBuilder.Entity<AspNetUserClaims>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<AspNetRoleClaims>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


}
