using DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
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
				SpecialTeams = _context.SpecialTeams
			};

            return View(homeVM);
		}
	}
}
