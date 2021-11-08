using CMPG_323_Project2.Data;
using CMPG_323_Project2.Models;
using CMPG_323_Project2.Repository;
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
        private readonly IGenericRepository<Album> _album;
        public AlbumController(IGenericRepository<Album> album)
        {
            _album = album;
        }
        public IActionResult Index()
        {
            List<Album> albums=_album.GetAll();
            return View(albums);
        }

        public IActionResult Details(int Id)
        {
            Album album=_album.GetById(Id);
            return View(album);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Album album = _album.GetById(Id) ;
            return View(album);
        }
        [HttpPost]
        public IActionResult Edit(Album album)
        {
            _album.Update(album);

            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Create(int Id)
        {
            Album album = new Album();
            return View(album);
        }
        [HttpPost]
        public IActionResult Create(Album album)
        {
            int auid=0;
            try
            {
                auid=_album.GetAll().Max(auId => auId.AlbumId);
            }
            catch (Exception e)
            {
                auid=1;
            }


            int auNo;
            int.TryParse(auid.ToString(), out auNo);
            if (auNo > 0)
            {
                auNo++;
                auid=auNo;
            }

            album.AlbumId=auid;
            _album.Insert(album);


            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Album album = _album.GetById(Id);
            return View(album);
        }
        [HttpPost]
        public IActionResult Delete(Album album)
        {
            _album.Delete(album.AlbumId);

            return RedirectToAction("index");
        }
    }
}
