using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TechProgram2025.Models;

namespace TechProgram2025.Controllers
{
	[Authorize]
	public class InsuranceController : Controller
	{
		AppDbContext db;
		public InsuranceController(AppDbContext context)
		{
			db = context;
		}

		// GET: InsuranceController
		public async Task<ActionResult> Index()
		{
			List<InsuranceCategory> categories = await db.InsuranceCategories.ToListAsync();
			ViewBag.Categories = categories;
			return View(await db.InsuranceVariants.Include(variant => variant.Category).ToListAsync());
		}

		// GET: InsuranceController/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id != null)
			{
				InsuranceVariant? insuranceVariant = await db.InsuranceVariants.FirstOrDefaultAsync(p => p.InsuranceVariantID == id);
				if (insuranceVariant != null)
				{
					return View(insuranceVariant);
				}
			}
			return NotFound();
		}

		// GET: InsuranceController/Create
		public ActionResult Create()
		{
			SelectList categories = new SelectList(db.InsuranceCategories, "InsuranceCategoryID", "Name");
			ViewBag.Categories = categories;
			return View();
		}

		// POST: InsuranceController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(InsuranceVariant insuranceVariant)
		{
			try
			{
				db.InsuranceVariants.Add(insuranceVariant);
				await db.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: InsuranceController/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			SelectList categories = new SelectList(db.InsuranceCategories, "InsuranceCategoryID", "Name");
			ViewBag.Categories = categories;
			if (id != null)
			{
				InsuranceVariant? insuranceVariant = await db.InsuranceVariants.FirstOrDefaultAsync(p => p.InsuranceVariantID == id);
				if (insuranceVariant != null)
				{
					return View(insuranceVariant);
				}
			}
			return NotFound();
		}

		// POST: InsuranceController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(InsuranceVariant insuranceVariant)
		{
			try
			{
				Console.WriteLine(insuranceVariant.Price);
				db.InsuranceVariants.Update(insuranceVariant);
				await db.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: InsuranceController/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id != null)
			{
				InsuranceVariant? insuranceVariant = await db.InsuranceVariants.FirstOrDefaultAsync(p => p.InsuranceVariantID == id);
				if (insuranceVariant != null)
				{
					return View(insuranceVariant);
				}
			}
			return NotFound();
		}

		// POST: InsuranceController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(int? id, IFormCollection collection)
		{
			try
			{
				if (id != null)
				{
					InsuranceVariant? insuranceVariant = await db.InsuranceVariants.FirstOrDefaultAsync(p => p.InsuranceVariantID == id);
					if (insuranceVariant != null)
					{
						db.InsuranceVariants.Remove(insuranceVariant);
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
