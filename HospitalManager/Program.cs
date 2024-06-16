using HospitalManager.Data;
using HospitalManager.Interfaces;
using HospitalManager.Models;
using HospitalManager.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IReceptionRepository, ReceptionRepository>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

var app = builder.Build();
if (args.Length == 1 && args[0].ToLower() == "deletetable")
{
    Delete.DeleteTable(app);
}

if (args.Length == 1 && args[0].ToLower() == "clearusers")
{
    Delete.ClearUsers(app);
}

if (args.Length == 1 && args[0].ToLower() == "clearreceptions")
{
    Delete.ClearReceptions(app);
}

if (args.Length == 1 && args[0].ToLower() == "seedreceptions")
{
    await Seed.SeedReceptions(app);
}

if (args.Length == 1 && args[0].ToLower() == "seedusers")
{
    await Seed.SeedUsersAndRolesAsync(app);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Reception}/{action=Index}/{id?}");

app.Run();
