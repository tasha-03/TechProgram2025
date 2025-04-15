using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TechProgram2025.Models;

namespace TechProgram2025.Controllers
{
	public class InsuranceCategoryController : Controller
	{
		AppDbContext db;

		public InsuranceCategoryController(AppDbContext context)
		{
			this.db = context;
		}

		// GET: InsuranceCategoryController
		public async Task<ActionResult> Index()
		{
			var categories = await db.InsuranceCategories.ToListAsync();
			return View(categories);
		}

		// GET: InsuranceCategoryController/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id != null)
			{
				InsuranceCategory? category = 
					await db.InsuranceCategories.Include(c => c.ParentCategory)
					.FirstOrDefaultAsync(p => p.InsuranceCategoryID == id);
				if (category != null)
				{
					return View(category);
				}
			}
			return NotFound();
		}

		// GET: InsuranceCategoryController/Create
		public ActionResult Create()
		{
			ViewData["ParentCategoryID"] = new SelectList(db.InsuranceCategories, "InsuranceCategoryID", "Name");
			return View();
		}

		// POST: InsuranceCategoryController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(InsuranceCategory category)
		{
			try
			{
				db.InsuranceCategories.Add(category);
				await db.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: InsuranceCategoryController/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id != null)
			{
				ViewData["ParentCategoryID"] = new SelectList(db.InsuranceCategories, "InsuranceCategoryID", "Name");
				InsuranceCategory? category = await db.InsuranceCategories.FirstOrDefaultAsync(p => p.InsuranceCategoryID == id);
				if (category != null)
				{
					return View(category);
				}
			}
			return NotFound();
		}

		// POST: InsuranceCategoryController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(InsuranceCategory category)
		{
			try
			{
				db.InsuranceCategories.Update(category);
				await db.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: InsuranceCategoryController/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id != null)
			{
				InsuranceCategory? category = await db.InsuranceCategories.FirstOrDefaultAsync(p => p.InsuranceCategoryID == id);
				if (category != null)
				{
					return View(category);
				}
			}
			return NotFound();
		}

		// POST: InsuranceCategoryController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(int? id, IFormCollection collection)
		{
			try
			{
				if (id != null)
				{
					InsuranceCategory? category = await db.InsuranceCategories.FirstOrDefaultAsync(p => p.InsuranceCategoryID == id);
					if (category != null)
					{
						db.InsuranceCategories.Remove(category);
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
