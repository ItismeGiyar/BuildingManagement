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
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace BuildingManagement.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly BuildingDbContext _context;
        private readonly EncryptDecryptService encryptDecryptService;

        public UsersController(BuildingDbContext context)
        {
            _context = context;
            encryptDecryptService = new EncryptDecryptService();

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
            return View(await _context.ms_user.ToListAsync());
        }


       
        public async Task<IActionResult> Details(int? id)
        {
            SetLayOutData();
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

  
        public IActionResult Create()
        {
            SetLayOutData();
            ViewData["Companies"] = new SelectList(_context.ms_company.ToList(), "CmpyId", "CmpyNme");
            ViewData["Positions"] = new SelectList(_context.ms_menugp.ToList(), "MnugrpId", "MnugrpNme");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("UserCde,UserNme,CmpyId,MnugrpId,Gender,Pwd,ConfirmPwd")] User user)
        {
            SetLayOutData();
            if (ModelState.IsValid)
            {
                if (user.Password == null)
                {
                    user.Password = "User@123"; //default
                    user.ConfirmPwd = "User@123"; //default
                }

                // Merge by Ko Kg
                byte[] encryptedBytes = Encoding.UTF8.GetBytes(encryptDecryptService.EncryptString(user.Password));

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

       
        public async Task<IActionResult> Edit(int? id)
        {
            SetLayOutData();
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

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserCde,UserNme,Position,CmpyId,Gender,MnugrpId,Pwd")] User user)
        {
            SetLayOutData();
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
        public async Task<IActionResult> ResetPwd(int? id)
        {
            SetLayOutData();
            try
            {
                var user = await _context.ms_user.FindAsync(id);
                if (user != null)
                {
                    string defaultPassword = "User@123";
                    byte[] encryptedBytes = Encoding.UTF8.GetBytes(encryptDecryptService.EncryptString(defaultPassword));
                    user.Pwd = encryptedBytes;
                    user.RevDteTime = DateTime.Now;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                StatusCode(500, ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ChangePwd(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.ms_user.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var changePwd = new ChangePwd() 
            {
                UserId = user.UserId,
                UserCde = user.UserCde,
                UserNme = user.UserNme
            };
            return View(changePwd);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePwd(int id, [Bind("UserId,UserCde,UserNme,OldPwd,NewPwd,ConfirmPwd")] ChangePwd changePwd)
        {
            SetLayOutData();
            if (id != changePwd.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _context.ms_user.FindAsync(changePwd.UserId);
                    string encryptedString = Encoding.UTF8.GetString(user.Pwd);
                    string decryptedPassword = encryptDecryptService.DecryptString(encryptedString);
                    if (changePwd.NewPwd == null)
                    {
                        changePwd.NewPwd = "User@123"; 
                        changePwd.ConfirmPwd = "User@123";
                    }
                    if (decryptedPassword == changePwd.OldPwd) 
                    {                        
                        if (changePwd.NewPwd == changePwd.ConfirmPwd)
                        {
                            byte[] encryptedBytes = Encoding.UTF8.GetBytes(encryptDecryptService.EncryptString(changePwd.NewPwd));
                            user.Pwd = encryptedBytes;
                            user.RevDteTime = DateTime.Now;
                            _context.Update(user);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            ModelState.AddModelError("ConfirmPwd", "New password and confirm password do not match.");
                            return View(changePwd);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("OldPwd", "Incorrect current password.");
                        return View(changePwd);
                    }
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(changePwd.UserId))
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
            return View(changePwd);
        }


      
        public async Task<IActionResult> Delete(int? id)
        {
            SetLayOutData();
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


    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SetLayOutData();
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
