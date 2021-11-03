using CMPG_323_Project2.Data;
using CMPG_323_Project2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            MetaDatum metaDatum = _DBContext.MetaData.Where(p => p.MetadataId == Id).FirstOrDefault();
            return View(metaDatum);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            MetaDatum metaDatum = _DBContext.MetaData.Where(p => p.MetadataId == Id).FirstOrDefault();
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

        [HttpGet]
        public IActionResult Create(int Id)
        {
            MetaDatum metadata = new MetaDatum();
            return View(metadata);
        }
        [HttpPost]
        public IActionResult Create(MetaDatum metadata)
        {
            int auid = 0;
            try
            {
                auid = _DBContext.MetaData.Max(auId => auId.MetadataId);
            }
            catch (Exception e)
            {
                auid = 1;
            }


            int auNo;
            int.TryParse(auid.ToString(), out auNo);
            if (auNo > 0)
            {
                auNo++;
                auid = auNo;
            }

            metadata.MetadataId = auid;
            _DBContext.Attach(metadata);
            _DBContext.Entry(metadata).State = EntityState.Added;
            _DBContext.SaveChanges();


            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            MetaDatum accountuser = _DBContext.MetaData.Where(p => p.MetadataId == Id).FirstOrDefault();
            return View(accountuser);
        }
        [HttpPost]
        public IActionResult Delete(MetaDatum metadata)
        {
            _DBContext.Attach(metadata);
            _DBContext.Entry(metadata).State = EntityState.Deleted;
            _DBContext.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
