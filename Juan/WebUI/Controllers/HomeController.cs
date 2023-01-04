using DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;
using WebUI.ViewsModels;

namespace WebUI.Controllers;

public class HomeController : Controller
{
	private readonly AppDbContext _context;

	public HomeController(AppDbContext context)
	{
		_context = context;
	}

	public IActionResult Index()
	{
		HomeViewModel homeVM = new()
		{
			SlideItems = _context.SlideItems
		};
		return View(homeVM);
	}
}