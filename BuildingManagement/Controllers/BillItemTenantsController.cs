using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BuildingManagement.Data;
using BuildingManagement.Models;
using Microsoft.AspNetCore.Authorization;

namespace BuildingManagement.Controllers
{
    [Authorize]
    public class BillItemTenantsController : Controller
    {
        private readonly BuildingDbContext _context;

        public BillItemTenantsController(BuildingDbContext context)
        {
            _context = context;
        }

        // GET: BillItemTenants
        public async Task<IActionResult> Index()
        {
            return View(await _context.ms_billItemTenant.ToListAsync());
        }

        // GET: BillItemTenants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billItemTenant = await _context.ms_billItemTenant
                .FirstOrDefaultAsync(m => m.BtitemId == id);
            if (billItemTenant == null)
            {
                return NotFound();
            }

            return View(billItemTenant);
        }

        // GET: BillItemTenants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BillItemTenants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BtitemId,BItemId,TenantId,SubPlan,SubDte,ActiveFlg,LastReadingUnit,Amount,CmpyId,UserId,RevDteTime")] BillItemTenant billItemTenant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billItemTenant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billItemTenant);
        }

        // GET: BillItemTenants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billItemTenant = await _context.ms_billItemTenant.FindAsync(id);
            if (billItemTenant == null)
            {
                return NotFound();
            }
            return View(billItemTenant);
        }

        // POST: BillItemTenants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BtitemId,BItemId,TenantId,SubPlan,SubDte,ActiveFlg,LastReadingUnit,Amount,CmpyId,UserId,RevDteTime")] BillItemTenant billItemTenant)
        {
            if (id != billItemTenant.BtitemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billItemTenant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillItemTenantExists(billItemTenant.BtitemId))
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
            return View(billItemTenant);
        }

        // GET: BillItemTenants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billItemTenant = await _context.ms_billItemTenant
                .FirstOrDefaultAsync(m => m.BtitemId == id);
            if (billItemTenant == null)
            {
                return NotFound();
            }

            return View(billItemTenant);
        }

        // POST: BillItemTenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billItemTenant = await _context.ms_billItemTenant.FindAsync(id);
            if (billItemTenant != null)
            {
                _context.ms_billItemTenant.Remove(billItemTenant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillItemTenantExists(int id)
        {
            return _context.ms_billItemTenant.Any(e => e.BtitemId == id);
        }
    }
}
