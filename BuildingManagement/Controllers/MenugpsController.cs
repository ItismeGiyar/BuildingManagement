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
    public class MenugpsController : Controller
    {
        private readonly BuildingDbContext _context;

        public MenugpsController(BuildingDbContext context)
        {
            _context = context;
        }
        #region // Main Methods //

        public async Task<IActionResult> Index()
        {
            SetLayoutData();
            return View(await _context.ms_menugp.ToListAsync());
        }

        public async Task<IActionResult> Details(short? id)
        {
            SetLayoutData();
            if (id == null)
            {
                return NotFound();
            }

            var menugp = await _context.ms_menugp
                .FirstOrDefaultAsync(m => m.MnugrpId == id);
            if (menugp == null)
            {
                return NotFound();
            }

            return View(menugp);
        }

        
        public IActionResult Create()
        {
            SetLayoutData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MnugrpNme")] Menugp menugp)
        {
            SetLayoutData();
            if (ModelState.IsValid)
            {
                menugp.RevDteTime = DateTime.Now;
                _context.Add(menugp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menugp);
        }

        
        public async Task<IActionResult> Edit(short? id)
        {
            SetLayoutData();
            if (id == null)
            {
                return NotFound();
            }

            var menugp = await _context.ms_menugp.FindAsync(id);
            if (menugp == null)
            {
                return NotFound();
            }
            return View(menugp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("MnugrpId,MnugrpNme")] Menugp menugp)
        {
            SetLayoutData();
            if (id != menugp.MnugrpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    menugp.RevDteTime = DateTime.Now;
                    _context.Update(menugp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenugpExists(menugp.MnugrpId))
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
            return View(menugp);
        }

       
        public async Task<IActionResult> Delete(short? id)
        {
            SetLayoutData();
            if (id == null)
            {
                return NotFound();
            }

            var menugp = await _context.ms_menugp
                .FirstOrDefaultAsync(m => m.MnugrpId == id);
            if (menugp == null)
            {
                return NotFound();
            }

            return View(menugp);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            SetLayoutData();
            var menugp = await _context.ms_menugp.FindAsync(id);
            if (menugp != null)
            {
                _context.ms_menugp.Remove(menugp);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public bool MenugpExists(short id)
        {
            return _context.ms_menugp.Any(e => e.MnugrpId == id);
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
