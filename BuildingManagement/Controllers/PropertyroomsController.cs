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
    public class PropertyroomsController : Controller
    {
        private readonly BuildingDbContext _context;

        public PropertyroomsController(BuildingDbContext context)
        {
            _context = context;
        }

        // GET: Propertyrooms
        public async Task<IActionResult> Index()
        {
            return View(await _context.ms_propertyroom.ToListAsync());
        }

        // GET: Propertyrooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyroom = await _context.ms_propertyroom
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (propertyroom == null)
            {
                return NotFound();
            }

            return View(propertyroom);
        }

        // GET: Propertyrooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Propertyrooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomId,PropId,RoomNo,LocId,BdtypId,SqFullMeasure,SqRooMeasure,AmenityDesc,FeatureDesc,Addr,TenantId,CmpyId,UserId,RevDteTime")] Propertyroom propertyroom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(propertyroom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(propertyroom);
        }

        // GET: Propertyrooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyroom = await _context.ms_propertyroom.FindAsync(id);
            if (propertyroom == null)
            {
                return NotFound();
            }
            return View(propertyroom);
        }

        // POST: Propertyrooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomId,PropId,RoomNo,LocId,BdtypId,SqFullMeasure,SqRooMeasure,AmenityDesc,FeatureDesc,Addr,TenantId,CmpyId,UserId,RevDteTime")] Propertyroom propertyroom)
        {
            if (id != propertyroom.RoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propertyroom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyroomExists(propertyroom.RoomId))
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
            return View(propertyroom);
        }

        // GET: Propertyrooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyroom = await _context.ms_propertyroom
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (propertyroom == null)
            {
                return NotFound();
            }

            return View(propertyroom);
        }

        // POST: Propertyrooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var propertyroom = await _context.ms_propertyroom.FindAsync(id);
            if (propertyroom != null)
            {
                _context.ms_propertyroom.Remove(propertyroom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyroomExists(int id)
        {
            return _context.ms_propertyroom.Any(e => e.RoomId == id);
        }
    }
}
