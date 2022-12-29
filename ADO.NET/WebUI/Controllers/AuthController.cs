using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.ViewsModels;

namespace WebUI.Controllers
{
	public class AuthController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager = null)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}


		#region REGISTER 
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
		{
			if (!ModelState.IsValid) return View(registerViewModel);

			AppUser appUser = new()
			{
				Fullname = registerViewModel.Fullname,
				UserName = registerViewModel.Username,
				Email = registerViewModel.EmailAddress,
				IsActive = true,
			};
			var identityResult = await _userManager.CreateAsync(appUser, registerViewModel.Password);
			if (!identityResult.Succeeded)
			{
				foreach (var error in identityResult.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
				return View(identityResult);
			}
			return RedirectToAction(nameof(Login));
		}
		#endregion

		#region LOGIN and LOGOUT
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel loginViewModel)
		{
			if (!ModelState.IsValid) return View(loginViewModel);

			var user = await _userManager.FindByEmailAsync(loginViewModel.UsernameOrEmail);
			if (user == null)
			{
				user = await _userManager.FindByNameAsync(loginViewModel.UsernameOrEmail);
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
				return View(loginViewModel);
			}
			if (signInResult.Succeeded)
			{
				ModelState.AddModelError("", "Username/Email or Password incorrect");
				return View(loginViewModel);
			}
			if ((bool)!user.IsActive)
			{
				ModelState.AddModelError("", "not found");
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
	}
}
