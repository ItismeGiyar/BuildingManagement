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
            return View(await _context.pms_billpayment.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
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

            return View(billpayment);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillNo,BillOffsetDesc,PayAmt,CurrCde,CurrRate")] BillPayment billpayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billpayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billpayment);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
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
            if (id != billpayment.BillPId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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

            return View(billpayment);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billpayment = await _context.pms_billpayment.FindAsync(id);
            if (billpayment != null)
            {
                _context.pms_billpayment.Remove(billpayment);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillpaymentExists(int id)
        {
            return _context.pms_billpayment.Any(e => e.BillPId == id);
        }
    }
}
