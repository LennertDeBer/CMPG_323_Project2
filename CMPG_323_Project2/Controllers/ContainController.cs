using CMPG_323_Project2.Areas.Identity.Data;
using CMPG_323_Project2.Data;
using CMPG_323_Project2.Models;
using CMPG_323_Project2.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMPG_323_Project2.Controllers
{
    public class ContainController : Controller
    {

        private readonly CMPG_DBContext _DBContext;

        public ContainController(CMPG_DBContext DBContext)
        {
            _DBContext = DBContext;
        }

        public IActionResult Index(int Id)
        {
          //  Album album = _DBContext.Albums.Where(p => p.AlbumId == Id).FirstOrDefault();
            List<Photo> photos = _DBContext.Photos.ToList();
            List<Contain> contains = _DBContext.Contains.ToList();
            List<Album> albums= _DBContext.Albums.ToList();
            var userViewModelImages = from c in contains
                                      from p in photos
                                      from a in albums
                                      where a.AlbumId == c.AlbumId
                                      && a.AlbumId ==Id
                                      where p.PhotoId == c.PhotoId
                                      select new AlbumViewModelPhoto { albumVm = a, photoVm = p };
            return View(userViewModelImages);
           
        }
    }
}
