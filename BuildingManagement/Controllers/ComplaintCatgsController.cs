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
    public class ComplaintCatgsController : Controller
    {
        private readonly BuildingDbContext _context;

        public ComplaintCatgsController(BuildingDbContext context)
        {
            _context = context;
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


        #region // Main Methods //

        public async Task<IActionResult> Index()
        {
            return View(await _context.ms_complaintCatg.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintCatg = await _context.ms_complaintCatg
                .FirstOrDefaultAsync(m => m.CplCatgId == id);

            if (complaintCatg == null)
            {
                return NotFound();
            }

            complaintCatg.Company = 
                _context.ms_company
                .Where(c => c.CmpyId == complaintCatg.CmpyId)
                .Select(c => c.CmpyNme)
                .FirstOrDefault() ?? "";

            complaintCatg.User =
                _context.ms_user
                .Where(u => u.UserId == complaintCatg.UserId)
                .Select(u => u.UserNme)
                .FirstOrDefault() ?? "";
            

            return View(complaintCatg);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CplCatCde")] ComplaintCatg complaintCatg)
        {
            if (ModelState.IsValid)
            {
                complaintCatg.CmpyId = GetCmpyId(); //default
                complaintCatg.UserId = GetUserId(); //default
                complaintCatg.RevDteTime = DateTime.Now;
                _context.Add(complaintCatg);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(complaintCatg);
        }

        // GET: ComplaintCatgs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintCatg = await _context.ms_complaintCatg.FindAsync(id);
            if (complaintCatg == null)
            {
                return NotFound();
            }
            return View(complaintCatg);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CplCatgId,CplCatCde")] ComplaintCatg complaintCatg)
        {
            if (id != complaintCatg.CplCatgId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    complaintCatg.CmpyId = GetCmpyId(); //default
                    complaintCatg.UserId = GetUserId(); //default
                    complaintCatg.RevDteTime = DateTime.Now;
                    _context.Update(complaintCatg);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintCatgExists(complaintCatg.CplCatgId))
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
            return View(complaintCatg);
        }

        // GET: ComplaintCatgs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintCatg = await _context.ms_complaintCatg
                .FirstOrDefaultAsync(m => m.CplCatgId == id);
            if (complaintCatg == null)
            {
                return NotFound();
            }

            return View(complaintCatg);
        }

        // POST: ComplaintCatgs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complaintCatg = await _context.ms_complaintCatg.FindAsync(id);
            if (complaintCatg != null)
            {
                _context.ms_complaintCatg.Remove(complaintCatg);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintCatgExists(int id)
        {
            return _context.ms_complaintCatg.Any(e => e.CplCatgId == id);
        }
        #endregion
    }
}
