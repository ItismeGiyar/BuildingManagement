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
using BuildingManagement.Common;
using System.Text;

namespace BuildingManagement.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly BuildingDbContext _context;
        private readonly Encrypt encrypt;


        public UsersController(BuildingDbContext context)
        {
            _context = context;
            encrypt = new Encrypt();
        }
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

        // GET: Users
        public async Task<IActionResult> Index()
        {

            return View(await _context.ms_user.ToListAsync());
        }


        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.ms_user
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            user.Company =
                _context.ms_company
                .Where(c => c.CmpyId == user.CmpyId)
                .Select(c => c.CmpyNme)
                .FirstOrDefault() ?? "";
            user.MnuGrpNme = _context.ms_menugp.Where(m => m.MnugrpId == user.MnugrpId).Select(m => m.MnugrpNme).FirstOrDefault() ?? "";
            user.Position = _context.ms_menugp.Where(mg => mg.MnugrpId == user.MnugrpId).Select(mg => mg.MnugrpNme).FirstOrDefault() ?? "";

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["Companies"] = new SelectList(_context.ms_company.ToList(), "CmpyId", "CmpyNme");
            ViewData["Positions"] = new SelectList(_context.ms_menugp.ToList(), "MnugrpId", "MnugrpNme");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("UserCde,UserNme,CmpyId,MnugrpId,Gender,Password,ConfirmPassword")] User user)
        {
            if (ModelState.IsValid)
            {
                // Merge by Ko Kg
                byte[] encryptedBytes = Encoding.UTF8.GetBytes(encrypt.EncryptString(user.ConfirmPassword));
                user.Pwd = encryptedBytes;
                user.Position = _context.ms_menugp.Where(mg => mg.MnugrpId == user.MnugrpId).Select(mg => mg.MnugrpNme).FirstOrDefault() ?? "";
                user.RevDteTime = DateTime.Now;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Companies"] = new SelectList(_context.ms_company.ToList(), "CmpyId", "CmpyNme");
            ViewData["Positions"] = new SelectList(_context.ms_menugp.ToList(), "MnugrpId", "MnugrpNme");

            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.ms_user.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["Companies"] = new SelectList(_context.ms_company.ToList(), "CmpyId", "CmpyNme");
            ViewData["Positions"] = new SelectList(_context.ms_menugp.ToList(), "MnugrpId", "MnugrpNme");

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserCde,UserNme,Position,CmpyId,Gender,MnugrpId,Pwd")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.Position = _context.ms_menugp.Where(mg => mg.MnugrpId == user.MnugrpId).Select(mg => mg.MnugrpNme).FirstOrDefault() ?? "";
                    user.RevDteTime = DateTime.Now;
                    _context.Update(user);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            ViewData["Companies"] = new SelectList(_context.ms_company.ToList(), "CmpyId", "CmpyNme");
            ViewData["Positions"] = new SelectList(_context.ms_menugp.ToList(), "MnugrpId", "MnugrpNme");

            return View(user);
        }
        public async Task<IActionResult> ChangePassword(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.ms_user.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(int id, [Bind("UserCde,UserNme,Password,ConfirmPassword")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.RevDteTime = DateTime.Now;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            return View(user);
        }


        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.ms_user
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            user.MnuGrpNme = _context.ms_menugp.Where(m => m.MnugrpId == user.MnugrpId).Select(m => m.MnugrpNme).FirstOrDefault() ?? "";
            user.Position = _context.ms_menugp.Where(mg => mg.MnugrpId == user.MnugrpId).Select(mg => mg.MnugrpNme).FirstOrDefault() ?? "";

            return View(user);
        }


        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.ms_user.FindAsync(id);
            if (user != null)
            {
                _context.ms_user.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.ms_user.Any(e => e.UserId == id);
        }
        
    }
}
