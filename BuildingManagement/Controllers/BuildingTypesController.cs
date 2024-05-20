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
    public class BuildingtypesController : Controller
    {
        private readonly BuildingDbContext _context;

        public BuildingtypesController(BuildingDbContext context)
        {
            _context = context;
        }

        // GET: Buildingtypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ms_buildingtype.ToListAsync());
        }

        // GET: Buildingtypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildingtype = await _context.ms_buildingtype
                .FirstOrDefaultAsync(m => m.BdtypId == id);
            if (buildingtype == null)
            {
                return NotFound();
            }
            buildingtype.Company =
             _context.ms_company
             .Where(c => c.CmpyId == buildingtype.CmpyId)
             .Select(c => c.CmpyNme)
             .FirstOrDefault() ?? "";


            buildingtype.User=
             _context.ms_user
             .Where(u => u.UserId == buildingtype.UserId)
             .Select(u => u.UserNme)
             .FirstOrDefault() ?? "";

            return View(buildingtype);
        }

        // GET: Buildingtypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Buildingtypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BdtypDesc")] Buildingtype buildingtype)
        {
            if (ModelState.IsValid)
            {

                buildingtype.CmpyId = 1; //default
                buildingtype.UserId = 1; //default
                buildingtype.RevDteTime = DateTime.Now;
                _context.Add(buildingtype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(buildingtype);
        }

        // GET: Buildingtypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildingtype = await _context.ms_buildingtype.FindAsync(id);
            if (buildingtype == null)
            {
                return NotFound();
            }
            return View(buildingtype);
        }

        // POST: Buildingtypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BdtypId,BdtypDesc")] Buildingtype buildingtype)
        {
            if (id != buildingtype.BdtypId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    buildingtype.CmpyId = 1; //default
                    buildingtype.UserId = 1; //default
                    buildingtype.RevDteTime = DateTime.Now;
                    _context.Update(buildingtype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuildingtypeExists(buildingtype.BdtypId))
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
            return View(buildingtype);
        }

        // GET: Buildingtypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildingtype = await _context.ms_buildingtype
                .FirstOrDefaultAsync(m => m.BdtypId == id);
            if (buildingtype == null)
            {
                return NotFound();
            }

            return View(buildingtype);
        }

        // POST: Buildingtypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buildingtype = await _context.ms_buildingtype.FindAsync(id);
            if (buildingtype != null)
            {
                _context.ms_buildingtype.Remove(buildingtype);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuildingtypeExists(int id)
        {
            return _context.ms_buildingtype.Any(e => e.BdtypId == id);
        }
    }
}
