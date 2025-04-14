using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TechProgram2025.Models;

namespace TechProgram2025.Controllers
{
	[Authorize]
	public class ContractsController : Controller
    {
        private readonly AppDbContext db;

        public ContractsController(AppDbContext context)
        {
            db = context;
        }

        // GET: Contracts
        public async Task<ActionResult> Index()
        {
            var contracts = await db.Contracts
                .Include(c => c.Client)
                .Include(c => c.Agent)
                .Include(c => c.InsuranceVariants)
            .ToListAsync();

            return View(contracts);
        }

        // GET: Contracts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await db.Contracts
                .Include(c => c.Client).Include(c => c.InsuranceVariants)
                .FirstOrDefaultAsync(m => m.ContractID == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // GET: Contracts/Create
        public async Task<ActionResult> Create()
        {
            ViewData["ClientID"] = new SelectList(db.Clients, "ClientID", "ClientName");
			ViewBag.Insurances = await db.InsuranceVariants.ToListAsync();
            //ViewBag.MultiSelectBag = new MultiSelectList(insurances, "InsuranceVariantID", "Name");
            return View();
        }

        // POST: Contracts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Contract contract, int[] variantIds)
        {
            var email = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            User dbagent = await db.Users.SingleAsync(u => u.Email == email);
            int agentID = dbagent.UserID;

			ViewBag.Insurances = await db.InsuranceVariants.ToListAsync();
            foreach (int id in variantIds)
            {
                InsuranceVariant variant = await db.InsuranceVariants.SingleAsync(c => c.InsuranceVariantID == id);
                contract.InsuranceVariants.Add(variant);
			}

            User agent = await db.Users.SingleAsync(u => u.UserID == agentID);
            Client client = await db.Clients.SingleAsync(c => c.ClientID == contract.ClientID);

            if (agent != null)
            {
                contract.AgentUserID = agentID;
                contract.Agent = agent;
            }

            if (client != null)
            {
                contract.Client = client;
            }

            try
            {
                db.Contracts.Add(contract);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exc)
            {
                ViewData["Error"] = "error";
			    ViewData["Error"] = ModelState.IsValid;

			    return View();
            }
            
        }

        // GET: Contracts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await db.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            ViewData["ClientID"] = new SelectList(db.Clients, "ClientID", "ClientID", contract.ClientID);
            return View(contract);
        }

        // POST: Contracts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Contract contract)
        {
            if (id != contract.ContractID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(contract);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractExists(contract.ContractID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientID"] = new SelectList(db.Clients, "ClientID", "ClientID", contract.ClientID);
            return View(contract);
        }

        // GET: Contracts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await db.Contracts
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.ContractID == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var contract = await db.Contracts.FindAsync(id);
            if (contract != null)
            {
                db.Contracts.Remove(contract);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractExists(int id)
        {
            return db.Contracts.Any(e => e.ContractID == id);
        }
    }
}
