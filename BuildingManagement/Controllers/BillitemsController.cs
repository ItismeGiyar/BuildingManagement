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
    public class BillItemsController : Controller

    {
        private readonly BuildingDbContext _context;

        public BillItemsController(BuildingDbContext context)
        {
            _context = context;
        }


        #region // Main methods //

        public async Task<IActionResult> Index()
        {

            SetLayOutData();
            return View(await _context.ms_billitem.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var billItem = await _context.ms_billitem
                .FirstOrDefaultAsync(m => m.BItemID == id);
            if (billItem == null)
            {
                return NotFound();
            }

            billItem.Company = _context.ms_company.Where(c => c.CmpyId == billItem.CmpyId).Select(c => c.CmpyNme).FirstOrDefault() ?? "";
            billItem.User = _context.ms_user.Where(u => u.UserId == billItem.UserId).Select(u => u.UserNme).FirstOrDefault() ?? "";

            return View(billItem);
        }

        public IActionResult Create()
        {
            SetLayOutData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BItemDesc,MonthPostFlg,FixChrgFlg,FixChrgAmt")] BillItem billItem)
        {
            SetLayOutData();
            if (ModelState.IsValid)
            {
                billItem.CmpyId = GetCmpyId();
                billItem.UserId = GetUserId();
                billItem.RevDteTime = DateTime.Now;
                _context.Add(billItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billItem);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var billItem = await _context.ms_billitem.FindAsync(id);
            if (billItem == null)
            {
                return NotFound();
            }
            return View(billItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BItemID,BItemDesc,MonthPostFlg,FixChrgFlg,FixChrgAmt")] BillItem billItem)
        {
            SetLayOutData();
            if (id != billItem.BItemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    billItem.CmpyId = GetCmpyId();
                    billItem.UserId = GetUserId();
                    billItem.RevDteTime = DateTime.Now;
                    _context.Update(billItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillItemExists(billItem.BItemID))
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
            return View(billItem);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var billItem = await _context.ms_billitem
                .FirstOrDefaultAsync(m => m.BItemID == id);
            if (billItem == null)
            {
                return NotFound();
            }

            return View(billItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SetLayOutData();
            var billItem = await _context.ms_billitem.FindAsync(id);
            if (billItem != null)
            {
                _context.ms_billitem.Remove(billItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillItemExists(int id)
        {
            return _context.ms_billitem.Any(e => e.BItemID == id);
        }

        #endregion


        #region // Common methods //

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
        protected void SetLayOutData()
        {
            var userCde = HttpContext.User.Claims.FirstOrDefault()?.Value; // format for to claim usercde

            var userName = _context.ms_user.Where(u => u.UserCde == userCde).Select(u => u.UserNme).FirstOrDefault();

            ViewBag.UserName = userName;

        }
        #endregion
    }
}





