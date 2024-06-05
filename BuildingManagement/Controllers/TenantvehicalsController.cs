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


        #region // Main Methods  //
        public async Task<IActionResult> Index()
        {
            SetLayoutData();

            var list = await _context.ms_tenantvehical.ToListAsync();

            foreach (var data in list)
            {
                data.Tenant = _context.ms_tenant.Where(t => t.TenantId == data.TenantId).Select(t => t.TenantNme).FirstOrDefault() ?? "";
            }

            return View(list);
        }


       
        public async Task<IActionResult> Details(int? id)
        {
            SetLayoutData();
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

        
        public IActionResult Create()

        {
            SetLayoutData();



            ViewData["TenantList"] = new SelectList(_context.ms_tenant.ToList(), "TenantId", "TenantNme");
            return View();

            
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantId,TenantNme,PlateNo,AllocateNo")] TenantVehical tenantVehical)
        {
            SetLayoutData();
            if (ModelState.IsValid)
            {
                tenantVehical.CmpyId = GetCmpyId(); 
                tenantVehical.UserId = GetUserId(); 
                tenantVehical.RevDteTime = DateTime.Now;

                _context.Add(tenantVehical);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tenantVehical);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            SetLayoutData();
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

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehId,TenantId,TenantNme,PlateNo,AllocateNo")] TenantVehical tenantVehical)
        {
            SetLayoutData();
            if (id != tenantVehical.VehId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tenantVehical.CmpyId = GetCmpyId(); 
                    tenantVehical.UserId = GetUserId(); 
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

        
        public async Task<IActionResult> Delete(int? id)
        {
            SetLayoutData();
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

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SetLayoutData();
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
        #endregion
        #region // Common Methods //
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
        protected void SetLayoutData()
        {
            var userCde = HttpContext.User.Claims.FirstOrDefault()?.Value; // format for to claim usercde

            var userName = _context.ms_user.Where(u => u.UserCde == userCde).Select(u => u.UserNme).FirstOrDefault();

            ViewBag.UserName = userName;
        }
        #endregion
    }
}
