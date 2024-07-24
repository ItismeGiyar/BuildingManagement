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
        #region // Main Methods //
       

        public async Task<IActionResult> Index()
        {
            SetLayoutData();

            var list = await _context.ms_propertyroom.ToListAsync();

            foreach (var data in list)
            {
                data.Tenant = _context.ms_tenant.Where(t => t.TenantId == data.TenantId).Select(t => t.TenantNme).FirstOrDefault() ?? "";
                data.PropertyInfo = _context.ms_propertyinfo.Where(t => t.PropId == data.PropId).Select(t => t.PropNme).FirstOrDefault() ?? "";
                data.BuildingType = _context.ms_buildingtype.Where(t => t.BdtypId== data.BdtypId).Select(t => t.BdtypDesc).FirstOrDefault() ?? "";
                data.Location = _context.ms_location.Where(t => t.LocId == data.LocId).Select(t => t.LocDesc).FirstOrDefault() ?? "";



            }

            return View(list);
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            SetLayoutData();
            if (id == null)
            {
                return NotFound();
            }

            var propertyRoom = await _context.ms_propertyroom
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
             _context.ms_propertyinfo
             .Where(c => c.PropId == propertyRoom.PropId)
             .Select(c => c.PropNme)
             .FirstOrDefault() ?? "";

            propertyRoom.BuildingType =
                _context.ms_buildingtype
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

        
        public IActionResult Create()
        {
            SetLayoutData();
            var list = _context.ms_propertyinfo.ToList();
            ViewData["PropertyList"] = new SelectList(list, "PropId", "PropNme");

            ViewData["TenantList"] = new SelectList(_context.ms_tenant.ToList(), "TenantId", "TenantNme");
            ViewData["BuildingTypeList"] = new SelectList(_context.ms_buildingtype.ToList(), "BdtypId", "BdtypDesc");
            ViewData["LocationList"] = new SelectList(_context.ms_location.ToList(), "LocId", "LocDesc");

            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PropId,RoomNo,LocId,BdtypId,SqFullMeasure,SqRooMeasure,AmenityDesc,FeatureDesc,Addr,TenantId")] PropertyRoom propertyRoom)
        {
            SetLayoutData();
            if (ModelState.IsValid)
            {
                propertyRoom.CmpyId = GetCmpyId();
                propertyRoom.UserId = GetUserId();
                propertyRoom.RevDteTime = DateTime.Now;
                _context.Add(propertyRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(propertyRoom);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            SetLayoutData();
            if (id == null)
            {
                return NotFound();
            }

            var propertyRoom = await _context.ms_propertyroom.FindAsync(id);
            if (propertyRoom == null)
            {
                return NotFound();
            }
            var list = _context.ms_propertyinfo.ToList();
            ViewData["PropertyList"] = new SelectList(list, "PropId", "PropNme");
            ViewData["TenantList"] = new SelectList(_context.ms_tenant.ToList(), "TenantId", "TenantNme");
            ViewData["BuildingTypeList"] = new SelectList(_context.ms_buildingtype.ToList(), "BdtypId", "BdtypDesc");
            ViewData["LocationList"] = new SelectList(_context.ms_location.ToList(), "LocId", "LocDesc");
            return View(propertyRoom);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomId,PropId,RoomNo,LocId,BdtypId,SqFullMeasure,SqRooMeasure,AmenityDesc,FeatureDesc,Addr,TenantId")] PropertyRoom propertyRoom)
        {
            SetLayoutData();
            if (id != propertyRoom.RoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try

                {
                    propertyRoom.CmpyId = GetCmpyId();
                    propertyRoom.UserId = GetUserId();
                    propertyRoom.RevDteTime = DateTime.Now;


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

        public async Task<IActionResult> Delete(int? id)
        {
            
            SetLayoutData();
            if (id == null)
            {
                return NotFound();
            }

            var propertyRoom = await _context.ms_propertyroom
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
             _context.ms_propertyinfo
             .Where(c => c.PropId == propertyRoom.PropId)
             .Select(c => c.PropNme)
             .FirstOrDefault() ?? "";

            propertyRoom.BuildingType =
                _context.ms_buildingtype
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

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SetLayoutData();
            var propertyRoom = await _context.ms_propertyroom.FindAsync(id);
            if (propertyRoom != null)
            {
                _context.ms_propertyroom.Remove(propertyRoom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyRoomExists(int id)
        {
            return _context.ms_propertyroom.Any(e => e.RoomId == id);
        }
        #endregion
        #region // Common Methods //
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
        protected void SetLayoutData()
        {
            var userCde = HttpContext.User.Claims.FirstOrDefault()?.Value; // format for to claim usercde

            var userName = _context.ms_user.Where(u => u.UserCde == userCde).Select(u => u.UserNme).FirstOrDefault();

            ViewBag.UserName = userName;
        }
        #endregion
    }
}
