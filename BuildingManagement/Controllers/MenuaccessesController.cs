using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BuildingManagement.Data;
using BuildingManagement.Models;

namespace BuildingManagement.Controllers
{
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
            return View(await _context.ms_menuaccess.ToListAsync());
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

            return View(menuaccess);
        }

        // GET: Menuaccesses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Menuaccesses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccessId,MnugrpId,BtnNme,RevDteTime")] Menuaccess menuaccess)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuaccess);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menuaccess);
        }

        // GET: Menuaccesses/Edit/5
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
            return View(menuaccess);
        }

        // POST: Menuaccesses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccessId,MnugrpId,BtnNme,RevDteTime")] Menuaccess menuaccess)
        {
            if (id != menuaccess.AccessId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
