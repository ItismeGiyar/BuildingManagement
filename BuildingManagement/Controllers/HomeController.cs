using BuildingManagement.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using BuildingManagement.Data;

namespace BuildingManagement.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BuildingDbContext _context;

        public HomeController(ILogger<HomeController> logger, BuildingDbContext dbContext)
        {
            _logger = logger;
            _context = dbContext;

        }

        public IActionResult Index()
        {
            SetLayOutData();

            return View();
        }

        public IActionResult Privacy()
        {
            SetLayOutData();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        protected void SetLayOutData()
        {
            var userCde = HttpContext.User.Claims.FirstOrDefault()?.Value; // format for to claim usercde

            var userName = _context.ms_user.Where(u => u.UserCde == userCde).Select(u => u.UserNme).FirstOrDefault();

            ViewBag.UserName = userName;

        }


        public IActionResult Error()
        {
            SetLayOutData();
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
