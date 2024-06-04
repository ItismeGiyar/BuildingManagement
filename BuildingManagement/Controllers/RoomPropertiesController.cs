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


        #region // Main Methods //
        public async Task<IActionResult> Index()
        {
            SetLayoutData();
            var list = await _context.ms_propertyroom.Select(m => (double)m.SqFullMeasure).ToListAsync();

            return View(await _context.ms_propertyroom.ToListAsync());
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            SetLayoutData();
            if (id == null)
            {
                return NotFound();
            }

            var roomProperty = await _context.ms_propertyroom
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (roomProperty == null)
            {
                return NotFound();
            }

            return View(roomProperty);
        }

        
        public IActionResult Create()
        {
            SetLayoutData();
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomId,RoomNo,LocId,BdTypId,SqFullMeasure,SqRoomeasure,AmenityDesc,FeatureDesc,Addr,TenantId,CmpyId,UserId,RevDteTime")] RoomProperty roomProperty)
        {
            SetLayoutData();
            if (ModelState.IsValid)
            {
                _context.Add(roomProperty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roomProperty);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            SetLayoutData();
            if (id == null)
            {
                return NotFound();
            }

            var roomProperty = await _context.ms_propertyroom.FindAsync(id);
            if (roomProperty == null)
            {
                return NotFound();
            }
            return View(roomProperty);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomId,RoomNo,LocId,BdTypId,SqFullMeasure,SqRoomeasure,AmenityDesc,FeatureDesc,Addr,TenantId,CmpyId,UserId,RevDteTime")] RoomProperty roomProperty)
        {
            SetLayoutData();
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

        
        public async Task<IActionResult> Delete(int? id)
        {
            SetLayoutData();
            if (id == null)
            {
                return NotFound();
            }

            var roomProperty = await _context.ms_propertyroom
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (roomProperty == null)
            {
                return NotFound();
            }

            return View(roomProperty);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SetLayoutData();
            var roomProperty = await _context.ms_propertyroom.FindAsync(id);
            if (roomProperty != null)
            {
                _context.ms_propertyroom.Remove(roomProperty);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomPropertyExists(int id)
        {
            return _context.ms_propertyroom.Any(e => e.RoomId == id);
        }
        #endregion

        #region // Common Methods //
        protected void SetLayoutData()
        {
            var userCde = HttpContext.User.Claims.FirstOrDefault()?.Value; // format for to claim usercde

            var userName = _context.ms_user.Where(u => u.UserCde == userCde).Select(u => u.UserNme).FirstOrDefault();

            ViewBag.UserName = userName;
        }
        #endregion
    }
}
