using Event_Applikation.Models;
using Event_Applikation.Services.Interfaces;
using Event_Applikation.Services.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<mvp2_dk_db_eventapplikationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRazorPages(options =>
{
    // Angiv hvilke foldere login giver adgang til
    options.Conventions.AuthorizeFolder("/Events/Create");
});

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IBrugerRepository, BrugerRepository>();

// Angiv og konfigurér cookie-baseret Authentication
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
    {
        options.LoginPath = "/BrugerLogin/Login";
        options.AccessDeniedPath = "/BrugerLogin/AccessDenied";
    });

//builder
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Aktivér cookie-baseret Authentication ifm. login

app.UseAuthorization();

app.MapRazorPages();

app.Run();
