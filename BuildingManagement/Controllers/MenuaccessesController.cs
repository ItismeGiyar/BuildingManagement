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
    public class MenuaccessesController : Controller
    {
        private readonly BuildingDbContext _context;

        public MenuaccessesController(BuildingDbContext context)
        {
            _context = context;
        }

        // GET: Menuaccesses
        public async Task<IActionResult> Index()
        {
            var list = await _context.ms_menuaccess.ToListAsync();
            foreach (var data in list)
            {
                data.Menugp = _context.ms_menugp.Where(m => m.MnugrpId == data.MnugrpId).Select(m => m.MnugrpNme).FirstOrDefault() ?? "";
            }
            return View(list);
        }

        // GET: Menuaccesses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuaccess = await _context.ms_menuaccess
                .FirstOrDefaultAsync(m => m.AccessId == id);
            if (menuaccess == null)
            {
                return NotFound();
            }
            menuaccess.Menugp =
              _context.ms_menugp
              .Where(c => c.MnugrpId == menuaccess.MnugrpId)
              .Select(c => c.MnugrpNme)
              .FirstOrDefault() ?? "";

            return View(menuaccess);
        }

        public IActionResult Create()
        {
            ViewData["MenuGroupList"] = new SelectList(_context.ms_menugp.ToList(), "MnugrpId", "MnugrpNme");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MnugrpId,BtnNme")] Menuaccess menuaccess)
        {
            if (ModelState.IsValid)
            {
                menuaccess.RevDteTime = DateTime.Now;
                _context.Add(menuaccess);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["MenuGroupList"] = new SelectList(_context.ms_menugp.ToList(), "MnugrpId", "MnugrpNme");
            return View(menuaccess);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuaccess = await _context.ms_menuaccess.FindAsync(id);
            if (menuaccess == null)
            {
                return NotFound();
            }

            ViewData["MenuGroupList"] = new SelectList(_context.ms_menugp.ToList(), "MnugrpId", "MnugrpNme");
            return View(menuaccess);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccessId,MnugrpId,BtnNme")] Menuaccess menuaccess)
        {
            if (id != menuaccess.AccessId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    menuaccess.RevDteTime = DateTime.Now;
                    _context.Update(menuaccess);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuaccessExists(menuaccess.AccessId))
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
            ViewData["MenuGroupList"] = new SelectList(_context.ms_menugp.ToList(), "MnugrpId", "MnugrpNme");
            return View(menuaccess);
        }

        // GET: Menuaccesses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuaccess = await _context.ms_menuaccess
                .FirstOrDefaultAsync(m => m.AccessId == id);
            if (menuaccess == null)
            {
                return NotFound();
            }

            return View(menuaccess);
        }

        // POST: Menuaccesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuaccess = await _context.ms_menuaccess.FindAsync(id);
            if (menuaccess != null)
            {
                _context.ms_menuaccess.Remove(menuaccess);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuaccessExists(int id)
        {
            return _context.ms_menuaccess.Any(e => e.AccessId == id);
        }
    }
}
