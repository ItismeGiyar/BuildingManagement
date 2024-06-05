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
    public class ConfigsController : Controller
    {
        private readonly BuildingDbContext _context;

        public ConfigsController(BuildingDbContext context)
        {
            _context = context;
        }
        #region // Main methods //
     
        public async Task<IActionResult> Index()
        {
            SetLayOutData();
            return View(await _context.ms_config.ToListAsync());
        }

      
        public async Task<IActionResult> Details(string id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var config = await _context.ms_config
                .FirstOrDefaultAsync(m => m.KeyCde == id);
            if (config == null)
            {
                return NotFound();
            }

            return View(config);
        }

    
        public IActionResult Create()
        {
            SetLayOutData();
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KeyCde,KeyValue")] Config config)
        {
            SetLayOutData();
            if (ModelState.IsValid)
            {
                _context.Add(config);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(config);
        }

        
        public async Task<IActionResult> Edit(string id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var config = await _context.ms_config.FindAsync(id);
            if (config == null)
            {
                return NotFound();
            }
            return View(config);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("KeyCde,KeyValue")] Config config)
        {
            SetLayOutData();
            if (id != config.KeyCde)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(config);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfigExists(config.KeyCde))
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
            return View(config);
        }

        // GET: Configs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var config = await _context.ms_config
                .FirstOrDefaultAsync(m => m.KeyCde == id);
            if (config == null)
            {
                return NotFound();
            }

            return View(config);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            SetLayOutData();
            var config = await _context.ms_config.FindAsync(id);
            if (config != null)
            {
                _context.ms_config.Remove(config);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfigExists(string id)
        {
            return _context.ms_config.Any(e => e.KeyCde == id);
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
