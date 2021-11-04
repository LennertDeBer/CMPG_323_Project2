using CMPG_323_Project2.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMPG_323_Project2.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using CMPG_323_Project2.Models;
using CMPG_323_Project2.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace CMPG_323_Project2.Controllers
{
    public class ShareAlbumController : Controller
    {
        private readonly CMPG_DBContext _DBContext;
        private readonly UserManager<AppUser> _UserManager;
        public ShareAlbumController(CMPG_DBContext DBContext, UserManager<AppUser> UserManager)
        {
            _DBContext = DBContext;
            _UserManager = UserManager;
        }
        public IActionResult Index()
        {
            var usid = _UserManager.GetUserId(HttpContext.User);
            List<AspNetUser> accountusers = _DBContext.AspNetUsers.ToList();
            List<ShareAlbum> albumuser = _DBContext.ShareAlbums.ToList();
            List<Album> albums = _DBContext.Albums.ToList();
            var userViewModelImages = from au in albumuser
                                      from u in accountusers
                                      from a in albums
                                      where u.Id == usid
                                      && (au.UserId == u.Id
                                      || au.RecipientUserId == u.Id)
                                      where a.AlbumId == au.AlbumId
                                      select new UserViewModelAlbum { userVm = u, albumVm = a };
            return View(userViewModelImages);
        }
        public IActionResult MyAlbum()
        {
            var usid = _UserManager.GetUserId(HttpContext.User);
            List<AspNetUser> accountusers = _DBContext.AspNetUsers.ToList();
            List<UserPhoto> user_image_link = _DBContext.UserPhotos.ToList();
            List<Photo> images = _DBContext.Photos.ToList();
            var userViewModelImages = from uil in user_image_link
                                      from u in accountusers
                                      from i in images
                                      where u.Id == usid
                                      && (uil.UserId == u.Id)
                                      where uil.PhotoId == i.PhotoId
                                      select new UserViewModelPhoto { userVm = u, photoVm = i };
            return View(userViewModelImages);

        }
        public IActionResult AlbumShared()
        {
            var usid = _UserManager.GetUserId(HttpContext.User);
            List<AspNetUser> accountusers = _DBContext.AspNetUsers.ToList();
            List<UserPhoto> user_image_link = _DBContext.UserPhotos.ToList();
            List<Photo> images = _DBContext.Photos.ToList();
            var userViewModelImages = from uil in user_image_link
                                      from u in accountusers
                                      from sender in accountusers
                                      from i in images
                                      where sender.Id == uil.UserId
                                      && uil.RecepientUserId == usid
                                      && uil.RecepientUserId != uil.UserId
                                      where u.Id == usid
                                      && (uil.RecepientUserId == u.Id
                                      && uil.RecepientUserId != uil.UserId)
                                      where uil.PhotoId == i.PhotoId
                                      select new UserViewModelPhoto { userVm = sender, photoVm = i };
            return View(userViewModelImages);
        }

        [HttpGet]
        public IActionResult Create(int Id)
        {
            UserViewModelAlbum userViewModelAlbum = new UserViewModelAlbum();
            return View(userViewModelAlbum);

        }
        [HttpPost]
        public IActionResult Create(UserViewModelAlbum userViewModelAlbum)
        {


            int auid = 0;
            try
            {
                auid = _DBContext.Albums.Max(auId => auId.AlbumId);
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

            userViewModelAlbum.albumVm.AlbumId = auid;
            _DBContext.Attach(userViewModelAlbum.albumVm);
            _DBContext.Entry(userViewModelAlbum.albumVm).State = EntityState.Added;
            _DBContext.SaveChanges();


            ShareAlbum share = new ShareAlbum();

            auid = 0;
            try
            {
                auid = _DBContext.ShareAlbums.Max(auId => auId.ShareAlbumId);
            }
            catch (Exception e)
            {
                auid = 1;
            }
            auNo = 0;
            int.TryParse(auid.ToString(), out auNo);
            if (auNo > 0)
            {
                auNo++;
                auid = auNo;
            }

            share.ShareAlbumId = auid;
            share.AlbumId = userViewModelAlbum.albumVm.AlbumId;
            share.UserId = _UserManager.GetUserId(HttpContext.User);
            share.RecipientUserId = _UserManager.GetUserId(HttpContext.User);
            share.AccessGranted = true;
            _DBContext.Attach(share);
            _DBContext.Entry(share).State = EntityState.Added;
            _DBContext.SaveChanges();


            return RedirectToAction("index");
        }
    }

}

