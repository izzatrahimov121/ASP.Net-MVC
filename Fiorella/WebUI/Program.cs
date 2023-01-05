using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var constr = builder.Configuration["ConnectionStrings:Default"];
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(constr);
});

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
	opt.Password.RequiredLength = 8;
	opt.Password.RequireDigit = true;
	opt.Password.RequireLowercase = true;
	opt.Password.RequireUppercase = true;
	opt.Password.RequireNonAlphanumeric = true;

	opt.User.RequireUniqueEmail = true;

	opt.Lockout.MaxFailedAccessAttempts = 5;
	opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(10);
	opt.Lockout.AllowedForNewUsers = true;
}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromSeconds(15);
});

builder.Services.ConfigureApplicationCookie(opt =>
{
	opt.LoginPath = "/Auth/Login";
});

builder.Services.AddScoped<ISlideItemRepository, SlideItemRepository>();

var app = builder.Build();
app.UseStaticFiles();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{Id?}"
    );

app.Run();
