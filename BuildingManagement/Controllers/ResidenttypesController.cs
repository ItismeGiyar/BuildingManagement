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
    public class ResidenttypesController : Controller
    {
        private readonly BuildingDbContext _context;

        public ResidenttypesController(BuildingDbContext context)
        {
            _context = context;
        }

        // GET: Residenttypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ms_residenttype.ToListAsync());
        }

        // GET: Residenttypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residenttype = await _context.ms_residenttype
                .FirstOrDefaultAsync(m => m.ResitypId == id);
            if (residenttype == null)
            {
                return NotFound();
            }

            return View(residenttype);
        }

        // GET: Residenttypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Residenttypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResitypId,RestypDesc,CmpyId,UserId,RevDteTime")] Residenttype residenttype)
        {
            if (ModelState.IsValid)
            {
                _context.Add(residenttype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(residenttype);
        }

        // GET: Residenttypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residenttype = await _context.ms_residenttype.FindAsync(id);
            if (residenttype == null)
            {
                return NotFound();
            }
            return View(residenttype);
        }

        // POST: Residenttypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResitypId,RestypDesc,CmpyId,UserId,RevDteTime")] Residenttype residenttype)
        {
            if (id != residenttype.ResitypId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(residenttype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResidenttypeExists(residenttype.ResitypId))
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
            return View(residenttype);
        }

        // GET: Residenttypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residenttype = await _context.ms_residenttype
                .FirstOrDefaultAsync(m => m.ResitypId == id);
            if (residenttype == null)
            {
                return NotFound();
            }

            return View(residenttype);
        }

        // POST: Residenttypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var residenttype = await _context.ms_residenttype.FindAsync(id);
            if (residenttype != null)
            {
                _context.ms_residenttype.Remove(residenttype);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResidenttypeExists(int id)
        {
            return _context.ms_residenttype.Any(e => e.ResitypId == id);
        }
    }
}
