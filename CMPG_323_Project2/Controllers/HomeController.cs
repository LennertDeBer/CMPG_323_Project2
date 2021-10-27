using CMPG_323_Project2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CMPG_323_Project2.Areas.Identity.Data;
using System.Security.Claims;

namespace CMPG_323_Project2.Controllers
{
    public class HomeController : Controller
    {
   
        private readonly UserManager<AppUser> _UserManager;
       
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> UserManager)
        {
            _logger = logger;
            _UserManager = UserManager;
        }
       
        public IActionResult Index()
        {
            //ViewBag.userid = _UserManager.GetUserId(HttpContext.User);
            try
            {
                var usid = _UserManager.GetUserId(HttpContext.User);
            }
            catch(Exception e)
            {

            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
