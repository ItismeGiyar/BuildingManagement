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
    public class BillpaymentsController : Controller
    {
        private readonly BuildingDbContext _context;

        public BillpaymentsController(BuildingDbContext context)
        {
            _context = context;
        }
        #region // Main Methods //
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
            var list = await _context.pms_billpayment.ToListAsync();

          
            return View(list);
        }



        public async Task<IActionResult> Details(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var billpayment = await _context.pms_billpayment
                .FirstOrDefaultAsync(m => m.BillPId == id);
            if (billpayment == null)
            {
                return NotFound();
            }
            billpayment.Company =
             _context.ms_company
             .Where(c => c.CmpyId == billpayment.CmpyId)
             .Select(c => c.CmpyNme)
             .FirstOrDefault() ?? "";

            billpayment.User =
                _context.ms_user
                .Where(u => u.UserId == billpayment.UserId)
                .Select(u => u.UserNme)
                .FirstOrDefault() ?? "";
          
            return View(billpayment);
        }

        
        public IActionResult Create()
        {
            SetLayOutData();
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillNo,BillOffsetDesc,PayAmt,CurrCde,CurrRate")] BillPayment billpayment)
        {
            SetLayOutData();
            if (ModelState.IsValid)
            {
                billpayment.CmpyId = GetCmpyId();
                billpayment.UserId = GetUserId();
                billpayment.RevDteTime = DateTime.Now;
                _context.Add(billpayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billpayment);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var billpayment = await _context.pms_billpayment.FindAsync(id);
            if (billpayment == null)
            {
                return NotFound();
            }
            return View(billpayment);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BillPId,BillNo,BillOffsetDesc,PayAmt,CurrCde,CurrRate")] BillPayment billpayment)
        {
            SetLayOutData();
            if (id != billpayment.BillPId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    billpayment.CmpyId = GetCmpyId();
                    billpayment.UserId = GetUserId();
                    billpayment.RevDteTime = DateTime.Now;


                    _context.Update(billpayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillpaymentExists(billpayment.BillPId))
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
            return View(billpayment);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var billpayment = await _context.pms_billpayment
                .FirstOrDefaultAsync(m => m.BillPId == id);
            if (billpayment == null)
            {
                return NotFound();
            }
            billpayment.Company =
             _context.ms_company
             .Where(c => c.CmpyId == billpayment.CmpyId)
             .Select(c => c.CmpyNme)
             .FirstOrDefault() ?? "";

            billpayment.User =
                _context.ms_user
                .Where(u => u.UserId == billpayment.UserId)
                .Select(u => u.UserNme)
                .FirstOrDefault() ?? "";
            return View(billpayment);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SetLayOutData();
            var billpayment = await _context.pms_billpayment.FindAsync(id);
            if (billpayment != null)
            {
                _context.pms_billpayment.Remove(billpayment);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        private bool BillpaymentExists(int id)
        {
            return _context.pms_billpayment.Any(e => e.BillPId == id);
        }
        #region //Common Methods //
        protected void SetLayOutData()
        {
            var userCde = HttpContext.User.Claims.FirstOrDefault()?.Value; // format for to claim usercde

            var userName = _context.ms_user.Where(u => u.UserCde == userCde).Select(u => u.UserNme).FirstOrDefault();

            ViewBag.UserName = userName;

        }
        #endregion
    }
}
