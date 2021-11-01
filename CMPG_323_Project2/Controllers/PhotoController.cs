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
    public class PhotoController : Controller
    {

        private readonly CMPG_DBContext _DBContext;


        public PhotoController(CMPG_DBContext DBContext)
        {
            _DBContext = DBContext;
        }
        public IActionResult Index()
        {

            List<Photo> photos = _DBContext.Photos.ToList();
            return View(photos);
        }


        [HttpGet]
        public IActionResult Create(int Id)
        {
            Photo accountuser = new Photo();
            return View(accountuser);
        }
        [HttpPost]
        public IActionResult Create(Photo photo)
        {
            int auid = 0;
            try
            {
                auid = _DBContext.Photos.Max(auId => auId.PhotoId);
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

            photo.PhotoId = auid;
            _DBContext.Attach(photo);
            _DBContext.Entry(photo).State = EntityState.Added;
            _DBContext.SaveChanges();


            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Photo photo = _DBContext.Photos.Where(p => p.PhotoId == Id).FirstOrDefault();
            return View(photo);
        }
        [HttpPost]
        public IActionResult Delete(Photo photo)
        {
            _DBContext.Attach(photo);
            _DBContext.Entry(photo).State = EntityState.Deleted;
            _DBContext.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
