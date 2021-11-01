using CMPG_323_Project2.Data;
using CMPG_323_Project2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMPG_323_Project2.Controllers
{
    public class MetaDataController : Controller
    {

        private readonly CMPG_DBContext _DBContext;
        public MetaDataController(CMPG_DBContext DBContext)
        {
            _DBContext = DBContext;
        }
        public IActionResult Index()
        {
            List<MetaDatum> metadatas = _DBContext.MetaData.ToList();
            return View(metadatas);
        }
        public IActionResult Details(int Id)
        {
            MetaDatum metaDatum = _DBContext.MetaData.Where(p => p.PhotoId == Id).FirstOrDefault();
            return View(metaDatum);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            MetaDatum metaDatum = _DBContext.MetaData.Where(p => p.PhotoId == Id).FirstOrDefault();
            return View(metaDatum);
        }
        [HttpPost]
        public IActionResult Edit(MetaDatum metaDatum)
        {
            _DBContext.Attach(metaDatum);
            _DBContext.Entry(metaDatum).State = EntityState.Modified;
            _DBContext.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
