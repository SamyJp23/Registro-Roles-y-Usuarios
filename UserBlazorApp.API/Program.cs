
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using UserBlazorApp.API.Context;
using UserBlazorApp.API.Services;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UsersDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("ConStr")));
builder.Services.AddScoped<ApiInterface<AspNetUsers>, UsersServices>();

//Cors

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseCors("AllowAll");

app.UseAuthorization();

app.Run();
