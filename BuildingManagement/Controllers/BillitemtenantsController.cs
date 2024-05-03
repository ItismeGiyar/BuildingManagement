using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BuildingManagement.Data;
using BuildingManagement.Models;

namespace BuildingManagement.Controllers
{
    public class BillitemtenantsController : Controller
    {
        private readonly BuildingDbContext _context;

        public BillitemtenantsController(BuildingDbContext context)
        {
            _context = context;
        }

        // GET: Billitemtenants
        public async Task<IActionResult> Index()
        {
            return View(await _context.ms_billitemtenant.ToListAsync());
        }

        // GET: Billitemtenants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billitemtenant = await _context.ms_billitemtenant
                .FirstOrDefaultAsync(m => m.BtItemId == id);
            if (billitemtenant == null)
            {
                return NotFound();
            }

            return View(billitemtenant);
        }

        // GET: Billitemtenants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Billitemtenants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BtItemId,BItemId,TenantId,SubPlan,SubDte,ActiveFlg,LastReadingUnit,Amount,CmpyId,UserId,RevDteTime")] Billitemtenant billitemtenant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billitemtenant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billitemtenant);
        }

        // GET: Billitemtenants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billitemtenant = await _context.ms_billitemtenant.FindAsync(id);
            if (billitemtenant == null)
            {
                return NotFound();
            }
            return View(billitemtenant);
        }

        // POST: Billitemtenants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BtItemId,BItemId,TenantId,SubPlan,SubDte,ActiveFlg,LastReadingUnit,Amount,CmpyId,UserId,RevDteTime")] Billitemtenant billitemtenant)
        {
            if (id != billitemtenant.BtItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billitemtenant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillitemtenantExists(billitemtenant.BtItemId))
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
            return View(billitemtenant);
        }

        // GET: Billitemtenants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billitemtenant = await _context.ms_billitemtenant
                .FirstOrDefaultAsync(m => m.BtItemId == id);
            if (billitemtenant == null)
            {
                return NotFound();
            }

            return View(billitemtenant);
        }

        // POST: Billitemtenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billitemtenant = await _context.ms_billitemtenant.FindAsync(id);
            if (billitemtenant != null)
            {
                _context.ms_billitemtenant.Remove(billitemtenant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillitemtenantExists(int id)
        {
            return _context.ms_billitemtenant.Any(e => e.BtItemId == id);
        }
    }
}
