﻿using System;
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
    public class TenantvehicalsController : Controller
    {
        private readonly BuildingDbContext _context;

        public TenantvehicalsController(BuildingDbContext context)
        {
            _context = context;
        }

        // GET: Tenantvehicals
        public async Task<IActionResult> Index()
        {
            return View(await _context.ms_tenantvehical.ToListAsync());
        }

        // GET: Tenantvehicals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantvehical = await _context.ms_tenantvehical
                .FirstOrDefaultAsync(m => m.VehId == id);
            if (tenantvehical == null)
            {
                return NotFound();
            }

            return View(tenantvehical);
        }

        // GET: Tenantvehicals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tenantvehicals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehId,TenantId,PlateNo,AllocateNo,CmpyId,UserId,RevDteTime")] Tenantvehical tenantvehical)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tenantvehical);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tenantvehical);
        }

        // GET: Tenantvehicals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantvehical = await _context.ms_tenantvehical.FindAsync(id);
            if (tenantvehical == null)
            {
                return NotFound();
            }
            return View(tenantvehical);
        }

        // POST: Tenantvehicals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehId,TenantId,PlateNo,AllocateNo,CmpyId,UserId,RevDteTime")] Tenantvehical tenantvehical)
        {
            if (id != tenantvehical.VehId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tenantvehical);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenantvehicalExists(tenantvehical.VehId))
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
            return View(tenantvehical);
        }

        // GET: Tenantvehicals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantvehical = await _context.ms_tenantvehical
                .FirstOrDefaultAsync(m => m.VehId == id);
            if (tenantvehical == null)
            {
                return NotFound();
            }

            return View(tenantvehical);
        }

        // POST: Tenantvehicals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tenantvehical = await _context.ms_tenantvehical.FindAsync(id);
            if (tenantvehical != null)
            {
                _context.ms_tenantvehical.Remove(tenantvehical);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TenantvehicalExists(int id)
        {
            return _context.ms_tenantvehical.Any(e => e.VehId == id);
        }
    }
}