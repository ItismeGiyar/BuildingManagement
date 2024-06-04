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
using static System.Runtime.InteropServices.JavaScript.JSType;


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

        [HttpGet]
        public ActionResult MyAction()
        {
            return View();
        }

       
        // GET: PropertyInfoes
        public async Task<IActionResult> Index()
        {
            var list = await _context.ms_propertyinfo.ToListAsync();

            foreach (var data in list)
            {
                data.Billitem = _context.ms_residenttype.Where(rt => rt.ResitypId == data.ResitypId).Select(rt => rt.RestypDesc).FirstOrDefault() ?? "";
            }
            return View(list);
        }

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
            propertyInfo.Billitem = _context.ms_residenttype.Where(rt => rt.ResitypId == propertyInfo.ResitypId).Select(rt => rt.RestypDesc).FirstOrDefault() ?? "";
            propertyInfo.Company = _context.ms_company.Where(c => c.CmpyId == propertyInfo.CmpyId).Select(c => c.CmpyNme).FirstOrDefault() ?? "";
            propertyInfo.User = _context.ms_user.Where(u => u.UserId == propertyInfo.UserId).Select(u => u.UserNme).FirstOrDefault() ?? "";

            return View(propertyInfo);
        }

        public IActionResult Create()
        {
            ViewData["ResidentTypes"] = new SelectList(_context.ms_residenttype.ToList(), "ResitypId", "RestypDesc");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PropNme,Phone1,Phone2,Email,City,Township,Addr,AcreMeasure,ResitypId,BlockCount,RoomCount,ParkingCount,ParkingSizeDesc,PoolCount,PoolSizeDesc,EstiblishDte")] PropertyInfo propertyInfo)
        {
            if (ModelState.IsValid)
            {
                propertyInfo.CmpyId = GetCmpyId();
                propertyInfo.UserId = GetUserId();
                propertyInfo.RevDteTime = DateTime.Now;
                _context.Add(propertyInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ResidentTypes"] = new SelectList(_context.ms_residenttype.ToList(), "ResitypId", "RestypDesc");
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

            ViewData["ResidentTypes"] = new SelectList(_context.ms_residenttype.ToList(), "ResitypId", "RestypDesc");
            return View(propertyInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PropId,PropNme,Phone1,Phone2,Email,City,Township,Addr,AcreMeasure,ResitypId,BlockCount,RoomCount,ParkingCount,ParkingSizeDesc,PoolCount,PoolSizeDesc,EstiblishDte")] PropertyInfo propertyInfo)
        {
            if (id != propertyInfo.PropId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    propertyInfo.CmpyId = GetCmpyId();
                    propertyInfo.UserId = GetUserId();
                    propertyInfo.RevDteTime = DateTime.Now;
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
            ViewData["ResidentTypes"] = new SelectList(_context.ms_residenttype.ToList(), "ResitypId", "RestypDesc");
            return View(propertyInfo);
        }

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


        #region // Global Methods (Important) //

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

        #endregion
    }
}
