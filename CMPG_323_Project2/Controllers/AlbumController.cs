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
  


    public class AlbumController : Controller
    {
        private readonly CMPG_DBContext _DBContext;
        public AlbumController(CMPG_DBContext DBContext)
        {
            _DBContext=DBContext;
    }
        public IActionResult Index()
        {
            List<Album> albums=_DBContext.Albums.ToList();
            return View(albums);
        }

        public IActionResult Details(int Id)
        {
            Album album=_DBContext.Albums.Where(p => p.AlbumId==Id).FirstOrDefault();
            return View(album);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Album album=_DBContext.Albums.Where(p => p.AlbumId==Id).FirstOrDefault();
            return View(album);
        }
        [HttpPost]
        public IActionResult Edit(Album album)
        {
            _DBContext.Attach(album);
            _DBContext.Entry(album).State = EntityState.Modified;
            _DBContext.SaveChanges();

            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Create(int Id)
        {
            Album album =new Album();
            return View(album);
        }
        [HttpPost]
        public IActionResult Create(Album album)
        {
            int auid=0;
            try
            {
                auid=_DBContext.Albums.Max(auId => auId.AlbumId);
            }
            catch (Exception e)
            {
                auid=1;
            }


            int auNo;
            int.TryParse(auid.ToString(), out auNo);
            if (auNo>0)
            {
                auNo++;
                auid=auNo;
            }

            album.AlbumId=auid;
            _DBContext.Attach(album);
            _DBContext.Entry(album).State=EntityState.Added;
            _DBContext.SaveChanges();


            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Album album=_DBContext.Albums.Where(p => p.AlbumId==Id).FirstOrDefault();
            return View(album);
        }
        [HttpPost]
        public IActionResult Delete(Album album)
        {
            _DBContext.Attach(album);
            _DBContext.Entry(album).State=EntityState.Deleted;
            _DBContext.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
