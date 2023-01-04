using Core.Entities;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebUI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles ="Admin")]
public class UserManagerController : Controller
{
	public readonly AppDbContext _context;
    private readonly DbSet<AppUser> _table;

    public UserManagerController(AppDbContext context, DbSet<AppUser> table)
    {
        _context = context;
        _table = _context.Set<AppUser>();
    }

    public async Task<IActionResult> Index()
	{
        //return View(_context.Users);
        return View(await _table.ToListAsync());
    }
}
