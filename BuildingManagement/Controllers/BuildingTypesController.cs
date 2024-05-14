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
    public class BuildingTypesController(BuildingDbContext context) : Controller
    {
        private readonly BuildingDbContext _context = context;

        // GET: BuildingTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ms_buildingType.ToListAsync());
        }

        // GET: BuildingTypes/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildingType = await _context.ms_buildingType
                .FirstOrDefaultAsync(m => m.BdtypId == id);
            if (buildingType == null)
            {
                return NotFound();
            }

            return View(buildingType);
        }

        // GET: BuildingTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BuildingTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BdtypId,BdtypDesc,CmpyId,UserId,RevDteTime")] BuildingType buildingType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buildingType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(buildingType);
        }

        // GET: BuildingTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildingType = await _context.ms_buildingType.FindAsync(id);
            if (buildingType == null)
            {
                return NotFound();
            }
            return View(buildingType);
        }

        // POST: BuildingTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("BdtypId,BdtypDesc,CmpyId,UserId,RevDteTime")] BuildingType buildingType)
        {
            if (id != buildingType.BdtypId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buildingType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuildingTypeExists(buildingType.BdtypId))
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
            return View(buildingType);
        }

        private bool BuildingTypeExists(int bdtypId)
        {
            throw new NotImplementedException();
        }

        // GET: BuildingTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildingType = await _context.ms_buildingType
                .FirstOrDefaultAsync(m => m.BdtypId == id);
            if (buildingType == null)
            {
                return NotFound();
            }

            return View(buildingType);
        }

        // POST: BuildingTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buildingType = await _context.ms_buildingType.FindAsync(id);
            if (buildingType != null)
            {
                _context.ms_buildingType.Remove(buildingType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuildingTypeExists(short id)
        {
            return _context.ms_buildingType.Any(e => e.BdtypId == id);
        }
    }
}
