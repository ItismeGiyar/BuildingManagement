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
        protected short GetUserId()
        { 
            var userCde = HttpContext.User.Claims.FirstOrDefault()?.Value;
            var userId = (short)_context.ms_user
                .Where(u => u.UserCde == userCde)
                .Select(u => u.UserId)
                .FirstOrDefault();

            return userId;
        }

        protected short GetCmpyId()
        {
            var cmpyId = _context.ms_user
                .Where(u => u.UserId == GetUserId())
                .Select(u => u.CmpyId)
                .FirstOrDefault();

            return cmpyId;
        }


        // GET: TenantVehicals
        public async Task<IActionResult> Index()
        {

            var list = await _context.ms_tenantvehical.ToListAsync();

            foreach (var data in list)
            {
                data.Tenant = _context.ms_tenant.Where(t => t.TenantId == data.TenantId).Select(t => t.TenantNme).FirstOrDefault() ?? "";
            }

            return View(list);
        }


        // GET: TenantVehicals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var tenantVehical = await _context.ms_tenantvehical
                .FirstOrDefaultAsync(m => m.VehId == id);
            if (tenantVehical == null)
            {
                return NotFound();
            }
            tenantVehical.Tenant =
               _context.ms_tenant
               .Where(c => c.TenantId == tenantVehical.TenantId)
               .Select(c => c.TenantNme)
               .FirstOrDefault() ?? "";


            tenantVehical.Company =
                _context.ms_company
                .Where(c => c.CmpyId == tenantVehical.CmpyId)
                .Select(c => c.CmpyNme)
                .FirstOrDefault() ?? "";

            tenantVehical.User =
                _context.ms_user
                .Where(u => u.UserId == tenantVehical.UserId)
                .Select(u => u.UserNme)
                .FirstOrDefault() ?? "";
            return View(tenantVehical);
        }

        // GET: TenantVehicals/Create
        public IActionResult Create()

        {
           

            ViewData["TenantList"] = new SelectList(_context.ms_tenant.ToList(), "TenantId", "TenantNme");
            return View();

            
        }

        // POST: TenantVehicals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantId,PlateNo,AllocateNo")] TenantVehical tenantVehical)
        {
            if (ModelState.IsValid)
            {
                tenantVehical.CmpyId = GetCmpyId(); //default
                tenantVehical.UserId = GetUserId(); //default
                tenantVehical.RevDteTime = DateTime.Now;

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

            var tenantVehical = await _context.ms_tenantvehical.FindAsync(id);
            if (tenantVehical == null)
            {
                return NotFound();
            }
            ViewData["TenantList"] = new SelectList(_context.ms_tenant.ToList(), "TenantId", "TenantNme");

            return View(tenantVehical);
        }

        // POST: TenantVehicals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehId,TenantId,PlateNo,AllocateNo")] TenantVehical tenantVehical)
        {
            if (id != tenantVehical.VehId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tenantVehical.CmpyId = GetCmpyId(); //default
                    tenantVehical.UserId = GetUserId(); //default
                    tenantVehical.RevDteTime = DateTime.Now;


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

            var tenantVehical = await _context.ms_tenantvehical
                .FirstOrDefaultAsync(m => m.VehId == id);
            if (tenantVehical == null)
            {
                return NotFound();
            }

            tenantVehical.Tenant =
               _context.ms_tenant
               .Where(c => c.TenantId == tenantVehical.TenantId)
               .Select(c => c.TenantNme)
               .FirstOrDefault() ?? "";


           
            return View(tenantVehical);

            
        }

        // POST: TenantVehicals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tenantVehical = await _context.ms_tenantvehical.FindAsync(id);
            if (tenantVehical != null)
            {
                _context.ms_tenantvehical.Remove(tenantVehical);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TenantVehicalExists(int id)
        {
            return _context.ms_tenantvehical.Any(e => e.VehId == id);
        }
    }
}
