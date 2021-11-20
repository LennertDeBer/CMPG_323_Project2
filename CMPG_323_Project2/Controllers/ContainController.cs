using CMPG_323_Project2.Areas.Identity.Data;
using CMPG_323_Project2.Data;
using CMPG_323_Project2.Models;
using CMPG_323_Project2.Repository;
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

        private readonly IGenericRepository<Contain> _contain;
        private readonly IGenericRepository<Album> _album;
        private readonly IGenericRepository<Photo> _photo;
        private readonly IGenericRepository<AspNetUser> _user;
        private readonly IGenericRepository<UserPhoto> _link;
        private readonly UserManager<AppUser> _UserManager;
        public ContainController(IGenericRepository<Contain> contain, IGenericRepository<Album> album, IGenericRepository<Photo> photo, IGenericRepository<UserPhoto> userPhoto, UserManager<AppUser> UserManager, IGenericRepository<AspNetUser> user)
        {
            _contain = contain;
            _album = album;
            _photo = photo;
            _link = userPhoto;
            _UserManager = UserManager;
            _user = user;
        }

        public IActionResult Index(int Id)
        {

            List<Photo> photos = _photo.GetAll();
            List<Contain> contains = _contain.GetAll();
            List<Album> albums = _album.GetAll();
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
                Photo photos = _photo.GetById(IdP);
                Contain contains = _contain.GetAll().FirstOrDefault();
                Album albums = _album.GetById(IdA);
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
                auid = _contain.GetAll().Max(auId => auId.ContainId);
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
          
            contain.ContainId = auid;
            contain.AlbumId=IdA;
            contain.PhotoId = IdP;



            //userPhoto.UserId = currentUser.Id;
            //userPhoto.PhotoId = PId;
            _contain.Insert(contain);


            return Redirect("/Contain/index/"+ IdA);
        }
        [HttpGet]
        public IActionResult RemovePhoto(int IdA,int IdP,int IdC)
        {
            Photo photos = _photo.GetById(IdP);
           Contain contains = _contain.GetById(IdC);
            Album albums = _album.GetById(IdA);
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
            Album currentAlbum = _album.GetById(AlbumId);
  

            var usid = _UserManager.GetUserId(HttpContext.User);
            List<AspNetUser> accountusers = _user.GetAll();
            List<UserPhoto> user_image_link = _link.Find("select * from(select *, row_number() over(partition by User_ID, Photo_ID, Recepient_User_ID  order by Photo_ID) as row_number from[dbo].UserPhoto) as rows where row_number = 1");
            List<Photo> images = _photo.GetAll();
            var userViewModelImages = from uil in user_image_link
                                      from u in accountusers
                                      from i in images
                                      where u.Id == usid
                                      && (uil.RecepientUserId == u.Id)
                                      where uil.PhotoId == i.PhotoId
                                      select new AlbumViewModelPhoto { albumVm = currentAlbum, photoVm = i };
            return View(userViewModelImages); 
                                       

        }
        [HttpPost]
        public IActionResult SelectPhoto(int IdA, int IdP)
        {
            return View();
        }
       
        [HttpPost]
        public IActionResult RemovePhoto(int IdC)
        {
            Contain contains = _contain.GetById(IdC);

            _contain.Delete(contains.ContainId);

            return RedirectToAction("index",new { Id = contains.AlbumId });
        }
    }
}
