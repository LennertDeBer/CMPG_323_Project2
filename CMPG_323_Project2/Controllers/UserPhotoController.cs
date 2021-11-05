using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMPG_323_Project2.Data;
using CMPG_323_Project2.Models;
using CMPG_323_Project2.ViewModel;
using Microsoft.AspNetCore.Identity;
using CMPG_323_Project2.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace CMPG_323_Project2.Controllers
{
    public class UserPhotoController : Controller
    {
        private readonly CMPG_DBContext _DBContext;
        private readonly UserManager<AppUser> _UserManager;
        public UserPhotoController(CMPG_DBContext DBContext, UserManager<AppUser> UserManager)
        {
            _DBContext = DBContext;
            _UserManager = UserManager;
        }
        public IActionResult Index()
        {


            var usid = _UserManager.GetUserId(HttpContext.User);
            List<AspNetUser> accountusers = _DBContext.AspNetUsers.ToList();
            List<UserPhoto> user_image_link = _DBContext.UserPhotos.ToList();
            List<Photo> images = _DBContext.Photos.ToList();
            var userViewModelImages = from uil in user_image_link
                                      from u in accountusers
                                      from i in images
                                      where u.Id == usid
                                      && (uil.UserId == u.Id
                                      || uil.RecepientUserId == u.Id)
                                      where uil.PhotoId == i.PhotoId
                                      select new UserViewModelPhoto { userVm = u, photoVm = i };
            return View(userViewModelImages);

        }
        public IActionResult MyPhoto()
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
        [HttpGet]
        public IActionResult ShareTo(int Id)
        {
            UserViewModelPhoto userViewModelPhoto = new UserViewModelPhoto();
            Photo photo = _DBContext.Photos.Where(p => p.PhotoId == Id).FirstOrDefault();
            AspNetUser aspUser = _DBContext.AspNetUsers.Where(p => p.Id == _UserManager.GetUserId(HttpContext.User)).FirstOrDefault();
            userViewModelPhoto.photoVm = photo;
            userViewModelPhoto.userVm = aspUser;
            return View(userViewModelPhoto);
        }
        [HttpPost]
        public IActionResult ShareTo(int PId,String Email)
        {
            AspNetUser aspUser = _DBContext.AspNetUsers.Where(p => p.Email == Email).FirstOrDefault();
            if(aspUser==null)
            {
                ViewBag.Message = "userNF";

                return View();

            }
            else {
                UserPhoto userPhoto = new UserPhoto();
               
                
                int auid = 0;
                try
                {
                    auid = _DBContext.UserPhotos.Max(auId => auId.ShareId);
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
                AspNetUser currentUser = _DBContext.AspNetUsers.Where(p => p.Id == _UserManager.GetUserId(HttpContext.User)).FirstOrDefault();
                userPhoto.ShareId = auid;
                userPhoto.RecepientUserId= aspUser.Id;


                userPhoto.UserId = currentUser.Id;
                userPhoto.PhotoId = PId;
                _DBContext.Attach(userPhoto);
                _DBContext.Entry(userPhoto).State = EntityState.Added;
                _DBContext.SaveChanges();


                return RedirectToAction("index");


                
            }
        }

        public IActionResult SharedWith()
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

    }
}
