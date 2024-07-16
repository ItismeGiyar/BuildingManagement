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
using Microsoft.Build.Framework;
using Microsoft.VisualBasic;

namespace BuildingManagement.Controllers
{

    [Authorize]
    public class BillledgersController : Controller
    {
        private readonly BuildingDbContext _context;

        public BillledgersController(BuildingDbContext context)
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
        private List<Billledger> bills = new List<Billledger>
		{
			new Billledger { DueDate = DateTime.Today.AddDays(-1), BillAmt = 100, PaidAmt= 100 },
	        new Billledger { DueDate = DateTime.Today.AddDays(1), BillAmt = 200, PaidAmt = 200 },
	        new Billledger { DueDate = DateTime.Today, BillAmt = 300, PaidAmt = 300 },



        };
		


		public async Task<IActionResult> Index(int tenantId,int billitemId,DateTime trandate,decimal? paidAmt)
        {
            SetLayOutData();
            ViewData["TenantList"] = new SelectList(_context.ms_tenant.ToList(), "TenantId", "TenantNme");
            ViewData["BillitemList"] = new SelectList(_context.ms_billitem.ToList(), "BItemID", "BItemDesc");
            ViewData["TranDate"] = trandate;
            ViewData["PaidAmount"] = paidAmt;






            
            var list = await _context.pms_billledger.ToListAsync();
            foreach (var data in list)
            {
                data.Billitem = _context.ms_billitem.Where(rt => rt.BItemID == data.BItemID).Select(rt => rt.BItemDesc).FirstOrDefault() ?? "";
                data.Tenant = _context.ms_tenant.Where(t => t.TenantId == data.TenantId).Select(t => t.TenantNme).FirstOrDefault() ?? "";
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

            var billledger = await _context.pms_billledger
                .FirstOrDefaultAsync(m => m.BillId == id);
            if (billledger == null)
            {
                return NotFound();
            }
			



			billledger.GeneratedDte = DateTime.Now;
           

            billledger.Billitem =
              _context.ms_billitem
              .Where(c => c.BItemID == billledger.BItemID)
              .Select(c => c.BItemDesc)
              .FirstOrDefault() ?? "";

            billledger.Tenant =
                _context.ms_tenant
                .Where(u => u.TenantId == billledger.TenantId)
                .Select(u => u.TenantNme)
                .FirstOrDefault() ?? "";
            billledger.Company =
               _context.ms_company
               .Where(c => c.CmpyId == billledger.CmpyId)
               .Select(c => c.CmpyNme)
               .FirstOrDefault() ?? "";

            billledger.User =
                _context.ms_user
                .Where(u => u.UserId == billledger.UserId)
                .Select(u => u.UserNme)
                .FirstOrDefault() ?? "";

            return View(billledger);
        }


        public IActionResult Create()
        {
            SetLayOutData();
            ViewData["TenantList"] = new SelectList(_context.ms_tenant.ToList(), "TenantId", "TenantNme");
            ViewData["BillitemList"] = new SelectList(_context.ms_billitem.ToList(), "BItemID", "BItemDesc");

            var billLedger = new Billledger()
            {


                TranDte = DateTime.Now,
                DueDate = DateTime.Now.AddDays(7),
                PaidAmt = 0
            };

            

            return View(billLedger);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillNo,TranDte,TenantId,BItemID,BItemDesc,BillAmt,PaidAmt,PayDte,GeneratedDte,Remark,DueDate")] Billledger billledger)
        {
            SetLayOutData();
            if (ModelState.IsValid)
            {
                if(billledger.PaidAmt > 0)
                {
                    billledger.PayDte = DateTime.Now;
                }
                else
                {
                    billledger.PayDte = null;
                }
                billledger.GeneratedDte = DateTime.Now;
                billledger.CmpyId = GetCmpyId();
                billledger.UserId = GetUserId();
                billledger.RevDteTime = DateTime.Now;
                _context.Add(billledger);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billledger);
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var billledger = await _context.pms_billledger.FindAsync(id);
            if (billledger == null)
            {
                return NotFound();
            }
            ViewData["TenantList"] = new SelectList(_context.ms_tenant.ToList(), "TenantId", "TenantNme");
            ViewData["BillitemList"] = new SelectList(_context.ms_billitem.ToList(), "BItemID", "BItemDesc");
            var billLedger = new Billledger()
            {


                PaidAmt = 0
            };


            return View(billledger);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BillId,BillNo,TranDte,TenantId,BItemID,BItemDesc,BillAmt,PaidAmt,PayDte,GeneratedDte,Remark,DueDate")] Billledger billledger)
        {
            SetLayOutData();
            if (id != billledger.BillId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (billledger.PaidAmt > 0)
                    {
                        billledger.PayDte = DateTime.Now;
                    }
                    else
                    {
                        billledger.PayDte = null;
                    }
                    billledger.GeneratedDte = DateTime.Now;
                    billledger.CmpyId = GetCmpyId();
                    billledger.UserId = GetUserId();
                    billledger.RevDteTime = DateTime.Now;
                    _context.Update(billledger);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillledgerExists(billledger.BillId))
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
            return View(billledger);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var billledger = await _context.pms_billledger
                .FirstOrDefaultAsync(m => m.BillId == id);
            if (billledger == null)
            {
                return NotFound();
            }
            billledger.Billitem =
             _context.ms_billitem
             .Where(c => c.BItemID == billledger.BItemID)
             .Select(c => c.BItemDesc)
             .FirstOrDefault() ?? "";

            billledger.Tenant =
                _context.ms_tenant
                .Where(u => u.TenantId == billledger.TenantId)
                .Select(u => u.TenantNme)
                .FirstOrDefault() ?? "";
            billledger.Company =
               _context.ms_company
               .Where(c => c.CmpyId == billledger.CmpyId)
               .Select(c => c.CmpyNme)
               .FirstOrDefault() ?? "";

            billledger.User =
                _context.ms_user
                .Where(u => u.UserId == billledger.UserId)
                .Select(u => u.UserNme)
                .FirstOrDefault() ?? "";

            return View(billledger);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SetLayOutData();
            var billledger = await _context.pms_billledger.FindAsync(id);
            if (billledger != null)
            {
                _context.pms_billledger.Remove(billledger);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillledgerExists(int id)
        {
            return _context.pms_billledger.Any(e => e.BillId == id);
        }


		#endregion


		#region // Other methods //

        public async Task<decimal> ShowBillAmount(int billItemId)
        {
            var billAmt = await _context.ms_billitem
                .Where(b => b.BItemID == billItemId)
                .Select(b => b.FixChrgAmt)
                .FirstOrDefaultAsync();

            return billAmt;
        }

		#endregion


		#region //Common Methods //
		protected void SetLayOutData()
        {
            var userCde = HttpContext.User.Claims.FirstOrDefault()?.Value; // format for to claim usercde

            var userName = _context.ms_user.Where(u => u.UserCde == userCde).Select(u => u.UserNme).FirstOrDefault();

            ViewBag.UserName = userName;

        }
        


        #endregion
       /* public string GenerateAutoBillNo()
        {
            var userCde = HttpContext.User.Claims.FirstOrDefault()?.Value;
            if (string.IsNullOrEmpty(userCde))
                return "";

            var UPOS = _context.ms_user
                .Join(_context.ms_user,
                    user => user.UserId,
                    userPOS => userPOS.UserId,
                    (user, userPOS) => new
                    {
                        user.UserCde,
                        POSId = userPOS.UserId
                    })
                .FirstOrDefault(u => u.UserCde == userCde);

            if (UPOS == null)
                return "";

            var autoNumber = _context.pms_autonumber.FirstOrDefault(pos => pos.AutoNoId == UPOS.POSId);
            if (autoNumber == null)
                return "";

            // Main method of this function which generates number								
            var generateNo = (autoNumber.LastUsedNo + 1).ToString();
            if (autoNumber.ZeroLeading)
            {
                var totalWidth = autoNumber.RunningNo - autoNumber.BillPrefix.Length - generateNo.Length;
                string paddedString = new string('0', totalWidth) + generateNo;
                return autoNumber.BillPrefix + paddedString;
            }
            else
            {
                return autoNumber.BillPrefix + generateNo;
            }
        }*/






    }
}
