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

        // GET: BillItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.ms_billitem.ToListAsync());
        }

        // GET: BillItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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

        // GET: BillItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BillItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BItemID,BItemDesc,MonthPostFlg,FixChrgFlg,FixChrgAmt,CmpyId,UserId,RevDteTime")] BillItem billItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billItem);
        }

        // GET: BillItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
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

        // POST: BillItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BItemID,BItemDesc,MonthPostFlg,FixChrgFlg,FixChrgAmt,CmpyId,UserId,RevDteTime")] BillItem billItem)
        {
            if (id != billItem.BItemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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

        // GET: BillItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
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

        // POST: BillItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
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
    }
}
