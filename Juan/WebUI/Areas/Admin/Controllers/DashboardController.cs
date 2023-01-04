using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Manager")]//login olmayanlari admin panele buraxmayacaq
public class DashboardController : Controller
{

    public IActionResult Index()
	{
		return View();
	}
}
