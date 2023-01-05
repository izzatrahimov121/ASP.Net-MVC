using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin,Manager")]
public class DashboardController : Controller
{

    public IActionResult Index()
    {
        return View();
    }
}
