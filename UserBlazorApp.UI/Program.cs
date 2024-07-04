using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UserBlazorApp.UI;
using UserBlazorApp.UI.Services;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.Services.AddBlazorBootstrap();
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7097/") });
builder.Services.AddScoped<UsersInterface<AspNetUsers>, UserService>();
builder.Services.AddScoped<UsersInterface<AspNetRoles>, RoleService>();
builder.Services.AddScoped<UsersInterface<AspNetRoleClaims>, UserClaimService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
