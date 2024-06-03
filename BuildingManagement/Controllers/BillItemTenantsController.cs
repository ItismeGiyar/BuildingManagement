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


        // GET: BillItemTenants
        public async Task<IActionResult> Index()
        {
            var list = await _context.ms_billitemtenant.ToListAsync();

            foreach (var data in list)
            {
                data.Billitem = _context.ms_billitem.Where(rt => rt.BItemID == data.BItemID).Select(rt => rt.BItemDesc).FirstOrDefault() ?? "";
                data.Tenant = _context.ms_tenant.Where(t => t.TenantId == data.TenantId).Select(t => t.TenantNme).FirstOrDefault() ?? "";
            }
            return View(list);
        }

        // GET: BillItemTenants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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
               .Where(c => c.BItemID == billItemTenant.BItemID)
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

        // GET: BillItemTenants/Create
        public IActionResult Create()
        {
            ViewData["TenantList"] = new SelectList(_context.ms_tenant.ToList(), "TenantId", "TenantNme");
            ViewData["BillitemList"] = new SelectList(_context.ms_billitem.ToList(), "BItemID", "BItemDesc");
            return View();
        }

        // POST: BillItemTenants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BItemID,TenantId,SubPlan,SubDte,ActiveFlg,LastReadingUnit,Amount")] BillItemTenant billItemTenant)
        {
            if (ModelState.IsValid)
            {
                billItemTenant.CmpyId = GetCmpyId(); //default
                billItemTenant.UserId = GetUserId(); //default
                billItemTenant.RevDteTime = DateTime.Now;
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

            var billItemTenant = await _context.ms_billitemtenant.FindAsync(id);
            if (billItemTenant == null)
            {
                return NotFound();
            }
            ViewData["TenantList"] = new SelectList(_context.ms_tenant.ToList(), "TenantId", "TenantNme");
            ViewData["BillitemList"] = new SelectList(_context.ms_billitem.ToList(), "BItemID", "BItemDesc");
            return View(billItemTenant);
        }

        // POST: BillItemTenants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BtitemId,BItemID,TenantId,SubPlan,SubDte,ActiveFlg,LastReadingUnit,Amount")] BillItemTenant billItemTenant)
        {
            if (id != billItemTenant.BtitemId)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    billItemTenant.CmpyId = GetCmpyId(); //default
                    billItemTenant.UserId = GetUserId(); //default
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

        // GET: BillItemTenants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
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
              .Where(c => c.BItemID == billItemTenant.BItemID)
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

        // POST: BillItemTenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
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
    }
}
