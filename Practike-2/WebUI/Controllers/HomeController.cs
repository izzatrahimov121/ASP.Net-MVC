using DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.ViewModel;

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
				Employees = _context.Employees.Take(4).AsNoTracking()
			};
			return View(homeVM);
		}
		public IActionResult LoadMore()
		{
			var employee = _context.Employees.Take(4).AsNoTracking();
			return PartialView("_EmployeePartial", employee);
		}
	}
}
