using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TechProgram2025.Models;

namespace TechProgram2025.Controllers
{
	[Authorize]
	public class ClientsController : Controller
	{
		AppDbContext db;
		public ClientsController(AppDbContext context) 
		{
			db = context;
		}
		// GET: ClientsController
		public async Task<ActionResult> Index()
		{
			return View(await db.Clients.ToListAsync());
		}

		// GET: ClientsController/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id != null)
			{
				Client? client = await db.Clients.FirstOrDefaultAsync(p => p.ClientID == id);
				if (client != null)
				{
					return View(client);
				}
			}
			return NotFound();
		}

		// GET: ClientsController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: ClientsController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(Client client)
		{
			try
			{
				db.Clients.Add(client);
				await db.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: ClientsController/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id != null)
			{
				Client? client = await db.Clients.FirstOrDefaultAsync(p => p.ClientID == id);
				if (client != null)
				{
					return View(client);
				}
			}
			return NotFound();
		}

		// POST: ClientsController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(Client client)
		{
			try
			{
				db.Clients.Update(client);
				await db.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: ClientsController/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id != null) 
			{
				Client? client = await db.Clients.FirstOrDefaultAsync(p => p.ClientID == id);
				if (client != null)
				{
					return View(client);
				}
			}
			return NotFound();
		}

		// POST: ClientsController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(int? id, IFormCollection collection)
		{
			try
			{
				if (id != null)
				{
					Client? client = await db.Clients.FirstOrDefaultAsync(p => p.ClientID == id);
					if (client != null)
					{
						db.Clients.Remove(client);
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
