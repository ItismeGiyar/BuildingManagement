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
    public class ResidenttypesController : Controller
    {
        private readonly BuildingDbContext _context;

        public ResidenttypesController(BuildingDbContext context)
        {
            _context = context;
        }
        #region // Main Methods //
       

       
        public async Task<IActionResult> Index()
        {
            SetLayoutData();
            return View(await _context.ms_residenttype.ToListAsync());
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            SetLayoutData();
            if (id == null)
            {
                return NotFound();
            }

            var residenttype = await _context.ms_residenttype
                .FirstOrDefaultAsync(m => m.ResitypId == id);
            if (residenttype == null)
            {
                return NotFound();
            }

            residenttype.Company =
                _context.ms_company
                .Where(c => c.CmpyId == residenttype.CmpyId)
                .Select(c => c.CmpyNme)
                .FirstOrDefault() ?? "";

            residenttype.User =
                _context.ms_user
                .Where(u => u.UserId == residenttype.UserId)
                .Select(u => u.UserNme)
                .FirstOrDefault() ?? "";



            return View(residenttype);
        }


        public IActionResult Create()
        {
            SetLayoutData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RestypDesc")] Residenttype residenttype)
        {
            SetLayoutData();
            if (ModelState.IsValid)
            {
                residenttype.CmpyId = GetCmpyId(); 
                residenttype.UserId = GetUserId(); 
                residenttype.RevdteTime = DateTime.Now;
                _context.Add(residenttype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(residenttype);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            SetLayoutData();
            if (id == null)
            {
                return NotFound();
            }

            var residenttype = await _context.ms_residenttype.FindAsync(id);
            if (residenttype == null)
            {
                return NotFound();
            }
            return View(residenttype);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResitypId,RestypDesc")] Residenttype residenttype)
        {
            SetLayoutData();
            if (id != residenttype.ResitypId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    residenttype.CmpyId = GetCmpyId();
                    residenttype.UserId = GetUserId();
                   
                    residenttype.RevdteTime = DateTime.Now;
                    _context.Update(residenttype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResidenttypeExists(residenttype.ResitypId))
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
            return View(residenttype);
        }

        private bool ResidenttypeExists(int resitypId)
        {
            throw new NotImplementedException();
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            SetLayoutData();
            if (id == null)
            {
                return NotFound();
            }

            var residenttype = await _context.ms_residenttype
                .FirstOrDefaultAsync(m => m.ResitypId == id);
            if (residenttype == null)
            {
                return NotFound();
            }

            return View(residenttype);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SetLayoutData();
            var residenttype = await _context.ms_residenttype.FindAsync(id);
            if (residenttype != null)
            {
                _context.ms_residenttype.Remove(residenttype);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResidenttypeExists(short id)
        {
            return _context.ms_residenttype.Any(e => e.ResitypId == id);
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
