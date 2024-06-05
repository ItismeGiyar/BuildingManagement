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
    public class TenantsController : Controller
    {
        private readonly BuildingDbContext _context;

        public TenantsController(BuildingDbContext context)
        {
            _context = context;
        }



        #region // Main Methods //
        public async Task<IActionResult> Index()
        {
            SetLayoutData();
            return View(await _context.ms_tenant.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            SetLayoutData();
            if (id == null)
            {
                return NotFound();
            }

           

            var tenant = await _context.ms_tenant
                .FirstOrDefaultAsync(m => m.TenantId == id);
            if (tenant == null)
            {
                return NotFound();
            }
            tenant.Company =
              _context.ms_company
              .Where(c => c.CmpyId == tenant.CmpyId)
              .Select(c => c.CmpyNme)
              .FirstOrDefault() ?? "";

            tenant.User =
                _context.ms_user
                .Where(u => u.UserId == tenant.UserId)
                .Select(u => u.UserNme)
                .FirstOrDefault() ?? "";

            return View(tenant);
        }

        
        public IActionResult Create()
        {
            SetLayoutData();
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantNme,Occupany,IdNo,Gender,Phone1,Phone2,LocalFlg,PermentAddr")] Tenant tenant)
        {
            SetLayoutData();


            if (ModelState.IsValid)
            {
                tenant.CmpyId = GetCmpyId();
                tenant.UserId = GetUserId();
                tenant.RevDteTime = DateTime.Now;
                _context.Add(tenant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tenant);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            SetLayoutData();
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.ms_tenant.FindAsync(id);
            if (tenant == null)
            {
                return NotFound();
            }
            return View(tenant);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TenantId,TenantNme,Occupany,IdNo,Gender,Phone1,Phone2,LocalFlg,PermentAddr")] Tenant tenant)
        {

            SetLayoutData();
            if (id != tenant.TenantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tenant.CmpyId = GetCmpyId();
                    tenant.UserId = GetUserId();
                    tenant.RevDteTime=DateTime.Now;
                    _context.Update(tenant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenantExists(tenant.TenantId))
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
            return View(tenant);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            SetLayoutData();
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.ms_tenant
                .FirstOrDefaultAsync(m => m.TenantId == id);
            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SetLayoutData();
            var tenant = await _context.ms_tenant.FindAsync(id);
            if (tenant != null)
            {
                _context.ms_tenant.Remove(tenant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TenantExists(int id)
        {
            return _context.ms_tenant.Any(e => e.TenantId == id);
        }
        #endregion

        #region //Common Methods //
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
