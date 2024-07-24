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
            return View(await _context.ms_complaintcatg.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var complaintCatg = await _context.ms_complaintcatg
                .FirstOrDefaultAsync(m => m.CmpCatgId == id);

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
            SetLayOutData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CplCatCde")] ComplaintCatg complaintCatg)
        {
            SetLayOutData();
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

    
        public async Task<IActionResult> Edit(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var complaintCatg = await _context.ms_complaintcatg.FindAsync(id);
            if (complaintCatg == null)
            {
                return NotFound();
            }
            return View(complaintCatg);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CmpCatgId,CplCatCde")] ComplaintCatg complaintCatg)
        {
            SetLayOutData();
            if (id != complaintCatg.CmpCatgId)
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
                    if (!ComplaintCatgExists(complaintCatg.CmpCatgId))
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

    
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintCatg = await _context.ms_complaintcatg
                .FirstOrDefaultAsync(m => m.CmpCatgId == id);
            if (complaintCatg == null)
            {
                return NotFound();
            }

            return View(complaintCatg);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complaintCatg = await _context.ms_complaintcatg.FindAsync(id);
            if (complaintCatg != null)
            {
                _context.ms_complaintcatg.Remove(complaintCatg);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintCatgExists(int id)
        {
            return _context.ms_complaintcatg.Any(e => e.CmpCatgId == id);
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