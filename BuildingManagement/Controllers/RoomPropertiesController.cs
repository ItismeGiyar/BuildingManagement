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
    public class RoomPropertiesController : Controller
    {
        private readonly BuildingDbContext _context;

        public RoomPropertiesController(BuildingDbContext context)
        {
            _context = context;
        }

        // GET: RoomProperties
        public async Task<IActionResult> Index()
        {
            var list = await _context.MS_PropertyRoom.Select(m => (double)m.SqFullMeasure).ToListAsync();

            return View(await _context.MS_PropertyRoom.ToListAsync());
        }

        // GET: RoomProperties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomProperty = await _context.MS_PropertyRoom
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (roomProperty == null)
            {
                return NotFound();
            }

            return View(roomProperty);
        }

        // GET: RoomProperties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoomProperties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomId,RoomNo,LocId,BdTypId,SqFullMeasure,SqRoomeasure,AmenityDesc,FeatureDesc,Addr,TenantId,CmpyId,UserId,RevDteTime")] RoomProperty roomProperty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomProperty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roomProperty);
        }

        // GET: RoomProperties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomProperty = await _context.MS_PropertyRoom.FindAsync(id);
            if (roomProperty == null)
            {
                return NotFound();
            }
            return View(roomProperty);
        }

        // POST: RoomProperties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomId,RoomNo,LocId,BdTypId,SqFullMeasure,SqRoomeasure,AmenityDesc,FeatureDesc,Addr,TenantId,CmpyId,UserId,RevDteTime")] RoomProperty roomProperty)
        {
            if (id != roomProperty.RoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomProperty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomPropertyExists(roomProperty.RoomId))
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
            return View(roomProperty);
        }

        // GET: RoomProperties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomProperty = await _context.MS_PropertyRoom
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (roomProperty == null)
            {
                return NotFound();
            }

            return View(roomProperty);
        }

        // POST: RoomProperties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomProperty = await _context.MS_PropertyRoom.FindAsync(id);
            if (roomProperty != null)
            {
                _context.MS_PropertyRoom.Remove(roomProperty);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomPropertyExists(int id)
        {
            return _context.MS_PropertyRoom.Any(e => e.RoomId == id);
        }
    }
}
