using CMPG_323_Project2.Data;
using CMPG_323_Project2.Models;
using CMPG_323_Project2.Repository;
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

        private readonly IGenericRepository<MetaDatum> _meta;

        public MetaDataController(IGenericRepository<MetaDatum> meta, CMPG_DBContext DBContext)
        {
            _meta = meta;
        }
        public IActionResult Index()
        {

            List<MetaDatum> metadatas=_meta.GetAll();
            return View(metadatas);
        }
        public IActionResult Details(int Id)
        {
            MetaDatum metaDatum=_meta.GetById(Id);
            return View(metaDatum);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            
            List<MetaDatum> metaDatum=_meta.Find("SELECT *  FROM MetaData WHERE Photo_ID = " + Id + " ");

            return View(metaDatum[0]);
        }

        //[HttpGet]
        //public IActionResult Search()
        //{
        //    return View();
        //}

        [HttpPost]
        public IActionResult Search(string colNeed,string searchNeed)
        {
            List<MetaDatum> metadatas=_meta.Find("SELECT *  FROM MetaData WHERE " + colNeed + " = '" + searchNeed + "'");
            //ViewBag.Message = searchNeed;
            
            //List<MetaDatum> returnMeta = (List<MetaDatum>)(from m in metadatas
            //                             where colNeed == searchNeed
            //                             select new MetaDatum { MetadataId=m.MetadataId,Tags=m.Tags,CapturedBy=m.CapturedBy, CapturedDate =m.CapturedDate
            //                             , Geolocation = m.Geolocation, PhotoId =m.PhotoId});
            return View(metadatas);

        }

        public IActionResult Edit(MetaDatum metaDatum)
        {
            int v = metaDatum.MetadataId;
            _meta.Update(metaDatum);
            //_DBContext.Attach(metaDatum);
            //_DBContext.Entry(metaDatum).State = EntityState.Modified;
            //_DBContext.SaveChanges();

            return RedirectToAction("index", "UserPhoto");
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
                auid=_meta.GetAll().Max(auId => auId.MetadataId);
            }
            catch (Exception e)
            {
                auid = 0;
            }


            int auNo;
            int.TryParse(auid.ToString(), out auNo);
            if (auNo >= 0)
            {
                auNo++;
                auid = auNo;
            }

            metadata.MetadataId = auid;
            _meta.Insert(metadata);
            //_DBContext.Attach(metadata);
            //_DBContext.Entry(metadata).State = EntityState.Added;
            //_DBContext.SaveChanges();


            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            MetaDatum accountuser=_meta.GetById(Id);
            return View(accountuser);
        }
        [HttpPost]
        public IActionResult Delete(MetaDatum metadata)
        {
            _meta.Delete(metadata.MetadataId);
            //_DBContext.Attach(metadata);
            //_DBContext.Entry(metadata).State = EntityState.Deleted;
            //_DBContext.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
