﻿using System;
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
    public class LocationsController : Controller
    {
        private readonly BuildingDbContext _context;

        public LocationsController(BuildingDbContext context)
        {
            _context = context;
        }


        #region // Main methods //
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


        public async Task<IActionResult> Index()
        {
            SetLayOutData();
            return View(await _context.ms_location.ToListAsync());
        }

    
        public async Task<IActionResult> Details(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.ms_location
                .FirstOrDefaultAsync(m => m.LocId == id);
            if (location == null)
            {

                return NotFound();
            }

            location.Company =
                _context.ms_company
                .Where(c => c.CmpyId == location.CmpyId)
                .Select(c => c.CmpyNme)
                .FirstOrDefault() ?? "";

            location.User =
                _context.ms_user
                .Where(u => u.UserId == location.UserId)
                .Select(u => u.UserNme)
                .FirstOrDefault() ?? "";

            return View(location);
        }

     
        public IActionResult Create()
        {
            SetLayOutData();
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocDesc")] Location location)
        {
            SetLayOutData();
            if (ModelState.IsValid)
            {
                location.CmpyId = GetCmpyId(); //default
                location.UserId = GetUserId(); //default
                location.RevDteTime = DateTime.Now;

                _context.Add(location);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

     
        public async Task<IActionResult> Edit(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.ms_location.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocId,LocDesc")] Location location)
        {
            SetLayOutData();
            if (id != location.LocId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    location.CmpyId = GetCmpyId(); //default
                    location.UserId = GetUserId(); //default
                    location.RevDteTime = DateTime.Now;
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.LocId))
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
            return View(location);
        }

      
        public async Task<IActionResult> Delete(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.ms_location
                .FirstOrDefaultAsync(m => m.LocId == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SetLayOutData();
            var location = await _context.ms_location.FindAsync(id);
            if (location != null)
            {
                _context.ms_location.Remove(location);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            return _context.ms_location.Any(e => e.LocId == id);
        }
        #endregion

        #region // Common methods //
        protected void SetLayOutData()
        {
            var userCde = HttpContext.User.Claims.FirstOrDefault()?.Value; // format for to claim usercde

            var userName = _context.ms_user.Where(u => u.UserCde == userCde).Select(u => u.UserNme).FirstOrDefault();

            ViewBag.UserName = userName;

        }
        #endregion
    }
}
