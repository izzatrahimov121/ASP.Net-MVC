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
	private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;

	public UserManagerController(AppDbContext context, UserManager<AppUser> userManager)
	{
		_context = context;
		_userManager = userManager;
	}

	public async Task<IActionResult> Index()
	{
        return View(_userManager.Users);
    }
}
