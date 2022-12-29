using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
	public class AuthController : Controller
	{
		public IActionResult Register()
		{
			return View();
		}
	}
}
