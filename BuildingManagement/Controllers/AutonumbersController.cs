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
    public class AutonumbersController : Controller
    {
        private readonly BuildingDbContext _context;

        public AutonumbersController(BuildingDbContext context)
        {
            _context = context;
        }
        #region // Main Methods //

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
            return View(await _context.pms_autonumber.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var autonumber = await _context.pms_autonumber
                .FirstOrDefaultAsync(m => m.AutoNoId == id);
            if (autonumber == null)
            {
                return NotFound();
            }
            autonumber.Company =_context.ms_company.Where(c => c.CmpyId == autonumber.CmpyId).Select(c => c.CmpyNme).FirstOrDefault() ?? "";
            return View(autonumber);
        }

      
        public IActionResult Create()
        {
            SetLayOutData();
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillPrefix,BizDte,ZeroLeading,RunningNo,LastUsedNo,LastGenerateDte")] Autonumber autonumber)
        {
            SetLayOutData();
            if (ModelState.IsValid)
            {
                autonumber.CmpyId = GetCmpyId();
                _context.Add(autonumber);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(autonumber);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var autonumber = await _context.pms_autonumber.FindAsync(id);
            if (autonumber == null)
            {
                return NotFound();
            }
            return View(autonumber);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AutoNoId,BillPrefix,BizDte,ZeroLeading,RunningNo,LastUsedNo,LastGenerateDte")] Autonumber autonumber)
        {
            SetLayOutData();
            if (id != autonumber.AutoNoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    autonumber.CmpyId = GetCmpyId();
                    _context.Update(autonumber);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutonumberExists(autonumber.AutoNoId))
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
            return View(autonumber);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var autonumber = await _context.pms_autonumber
                .FirstOrDefaultAsync(m => m.AutoNoId == id);
            if (autonumber == null)
            {
                return NotFound();
            }
            autonumber.Company =
                _context.ms_company
                .Where(c => c.CmpyId == autonumber.CmpyId)
                .Select(c => c.CmpyNme)
                .FirstOrDefault() ?? "";

            return View(autonumber);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SetLayOutData();
            var autonumber = await _context.pms_autonumber.FindAsync(id);
            if (autonumber != null)
            {
                _context.pms_autonumber.Remove(autonumber);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutonumberExists(int id)
        {
            return _context.pms_autonumber.Any(e => e.AutoNoId == id);
        }
        #endregion
        #region // Common Methods //
        protected void SetLayOutData()
        {
            var userCde = HttpContext.User.Claims.FirstOrDefault()?.Value; // format for to claim usercde

            var userName = _context.ms_user.Where(u => u.UserCde == userCde).Select(u => u.UserNme).FirstOrDefault();

            ViewBag.UserName = userName;

        }
        #endregion
       


    }
}
