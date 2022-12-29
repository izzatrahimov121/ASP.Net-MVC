using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);
var constr = builder.Configuration["ConnectionStrings:Default"];
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(constr);
});



builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    //Password
    opt.Password.RequiredLength = 8;
    opt.Password.RequireNonAlphanumeric = true;
    opt.Password.RequireDigit = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = true;
    //User
    opt.User.RequireUniqueEmail = true;


    opt.Lockout.MaxFailedAccessAttempts= 5;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
    opt.Lockout.AllowedForNewUsers= true;
}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddSession(opt =>
{
	opt.IdleTimeout = TimeSpan.FromSeconds(30);
});
builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = "/Auth/Login";
});

builder.Services.AddScoped<IShippingItemRepository,ShippingItemRepository>();
builder.Services.AddScoped<ISlideItemReposiyory,SlideItemRepository>();

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
