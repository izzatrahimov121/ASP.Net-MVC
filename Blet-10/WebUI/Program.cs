using DataAccess.Contexts;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var constr = builder.Configuration["ConnectionStrings:Default"];
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(constr);
});

builder.Services.AddScoped<ISpecialTeamRepository, SpecialTeamRepository>();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{Id?}"
);
app.Run();
