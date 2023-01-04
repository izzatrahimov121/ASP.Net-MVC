using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccess.Contexts;

public class AppDbContext : IdentityDbContext<AppUser>
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}
	public DbSet<SlideItem> SlideItems { get; set; } = null!;
}
