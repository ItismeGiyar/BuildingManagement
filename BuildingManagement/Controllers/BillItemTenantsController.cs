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
        #region // Main methods //
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


       
        public async Task<IActionResult> Index()
        {
            SetLayOutData();
            var list = await _context.ms_billitemtenant.ToListAsync();

            foreach (var data in list)
            {
                data.Billitem = _context.ms_billitem.Where(rt => rt.BItemID == data.BItemId).Select(rt => rt.BItemDesc).FirstOrDefault() ?? "";
                data.Tenant = _context.ms_tenant.Where(t => t.TenantId == data.TenantId).Select(t => t.TenantNme).FirstOrDefault() ?? "";
            }
            return View(list);
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var billItemTenant = await _context.ms_billitemtenant
                .FirstOrDefaultAsync(m => m.BtitemId == id);
            if (billItemTenant == null)
            {
                return NotFound();
            }

            billItemTenant.Billitem =
               _context.ms_billitem
               .Where(c => c.BItemID == billItemTenant.BItemId)
               .Select(c => c.BItemDesc)
               .FirstOrDefault() ?? "";

            billItemTenant.Tenant=
                _context.ms_tenant
                .Where(u => u.TenantId == billItemTenant.TenantId)
                .Select(u => u.TenantNme)
                .FirstOrDefault() ?? "";
            billItemTenant.Company =
                _context.ms_company 
                .Where(c => c.CmpyId == billItemTenant.CmpyId)
                .Select(c => c.CmpyNme)
                .FirstOrDefault() ?? "";

            billItemTenant.User =
                _context.ms_user
                .Where(u => u.UserId == billItemTenant.UserId)
                .Select(u => u.UserNme)
                .FirstOrDefault() ?? "";


            return View(billItemTenant);
        }

        
        public IActionResult Create()
        {
            SetLayOutData();

            ViewData["TenantList"] = new SelectList(_context.ms_tenant.ToList(), "TenantId", "TenantNme");
            ViewData["BillitemList"] = new SelectList(_context.ms_billitem.ToList(), "BItemID", "BItemDesc");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BItemId,TenantId,SubPlan,SubDte,ActiveFlg,LastReadingUnit,Amount")] BillItemTenant billItemTenant)
        {
            SetLayOutData();
            if (ModelState.IsValid)
            {
                billItemTenant.CmpyId = GetCmpyId(); 
                billItemTenant.UserId = GetUserId(); 
                billItemTenant.RevDteTime = DateTime.Now;
                _context.Add(billItemTenant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billItemTenant);
        }

     
        public async Task<IActionResult> Edit(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var billItemTenant = await _context.ms_billitemtenant.FindAsync(id);
            if (billItemTenant == null)
            {
                return NotFound();
            }
            ViewData["TenantList"] = new SelectList(_context.ms_tenant.ToList(), "TenantId", "TenantNme");
            ViewData["BillitemList"] = new SelectList(_context.ms_billitem.ToList(), "BItemID", "BItemDesc");
            return View(billItemTenant);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BtitemId,BItemId,TenantId,SubPlan,SubDte,ActiveFlg,LastReadingUnit,Amount")] BillItemTenant billItemTenant)
        {
            SetLayOutData();
            if (id != billItemTenant.BtitemId)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    billItemTenant.CmpyId = GetCmpyId(); 
                    billItemTenant.UserId = GetUserId(); 
                    billItemTenant.RevDteTime = DateTime.Now;
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

     
        public async Task<IActionResult> Delete(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var billItemTenant = await _context.ms_billitemtenant
                .FirstOrDefaultAsync(m => m.BtitemId == id);
            if (billItemTenant == null)
            {
                return NotFound();
            }
            billItemTenant.Billitem =
              _context.ms_billitem
              .Where(c => c.BItemID == billItemTenant.BItemId)
              .Select(c => c.BItemDesc)
              .FirstOrDefault() ?? "";

            billItemTenant.Tenant =
                _context.ms_tenant
                .Where(u => u.TenantId == billItemTenant.TenantId)
                .Select(u => u.TenantNme)
                .FirstOrDefault() ?? "";
            billItemTenant.Company =
                _context.ms_company
                .Where(c => c.CmpyId == billItemTenant.CmpyId)
                .Select(c => c.CmpyNme)
                .FirstOrDefault() ?? "";

            billItemTenant.User =
                _context.ms_user
                .Where(u => u.UserId == billItemTenant.UserId)
                .Select(u => u.UserNme)
                .FirstOrDefault() ?? "";


            return View(billItemTenant);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SetLayOutData();
            var billItemTenant = await _context.ms_billitemtenant.FindAsync(id);
            if (billItemTenant != null)
            {
                _context.ms_billitemtenant.Remove(billItemTenant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillItemTenantExists(int id)
        {
            return _context.ms_billitemtenant.Any(e => e.BtitemId == id);
        }
        #endregion


        #region // Common methods //
        protected void SetLayOutData()
        {
            var userCde = HttpContext.User.Claims.FirstOrDefault()?.Value; // format for to claim usercde

            var userName = _context.ms_user.Where(u => u.UserCde == userCde).Select(u => u.UserNme).FirstOrDefault();

            ViewBag.UserName = userName;

        }
        #endregion
    }
}
