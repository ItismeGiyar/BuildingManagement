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
    public class PropertyinfoesController : Controller
    {
        private readonly BuildingDbContext _context;

        public PropertyinfoesController(BuildingDbContext context)
        {
            _context = context;
        }

        // GET: Propertyinfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ms_propertyinfo.ToListAsync());
        }

        // GET: Propertyinfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyinfo = await _context.ms_propertyinfo
                .FirstOrDefaultAsync(m => m.PropId == id);
            if (propertyinfo == null)
            {
                return NotFound();
            }

            return View(propertyinfo);
        }

        // GET: Propertyinfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Propertyinfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PropId,PropNme,Phone1,Phone2,Email,City,Township,Addr,AcreMeasure,ResitypId,BlockCount,RoomCount,ParkingCount,ParkingSizeDesc,PoolCount,PoolSizeDesc,EstiblishDte,CmpyId,UserId,RevDteTime")] Propertyinfo propertyinfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(propertyinfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(propertyinfo);
        }

        // GET: Propertyinfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyinfo = await _context.ms_propertyinfo.FindAsync(id);
            if (propertyinfo == null)
            {
                return NotFound();
            }
            return View(propertyinfo);
        }

        // POST: Propertyinfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PropId,PropNme,Phone1,Phone2,Email,City,Township,Addr,AcreMeasure,ResitypId,BlockCount,RoomCount,ParkingCount,ParkingSizeDesc,PoolCount,PoolSizeDesc,EstiblishDte,CmpyId,UserId,RevDteTime")] Propertyinfo propertyinfo)
        {
            if (id != propertyinfo.PropId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propertyinfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyinfoExists(propertyinfo.PropId))
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
            return View(propertyinfo);
        }

        // GET: Propertyinfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyinfo = await _context.ms_propertyinfo
                .FirstOrDefaultAsync(m => m.PropId == id);
            if (propertyinfo == null)
            {
                return NotFound();
            }

            return View(propertyinfo);
        }

        // POST: Propertyinfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var propertyinfo = await _context.ms_propertyinfo.FindAsync(id);
            if (propertyinfo != null)
            {
                _context.ms_propertyinfo.Remove(propertyinfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyinfoExists(int id)
        {
            return _context.ms_propertyinfo.Any(e => e.PropId == id);
        }
    }
}
