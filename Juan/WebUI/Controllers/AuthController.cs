using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using WebUI.ViewsModels;
using static WebUI.Utilites.Helper;

namespace WebUI.Controllers;

public class AuthController : Controller
{

	private readonly UserManager<AppUser> _userManager;
	private readonly SignInManager<AppUser> _signInManager;
	private readonly RoleManager<IdentityRole> _roleManager;

	public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager = null,
		RoleManager<IdentityRole> roleManager = null)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_roleManager = roleManager;
	}

	#region Register
	public IActionResult Register()
	{
		return View();
	}
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Register(RegisterViewModel registerVM)
	{
		if (!ModelState.IsValid) { return View(registerVM); }
		AppUser appUser = new()
		{
			Fullname = registerVM.Fullname,
			UserName = registerVM.Username,
			Email = registerVM.Email,
			IsActive = true
		};
		var identityResult = await _userManager.CreateAsync(appUser, registerVM.Password);
		if (!identityResult.Succeeded)
		{
			foreach (var error in identityResult.Errors)
			{
				ModelState.AddModelError("", error.Description);
			}
			return View(registerVM);
		}
		await _userManager.AddToRoleAsync(appUser, RoleType.Member.ToString());
		return RedirectToAction(nameof(Login));
	}
	#endregion

	#region Login and Logout
	public IActionResult Login()
	{
		return View();
	}
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(LoginViewModel loginViewModel)
	{
		if (!ModelState.IsValid) return Content("ModelState");
		var user = await _userManager.FindByNameAsync(loginViewModel.UsernameOrEmail);
		if (user == null)
		{
			user = await _userManager.FindByEmailAsync(loginViewModel.UsernameOrEmail);
			if (user == null)
			{
				ModelState.AddModelError("", "Username/Email or Password incorrect");
				return View(loginViewModel);
			}

		}
		var signInResult = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, (bool)loginViewModel.RememberMe, true);

		if (signInResult.IsLockedOut)
		{
			ModelState.AddModelError("", "Biraz gözle");
			//return Content("IsLockedOut");
			return View(loginViewModel);
		}
		if (!signInResult.Succeeded)
		{
			ModelState.AddModelError("", "Username/Email or Password incorrect");
			//return Content("Succeeded");
			return View(loginViewModel);
		}
		if ((bool)!user.IsActive)
		{
			ModelState.AddModelError("", "not found");
			//return Content("IsActive");
			return View(loginViewModel);
		}
		return RedirectToAction("Index", "Home");
	}

	public async Task<IActionResult> Logout()
	{
		if (User.Identity.IsAuthenticated)
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
		return BadRequest();
	}
	#endregion



	///////////User Roles///////////////////
	//public async Task<IActionResult> CreateRoles()
	//{
	//	foreach (var role in Enum.GetValues(typeof(RoleType)))
	//	{
	//		if (!await _roleManager.RoleExistsAsync(role.ToString()))
	//		{
	//			await _roleManager.CreateAsync(new IdentityRole(role.ToString()));
	//		}
	//	}

	//	return Content("ok");
	//}

}
