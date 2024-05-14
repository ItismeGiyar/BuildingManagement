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
    public class MenugpsController : Controller
    {
        private readonly BuildingDbContext _context;

        public MenugpsController(BuildingDbContext context)
        {
            _context = context;
        }

        // GET: Menugps
        public async Task<IActionResult> Index()
        {
            return View(await _context.ms_menugp.ToListAsync());
        }

        // GET: Menugps/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menugp = await _context.ms_menugp
                .FirstOrDefaultAsync(m => m.MnugrpId == id);
            if (menugp == null)
            {
                return NotFound();
            }

            return View(menugp);
        }

        // GET: Menugps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Menugps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MnugrpId,MnugrpNme,RevDteTime")] Menugp menugp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menugp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menugp);
        }

        // GET: Menugps/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menugp = await _context.ms_menugp.FindAsync(id);
            if (menugp == null)
            {
                return NotFound();
            }
            return View(menugp);
        }

        // POST: Menugps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("MnugrpId,MnugrpNme,RevDteTime")] Menugp menugp)
        {
            if (id != menugp.MnugrpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menugp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenugpExists(menugp.MnugrpId))
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
            return View(menugp);
        }

        // GET: Menugps/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menugp = await _context.ms_menugp
                .FirstOrDefaultAsync(m => m.MnugrpId == id);
            if (menugp == null)
            {
                return NotFound();
            }

            return View(menugp);
        }

        // POST: Menugps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var menugp = await _context.ms_menugp.FindAsync(id);
            if (menugp != null)
            {
                _context.ms_menugp.Remove(menugp);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        {
            return _context.ms_menugp.Any(e => e.MnugrpId == id);
        }
    }
}
