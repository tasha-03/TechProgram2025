using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TechProgram2025.Models;
using TechProgram2025.Utils;

namespace TechProgram2025.Controllers
{
	[Authorize]
	[CustomAuthorize(Roles.Admin)]
	public class UsersController : Controller
	{
		AppDbContext db;
		public UsersController(AppDbContext context) 
		{
			db = context;
		}
		// GET: UsersController
		public async Task<ActionResult> Index()
		{
			return View(await db.Users.ToListAsync());
		}

		// GET: UsersController/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id != null)
			{
				User? user = await db.Users.FirstOrDefaultAsync(p => p.UserID == id);
				if (user != null)
				{
					return View(user);
				}
			}
			return NotFound();
		}

		// GET: UsersController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: UsersController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(User user)
		{
			try
			{
				string hash = PasswordHelper.HashPassword(user.Password);
				user.Password = hash;
				db.Users.Add(user);
				await db.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: UsersController/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id != null)
			{
				User? user = await db.Users.FirstOrDefaultAsync(p => p.UserID == id);
				if (user != null)
				{
					return View(user);
				}
			}
			return NotFound();
		}

		// POST: UsersController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(int id, User userInput)
		{
			try
			{
				User? user = await db.Users.FirstOrDefaultAsync(p => p.UserID == id);
				if (user != null)
				{
					user.FullName = userInput.FullName;
					user.Email = userInput.Email;
					user.PhoneNumber = userInput.PhoneNumber;
					user.Role = userInput.Role;
					string password = userInput.Password;
					if (password != null && password != "")
					{
						string hash = PasswordHelper.HashPassword(password);
						user.Password = hash;
					}

					await db.SaveChangesAsync();
					return RedirectToAction(nameof(Index));
				}
				return NotFound();
			}
			catch
			{
				return View();
			}
		}

		// GET: UsersController/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id != null)
			{
				User? user = await db.Users.FirstOrDefaultAsync(p => p.UserID == id);
				if (user != null)
				{
					return View(user);
				}
			}
			return NotFound();
		}

		// POST: UsersController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(int? id, IFormCollection collection)
		{
			try
			{
				if (id != null)
				{
					User? user = await db.Users.FirstOrDefaultAsync(p => p.UserID == id);
					if (user != null)
					{
						db.Users.Remove(user);
						await db.SaveChangesAsync();
						return RedirectToAction(nameof(Index));
					}
				}
				return NotFound();
			}
			catch
			{
				return View();
			}
		}
	}
}
