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
    public class ComplaintcatgsController : Controller
    {
        private readonly BuildingDbContext _context;

        public ComplaintcatgsController(BuildingDbContext context)
        {
            _context = context;
        }

        // GET: Complaintcatgs
        public async Task<IActionResult> Index()
        {
            return View(await _context.ms_complaintcatg.ToListAsync());
        }

        // GET: Complaintcatgs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintcatg = await _context.ms_complaintcatg
                .FirstOrDefaultAsync(m => m.CplCatgId == id);
            if (complaintcatg == null)
            {
                return NotFound();
            }

            return View(complaintcatg);
        }

        // GET: Complaintcatgs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Complaintcatgs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CplCatgId,CplCatcDe,CmpyId,UserId,RevDteTime")] Complaintcatg complaintcatg)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complaintcatg);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(complaintcatg);
        }

        // GET: Complaintcatgs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintcatg = await _context.ms_complaintcatg.FindAsync(id);
            if (complaintcatg == null)
            {
                return NotFound();
            }
            return View(complaintcatg);
        }

        // POST: Complaintcatgs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CplCatgId,CplCatcDe,CmpyId,UserId,RevDteTime")] Complaintcatg complaintcatg)
        {
            if (id != complaintcatg.CplCatgId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaintcatg);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintcatgExists(complaintcatg.CplCatgId))
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
            return View(complaintcatg);
        }

        // GET: Complaintcatgs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintcatg = await _context.ms_complaintcatg
                .FirstOrDefaultAsync(m => m.CplCatgId == id);
            if (complaintcatg == null)
            {
                return NotFound();
            }

            return View(complaintcatg);
        }

        // POST: Complaintcatgs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complaintcatg = await _context.ms_complaintcatg.FindAsync(id);
            if (complaintcatg != null)
            {
                _context.ms_complaintcatg.Remove(complaintcatg);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintcatgExists(int id)
        {
            return _context.ms_complaintcatg.Any(e => e.CplCatgId == id);
        }
    }
}
