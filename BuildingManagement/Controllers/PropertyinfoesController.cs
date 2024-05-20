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
    public class PropertyInfoesController : Controller
    {
        private readonly BuildingDbContext _context;

        public PropertyInfoesController(BuildingDbContext context)
        {
            _context = context;
        }

        // GET: PropertyInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ms_propertyinfo.ToListAsync());
        }

        // GET: PropertyInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyInfo = await _context.ms_propertyinfo
                .FirstOrDefaultAsync(m => m.PropId == id);
            if (propertyInfo == null)
            {
                return NotFound();
            }

            return View(propertyInfo);
        }

        // GET: PropertyInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PropertyInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PropId,PropNme,Phone1,Phone2,Email,City,Township,Addr,AcreMeasure,ResitypId,BlockCount,RoomCount,ParkingCount,ParkingSizeDesc,PoolCount,PoolSizeDesc,EstiblishDte,CmpyId,UserId,RevDteTime")] PropertyInfo propertyInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(propertyInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(propertyInfo);
        }

        // GET: PropertyInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyInfo = await _context.ms_propertyinfo.FindAsync(id);
            if (propertyInfo == null)
            {
                return NotFound();
            }
            return View(propertyInfo);
        }

        // POST: PropertyInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PropId,PropNme,Phone1,Phone2,Email,City,Township,Addr,AcreMeasure,ResitypId,BlockCount,RoomCount,ParkingCount,ParkingSizeDesc,PoolCount,PoolSizeDesc,EstiblishDte,CmpyId,UserId,RevDteTime")] PropertyInfo propertyInfo)
        {
            if (id != propertyInfo.PropId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propertyInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyInfoExists(propertyInfo.PropId))
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
            return View(propertyInfo);
        }

        // GET: PropertyInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyInfo = await _context.ms_propertyinfo
                .FirstOrDefaultAsync(m => m.PropId == id);
            if (propertyInfo == null)
            {
                return NotFound();
            }

            return View(propertyInfo);
        }

        // POST: PropertyInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var propertyInfo = await _context.ms_propertyinfo.FindAsync(id);
            if (propertyInfo != null)
            {
                _context.ms_propertyinfo.Remove(propertyInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyInfoExists(int id)
        {
            return _context.ms_propertyinfo.Any(e => e.PropId == id);
        }
    }
}
