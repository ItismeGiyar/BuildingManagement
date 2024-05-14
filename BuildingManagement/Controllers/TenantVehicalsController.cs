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
    public class TenantVehicalsController : Controller
    {
        private readonly BuildingDbContext _context;

        public TenantVehicalsController(BuildingDbContext context)
        {
            _context = context;
        }

        // GET: TenantVehicals
        public async Task<IActionResult> Index()
        {
            return View(await _context.ms_tenantVehical.ToListAsync());
        }

        // GET: TenantVehicals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantVehical = await _context.ms_tenantVehical
                .FirstOrDefaultAsync(m => m.VehId == id);
            if (tenantVehical == null)
            {
                return NotFound();
            }

            return View(tenantVehical);
        }

        // GET: TenantVehicals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TenantVehicals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehId,TenantId,PlateNo,AllocateNo,CmpyId,UserId,RevDteTime")] TenantVehical tenantVehical)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tenantVehical);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tenantVehical);
        }

        // GET: TenantVehicals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantVehical = await _context.ms_tenantVehical.FindAsync(id);
            if (tenantVehical == null)
            {
                return NotFound();
            }
            return View(tenantVehical);
        }

        // POST: TenantVehicals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehId,TenantId,PlateNo,AllocateNo,CmpyId,UserId,RevDteTime")] TenantVehical tenantVehical)
        {
            if (id != tenantVehical.VehId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tenantVehical);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenantVehicalExists(tenantVehical.VehId))
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
            return View(tenantVehical);
        }

        // GET: TenantVehicals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantVehical = await _context.ms_tenantVehical
                .FirstOrDefaultAsync(m => m.VehId == id);
            if (tenantVehical == null)
            {
                return NotFound();
            }

            return View(tenantVehical);
        }

        // POST: TenantVehicals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tenantVehical = await _context.ms_tenantVehical.FindAsync(id);
            if (tenantVehical != null)
            {
                _context.ms_tenantVehical.Remove(tenantVehical);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TenantVehicalExists(int id)
        {
            return _context.ms_tenantVehical.Any(e => e.VehId == id);
        }
    }
}
