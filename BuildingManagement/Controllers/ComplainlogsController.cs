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
using Microsoft.CodeAnalysis;

namespace BuildingManagement.Controllers
{
    [Authorize]
    public class ComplainlogsController : Controller
    {
        private readonly BuildingDbContext _context;

        public ComplainlogsController(BuildingDbContext context)
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

            var list = await _context.pms_complainlog.ToListAsync();

            foreach (var data in list)
            {
                data.ComplaintCatg = _context.ms_complaintcatg.Where(c => c.CmpCatgId == data.CmpCatgId).Select(c => c.CplCatCde).FirstOrDefault() ?? "";
                data.Tenant = _context.ms_tenant.Where(c => c.TenantId == data.TenantId).Select(c => c.TenantNme).FirstOrDefault() ?? "";
            }            
            return View(list);
        }

        public async Task<IActionResult> Details(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var complainlog = await _context.pms_complainlog
                .FirstOrDefaultAsync(m => m.CmpId == id);

            if (complainlog == null)
            {
                return NotFound();
            }
            complainlog.ComplaintCatg =
                  _context.ms_complaintcatg
                  .Where(c => c.CmpCatgId == complainlog.CmpCatgId)
                  .Select(c => c.CplCatCde)
                  .FirstOrDefault() ?? "";
            complainlog.Tenant =
                   _context.ms_tenant
                   .Where(t => t.TenantId == complainlog.TenantId)
                   .Select(t => t.TenantNme)
                   .FirstOrDefault() ?? "";
            complainlog.Company =
                _context.ms_company
                .Where(e => e.CmpyId == complainlog.CmpyId)
                .Select(e => e.CmpyNme)
                .FirstOrDefault() ?? "";

            complainlog.User =
                _context.ms_user
                .Where(u => u.UserId == complainlog.UserId)
                .Select(u => u.UserNme)
                .FirstOrDefault() ?? "";


            return View(complainlog);
        }

        public IActionResult Create()
        {
            SetLayOutData();
            ViewData["TenantList"] = new SelectList(_context.ms_tenant.ToList(), "TenantId", "TenantNme");
            ViewData["ComplaintCategoryList"] = new SelectList(_context.ms_complaintcatg.ToList(), "CmpCatgId", "CplCatCde");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CmpCatgId,TenantId,CmpDesc,CmpImgFile,Priority,ResolveFlg")] Complainlog complainlog)

        {
            SetLayOutData();
            if (ModelState.IsValid)
            {
                complainlog.CmpDteTime = DateTime.Now; // but not in edit
                complainlog.CmpyId = GetCmpyId();
                complainlog.UserId = GetUserId();
                complainlog.RevDteTime = DateTime.Now;

                var generatedId = complainlog.CmpId;
                try
                {
                    if (complainlog.CmpImgFile != null)
                    {
                        if (complainlog.CmpImgFile.Length > 0 && complainlog.CmpImgFile.Length < 12345678)
                        {
                            // change image to byte[]
                            using var image = SixLabors.ImageSharp.Image.Load(complainlog.CmpImgFile.OpenReadStream());
                            using var memoryStream = new MemoryStream();

                            complainlog.CmpImgFile.CopyTo(memoryStream);
                            var byteImage = memoryStream.ToArray();
                            complainlog.CmpImg = byteImage;
                        }
                        else
                        {
                            ViewBag.Msg = "Image size needs to be less than 1MB.";
                            return View(complainlog);
                        }
                    }
                    _context.Add(complainlog);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch
                {
                    ViewBag.Msg = "Choosed file is not an image type.";
                    return View(complainlog);
                }
            }

            return View(complainlog);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var complainlog = await _context.pms_complainlog.FindAsync(id);
            if (complainlog == null)

            {
                return NotFound();
            }
            ViewData["TenantList"] = new SelectList(_context.ms_tenant.ToList(), "TenantId", "TenantNme");
            ViewData["ComplaintCategoryList"] = new SelectList(_context.ms_complaintcatg.ToList(), "CmpCatgId", "CplCatCde");
            return View(complainlog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CmpId,CmpCatgId,TenantId,CmpDesc,CmpImgFile,Priority,ResolveDesc,ResolveFlg,ResolveBy,ResolveImg")] Complainlog complainlog)

        {
            SetLayOutData();
            if (id != complainlog.CmpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    complainlog.CmpDteTime = DateTime.Now;
                    complainlog.CmpyId = GetCmpyId(); //default
                    complainlog.UserId = GetUserId(); //default
                    complainlog.RevDteTime = DateTime.Now;
                    using var memoryStream = new MemoryStream();

                    complainlog.CmpImgFile.CopyTo(memoryStream);
                    var byteImage = memoryStream.ToArray();
                    complainlog.CmpImg = byteImage;
                    _context.Update(complainlog);
                    await _context.SaveChangesAsync();

                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplainlogExists(complainlog.CmpId))
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
            return View(complainlog);
        }

        public IActionResult Resolve(int? id)
        {
            SetLayOutData();

            var complainlog = _context.pms_complainlog.Where(c => c.CmpId == id).FirstOrDefault();
            ViewData["TenantList"] = new SelectList(_context.ms_tenant.ToList(), "TenantId", "TenantNme");
            ViewData["ComplaintCategoryList"] = new SelectList(_context.ms_complaintcatg.ToList(), "CmpCatgId", "CplCatCde");
            return View(complainlog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Resolve(int id,[Bind("CmpId,CmpCatgId,TenantId,CmpDesc,CmpImg,Priority,ResolveBy,ResolveImgFile,ResolveDesc,ResolveFlg")] Complainlog resolve)
        {
            SetLayOutData();
            if (id != resolve.CmpId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                resolve.CmpDteTime = DateTime.Now;
                resolve.CmpyId = GetCmpyId();
                resolve.UserId = GetUserId();
                resolve.RevDteTime = DateTime.Now;
                var generatedId = resolve.CmpId;
                try
                {
                    if (resolve.ResolveImgFile != null)
                    {
                        if (resolve.ResolveImgFile.Length > 0 && resolve.ResolveImgFile.Length < 12345678)
                        {
                            using var image = SixLabors.ImageSharp.Image.Load(resolve.ResolveImgFile.OpenReadStream());
                            using var memoryStream = new MemoryStream();

                            resolve.ResolveImgFile.CopyTo(memoryStream);
                            var byteImage = memoryStream.ToArray();
                            resolve.ResolveImg = byteImage;
                        }
                        else
                        {
                            ViewBag.Msg = "Image size needs to be less than 1MB.";
                            return View(resolve);
                        }
                    }
                    _context.Update(resolve);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch
                {
                    ViewBag.Msg = "Choosed file is not an image type.";
                    return View(resolve);
                }
            }

            return View(resolve);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complainlog = await _context.pms_complainlog
                .FirstOrDefaultAsync(m => m.CmpId == id);
            if (complainlog == null)
            {
                return NotFound();
            }
            complainlog.Tenant =
              _context.ms_tenant
              .Where(c => c.TenantId == complainlog.TenantId)
              .Select(c => c.TenantNme)
              .FirstOrDefault() ?? "";
            complainlog.ComplaintCatg =
              _context.ms_complaintcatg
              .Where(c => c.CmpCatgId == complainlog.CmpCatgId)
              .Select(c => c.CplCatCde)
              .FirstOrDefault() ?? "";

            return View(complainlog);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complainlog = await _context.pms_complainlog.FindAsync(id);
            if (complainlog != null)
            {
                _context.pms_complainlog.Remove(complainlog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplainlogExists(int id)
        {
            return _context.pms_complainlog.Any(c => c.CmpId == id);
        }
        #endregion


        #region // Common methods //
        protected void SetLayOutData()
        {
            var userCde = HttpContext.User.Claims.FirstOrDefault()?.Value; // format for to claim usercde

            var userName = _context.ms_user.Where(u => u.UserCde == userCde).Select(u => u.UserNme).FirstOrDefault();

            ViewBag.UserName = userName;

        }

        private bool ValidateImageExtension(string fileName)
        {
            var allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(fileName).ToLower();
            return allowedExtensions.Contains(extension);
        }
    }

    #endregion
}
