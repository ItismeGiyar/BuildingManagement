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
    public class BillitemsController : Controller
    {
        private readonly BuildingDbContext _context;

        public BillitemsController(BuildingDbContext context)
        {
            _context = context;
        }

        // GET: Billitems
        public async Task<IActionResult> Index()
        {
            return View(await _context.ms_billitem.ToListAsync());
        }

        // GET: Billitems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billitem = await _context.ms_billitem
                .FirstOrDefaultAsync(m => m.BItemId == id);
            if (billitem == null)
            {
                return NotFound();
            }

            return View(billitem);
        }

        // GET: Billitems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Billitems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BItemId,BItemDesc,MonthPostFlg,FixChrgFlg,FixChrgAmt,CmpyId,UserId,RevDteTime")] Billitem billitem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billitem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billitem);
        }

        // GET: Billitems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billitem = await _context.ms_billitem.FindAsync(id);
            if (billitem == null)
            {
                return NotFound();
            }
            return View(billitem);
        }

        // POST: Billitems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BItemId,BItemDesc,MonthPostFlg,FixChrgFlg,FixChrgAmt,CmpyId,UserId,RevDteTime")] Billitem billitem)
        {
            if (id != billitem.BItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billitem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillitemExists(billitem.BItemId))
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
            return View(billitem);
        }

        // GET: Billitems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billitem = await _context.ms_billitem
                .FirstOrDefaultAsync(m => m.BItemId == id);
            if (billitem == null)
            {
                return NotFound();
            }

            return View(billitem);
        }

        // POST: Billitems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billitem = await _context.ms_billitem.FindAsync(id);
            if (billitem != null)
            {
                _context.ms_billitem.Remove(billitem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillitemExists(int id)
        {
            return _context.ms_billitem.Any(e => e.BItemId == id);
        }
    }
}
