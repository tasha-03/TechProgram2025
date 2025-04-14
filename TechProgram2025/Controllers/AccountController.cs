using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TechProgram2025.Models;

namespace TechProgram2025.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private readonly AppDbContext db;
		public AccountController(AppDbContext context) 
		{
			db = context;
		}

		// GET: Login
		[AllowAnonymous]
		[HttpGet]
		public ActionResult Login()
		{
			return View();
		}

		[AllowAnonymous]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(string email, string password)
		{
			User user = await db.Users.SingleAsync(x => x.Email == email);

			if (user != null)
			{
				if (PasswordHelper.VerifyPassword(password, user.Password))
				{
					await Authenticate(user);
					return RedirectToAction("Index", "Home");
				}
			}
			ModelState.AddModelError("", "Некорректные логин и(или) пароль");
			return View();
		}

		private async Task Authenticate(User user)
		{
			var claims = new List<Claim> {
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
				new Claim(ClaimTypes.Role, user.Role.ToString())
			};
			ClaimsIdentity id = new ClaimsIdentity(
				claims, 
				"ApplicationCookie", 
				ClaimsIdentity.DefaultNameClaimType, 
				ClaimsIdentity.DefaultRoleClaimType
			);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
		}

		public async Task<ActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Account");
		}
	}
}
