using CMPG_323_Project2.Areas.Identity.Data;
using CMPG_323_Project2.Data;
using CMPG_323_Project2.Models;
using CMPG_323_Project2.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMPG_323_Project2.Controllers
{
    public class ContainController : Controller
    {

        private readonly CMPG_DBContext _DBContext;
        private readonly UserManager<AppUser> _UserManager;
        public ContainController(CMPG_DBContext DBContext, UserManager<AppUser> UserManager)
        {
            _DBContext = DBContext;
            _UserManager = UserManager;
        }

        public IActionResult Index(int Id)
        {

            List<Photo> photos = _DBContext.Photos.ToList();
            List<Contain> contains = _DBContext.Contains.ToList();
            List<Album> albums = _DBContext.Albums.ToList();
            var userViewModelImages = from c in contains
                                      from p in photos
                                      from a in albums
                                      where a.AlbumId == c.AlbumId
                                      && c.AlbumId == Id
                                      where p.PhotoId == c.PhotoId
                                      select new AlbumViewModelPhoto { albumVm = a, containVm = c, photoVm = p };
            ViewBag.AlbumId = Id;
            //if(userViewModelImages.Count()>1)
            //{ 
                return View(userViewModelImages); 
            //}
            //else
            //{
            //    return
            //}
            

        }
        private bool first = true;
        [HttpGet]
        public IActionResult AddPhoto(int IdA, int IdP)
        {
            if (first)
            {
                Photo photos = _DBContext.Photos.Where(p => p.PhotoId == IdP).FirstOrDefault();
                Contain contains = _DBContext.Contains.FirstOrDefault();
                Album albums = _DBContext.Albums.Where(p => p.AlbumId == IdA).FirstOrDefault();
                AlbumViewModelPhoto userViewModelImages = new AlbumViewModelPhoto();
                userViewModelImages.albumVm = albums;
                userViewModelImages.photoVm = photos;
                userViewModelImages.containVm = contains;
                first = false;
                return View(userViewModelImages);
            }
            else
            {
                first = true;
                return View();
            }

        }
        [HttpPost]
        public IActionResult AddPhoto(int IdA,int IdP,bool Flag)
        {

            Contain contain = new Contain();


            int auid = 0;
            try
            {
                auid = _DBContext.Contains.Max(auId => auId.ContainId);
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
          
            contain.ContainId = auid;
            contain.AlbumId=IdA;
            contain.PhotoId = IdP;



            //userPhoto.UserId = currentUser.Id;
            //userPhoto.PhotoId = PId;
            _DBContext.Attach(contain);
            _DBContext.Entry(contain).State = EntityState.Added;
            _DBContext.SaveChanges();


            return Redirect("/Contain/index/"+ IdA);
        }
        [HttpGet]
        public IActionResult RemovePhoto(int IdA,int IdP,int IdC)
        {
            Photo photos = _DBContext.Photos.Where(p => p.PhotoId == IdP).FirstOrDefault();
           Contain contains = _DBContext.Contains.Where(p => p.ContainId == IdC).FirstOrDefault();
            Album albums = _DBContext.Albums.Where(p=>p.AlbumId==IdA).FirstOrDefault();
            AlbumViewModelPhoto albumViewModelPhoto = new AlbumViewModelPhoto();
            albumViewModelPhoto.albumVm = albums;
            albumViewModelPhoto.photoVm = photos;
            albumViewModelPhoto.containVm = contains;
                //from c in contains
                //                      from p in photos
                //                      from a in albums
                //                      where a.AlbumId == c.AlbumId
                //                      && c.AlbumId == IdA
                //                      where p.PhotoId == c.PhotoId
                //                      && c.PhotoId == IdP
                //                      where c.ContainId ==IdC
                //                      select new AlbumViewModelPhoto { albumVm = a, containVm = c, photoVm = p };
            return View(albumViewModelPhoto);
        }
        [HttpGet]
        public IActionResult SelectPhoto(int AlbumId) 
        {
            var usid = _UserManager.GetUserId(HttpContext.User);
           // List<AspNetUser> accountusers = _DBContext.AspNetUsers.ToList();
            List<UserPhoto> user_image_link = _DBContext.UserPhotos.ToList();
            //List<Photo> images = _DBContext.Photos.ToList();
            //var userViewModelImages = from uil in user_image_link
            //                          from u in accountusers
            //                          from i in images
            //                          where u.Id == usid
            //                          && (uil.UserId == u.Id)
            //                          where uil.PhotoId == i.PhotoId
            //                          select new UserViewModelPhoto { userVm = u, photoVm = i };
            List<Photo> photos = _DBContext.Photos.ToList();
        List<Album> albums = _DBContext.Albums.ToList();
     
            var albumViewModelPhoto =   from p in photos
                                        from a in albums
                                        from al in user_image_link
                                        where a.AlbumId == AlbumId
                                        where al.UserId== usid
                                       where al.PhotoId==p.PhotoId

                                        select new AlbumViewModelPhoto { albumVm = a, photoVm = p };
                                       
            return View(albumViewModelPhoto);
        }
        [HttpPost]
        public IActionResult SelectPhoto(int IdA, int IdP)
        {
            return View();
        }
        [HttpPost]
        public IActionResult RemovePhoto(int IdC)
        {
            Contain contains = _DBContext.Contains.Where(p => p.ContainId == IdC).FirstOrDefault();

            _DBContext.Attach(contains);
            _DBContext.Entry(contains).State = EntityState.Deleted;
            _DBContext.SaveChanges();

            return Redirect("index");
        }
    }
}
