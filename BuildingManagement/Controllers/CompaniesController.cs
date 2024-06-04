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
    public class CompaniesController : Controller
    {
        private readonly BuildingDbContext _context;

        public CompaniesController(BuildingDbContext context)
        {
            _context = context;
        }
        #region // Main methods //
      
        public async Task<IActionResult> Index()
        {
            SetLayOutData();
            return View(await _context.ms_company.ToListAsync());
        }

      
        public async Task<IActionResult> Details(short? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.ms_company
                .FirstOrDefaultAsync(m => m.CmpyId == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            SetLayOutData();
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CmpyNme,Address")] Company company)
        {
            SetLayOutData();
            if (ModelState.IsValid)
            {
                
                company.RevDteTime = DateTime.Now;
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

      
        public async Task<IActionResult> Edit(short? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.ms_company.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("CmpyId,CmpyNme,Address")] Company company)
        {
            SetLayOutData();
            if (id != company.CmpyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    company.RevDteTime = DateTime.Now;
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.CmpyId))
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
            return View(company);
        }

     
        public async Task<IActionResult> Delete(short? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.ms_company
                .FirstOrDefaultAsync(m => m.CmpyId == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            SetLayOutData();
            var company = await _context.ms_company.FindAsync(id);
            if (company != null)
            {
                _context.ms_company.Remove(company);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(short id)
        {
            return _context.ms_company.Any(e => e.CmpyId == id);
        }
        #endregion

        #region //common methods//
        protected void SetLayOutData()
        {
            var userCde = HttpContext.User.Claims.FirstOrDefault()?.Value; // format for to claim usercde

            var userName = _context.ms_user.Where(u => u.UserCde == userCde).Select(u => u.UserNme).FirstOrDefault();

            ViewBag.UserName = userName;

        }
        #endregion
    }
}
