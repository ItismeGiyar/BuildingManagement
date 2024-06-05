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
            return View(await _context.ms_buildingtype.ToListAsync());
        }


       
        public async Task<IActionResult> Details(int? id)
            {
            SetLayOutData();
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


                buildingtype.User =
                 _context.ms_user
                 .Where(u => u.UserId == buildingtype.UserId)
                 .Select(u => u.UserNme)
                 .FirstOrDefault() ?? "";

                return View(buildingtype);
            }

          
            public IActionResult Create()
            {
            SetLayOutData();
            return View();
            }

           
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("BdtypDesc")] Buildingtype buildingtype)
            {
            SetLayOutData();
            if (ModelState.IsValid)
                {

                    buildingtype.CmpyId = GetCmpyId(); //default
                    buildingtype.UserId = GetUserId(); //default
                    buildingtype.RevDteTime = DateTime.Now;
                    _context.Add(buildingtype);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(buildingtype);
            }
        
        public async Task<IActionResult> Edit(int? id)
        {
            SetLayOutData();
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


        
        [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("BdtypId,BdtypDesc")] Buildingtype buildingtype)
            {
            SetLayOutData();
            if (id != buildingtype.BdtypId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        buildingtype.CmpyId = GetCmpyId(); //default
                        buildingtype.UserId = GetUserId(); //default
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

            public async Task<IActionResult> Delete(int? id)
            {
            SetLayOutData();
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

            
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
            SetLayOutData();
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
