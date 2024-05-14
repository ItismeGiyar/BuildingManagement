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
    public class PropertyRoomsController : Controller
    {
        private readonly BuildingDbContext _context;

        public PropertyRoomsController(BuildingDbContext context)
        {
            _context = context;
        }
        protected short GetUserId()
        {
            var userCde = HttpContext.User.Claims.FirstOrDefault()?.Value;
            var userId = (short)_context.ms_user
                .Where(u => u.UserCde == userCde)
                .Select(u => u.UserId)
                .FirstOrDefault();

            return userId;
        }

        protected short GetCmpyId()
        {
            var cmpyId = _context.ms_user
                .Where(u => u.UserId == GetUserId())
                .Select(u => u.CmpyId)
                .FirstOrDefault();

            return cmpyId;
        }

        // GET: PropertyRooms
        public async Task<IActionResult> Index()
        {

            var list = await _context.ms_propertyRoom.ToListAsync();

            foreach(var data in list)
            {
                data.Tenant = _context.ms_tenant.Where(t => t.TenantId == data.TenantId).Select(t => t.TenantNme).FirstOrDefault() ?? "";
                data.PropertyInfo = _context.ms_propertyInfo.Where(t => t.PropId == data.PropId).Select(t => t.PropNme).FirstOrDefault() ?? "";
                data.BuildingType = _context.ms_buildingType.Where(t => t.BdtypId== data.BdtypId).Select(t => t.BdtypDesc).FirstOrDefault() ?? "";
                data.Location = _context.ms_location.Where(t => t.LocId == data.LocId).Select(t => t.LocDesc).FirstOrDefault() ?? "";



            }

            return View(list);
        }

        // GET: PropertyRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyRoom = await _context.ms_propertyRoom
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (propertyRoom == null)
            {
                return NotFound();
            }
            propertyRoom.Company =
               _context.ms_company
               .Where(c => c.CmpyId == propertyRoom.CmpyId)
               .Select(c => c.CmpyNme)
               .FirstOrDefault() ?? "";

            propertyRoom.User =
                _context.ms_user
                .Where(u => u.UserId == propertyRoom.UserId)
                .Select(u => u.UserNme)
                .FirstOrDefault() ?? "";
            propertyRoom.PropertyInfo =
             _context.ms_propertyInfo
             .Where(c => c.PropId == propertyRoom.PropId)
             .Select(c => c.PropNme)
             .FirstOrDefault() ?? "";

            propertyRoom.BuildingType =
                _context.ms_buildingType
                .Where(u => u.BdtypId == propertyRoom.BdtypId)
                .Select(u => u.BdtypDesc)
                .FirstOrDefault() ?? "";
            propertyRoom.Location =
                _context.ms_location
                .Where(u => u.LocId == propertyRoom.LocId)
                .Select(u => u.LocDesc)
                .FirstOrDefault() ?? "";

            propertyRoom.Tenant =
                _context.ms_tenant
                .Where(u => u.TenantId == propertyRoom.TenantId)
                .Select(u => u.TenantNme)
                .FirstOrDefault() ?? "";
            return View(propertyRoom);
        }

        // GET: PropertyRooms/Create
        public IActionResult Create()
        {
            var list = _context.ms_propertyInfo.ToList();
            ViewData["PropertyList"] = new SelectList(list, "PropId", "PropNme");

            ViewData["TenantList"] = new SelectList(_context.ms_tenant.ToList(), "TenantId", "TenantNme");
            ViewData["BuildingTypeList"] = new SelectList(_context.ms_buildingType.ToList(), "BdtypId", "BdtypDesc");
            ViewData["LocationList"] = new SelectList(_context.ms_location.ToList(), "LocId", "LocDesc");

            return View();
        }

        // POST: PropertyRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PropId,RoomNo,LocId,BdtypId,SqFullMeasure,SqRooMeasure,AmenityDesc,FeatureDesc,Addr,TenantId")] PropertyRoom propertyRoom)
        {
            if (ModelState.IsValid)
            {
                propertyRoom.CmpyId = GetCmpyId();//default
                propertyRoom.UserId = GetUserId();//default
                propertyRoom.RevDteTime = DateTime.Now;
                _context.Add(propertyRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(propertyRoom);
        }
        
        // GET: PropertyRooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyRoom = await _context.ms_propertyRoom.FindAsync(id);
            if (propertyRoom == null)
            {
                return NotFound();
            }
            var list = _context.ms_propertyInfo.ToList();
            ViewData["PropertyList"] = new SelectList(list, "PropId", "PropNme");
            ViewData["TenantList"] = new SelectList(_context.ms_tenant.ToList(), "TenantId", "TenantNme");
            ViewData["BuildingTypeList"] = new SelectList(_context.ms_buildingType.ToList(), "BdtypId", "BdtypDesc");
            ViewData["LocationList"] = new SelectList(_context.ms_location.ToList(), "LocId", "LocDesc");
            return View(propertyRoom);
        }

        // POST: PropertyRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomId,PropId,RoomNo,LocId,BdtypId,SqFullMeasure,SqRooMeasure,AmenityDesc,FeatureDesc,Addr,TenantId")] PropertyRoom propertyRoom)
        {
            if (id != propertyRoom.RoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                   
                {
                    propertyRoom.CmpyId = GetCmpyId();//default
                    propertyRoom.UserId = GetUserId();//default;
                    propertyRoom.RevDteTime=DateTime.Now;


                    _context.Update(propertyRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyRoomExists(propertyRoom.RoomId))
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
            return View(propertyRoom);
        }

        // GET: PropertyRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyRoom = await _context.ms_propertyRoom
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (propertyRoom == null)
            {
                return NotFound();
            }

            return View(propertyRoom);
        }

        // POST: PropertyRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var propertyRoom = await _context.ms_propertyRoom.FindAsync(id);
            if (propertyRoom != null)
            {
                _context.ms_propertyRoom.Remove(propertyRoom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyRoomExists(int id)
        {
            return _context.ms_propertyRoom.Any(e => e.RoomId == id);
        }
    }
}
