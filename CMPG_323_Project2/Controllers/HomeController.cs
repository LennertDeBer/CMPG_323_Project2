
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CMPG_323_Project2.Models;
using System.IO;

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CMPG_323_Project2.Logic;

namespace CMPG_323_Project2.Controllers
{
    public class HomeController : Controller
    {
     
            private readonly ILogger<HomeController> _logger;
            private IHostingEnvironment _env;
            private readonly IWebHostEnvironment hostEnvironment;
       
        public HomeController(ILogger<HomeController> logger, IHostingEnvironment env, IWebHostEnvironment hostEnvironment)
            {
                _logger = logger;
                _env = env;
                this.hostEnvironment = hostEnvironment;
         
            }

            public IActionResult Index() => View();
            public IActionResult SingleFile(IFormFile file)
            {
                //string wwwRootPaht = hostEnvironment.WebRootPath;
                //string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                //string extension = Path.GetExtension(file.FileName);
                //fileName = fileName + extension;

                //string pa = Path.Combine(wwwRootPaht + "/pic/", fileName);
                //using (var fileStream = new FileStream(pa, FileMode.Create, FileAccess.Write))
                //{
                //    file.CopyToAsync(fileStream);
                //}


                return RedirectToAction("Index");

            }
            //public IActionResult Index()
            //{
            //    return View();
            //}

            public IActionResult Privacy()
            {
                return View();
            }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
    //        private readonly ILogger<HomeController> _logger;
        
    //    public HomeController(ILogger<HomeController> logger)
    //    {
    //        _logger = logger;
    //}

    //    public IActionResult Index()
    //    {
    //        return View();
    //    }

    //    public IActionResult Privacy()
    //    {
    //        return View();
    //    }

    //    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //    public IActionResult Error()
    //    {
    //        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    //    }
    }
}
