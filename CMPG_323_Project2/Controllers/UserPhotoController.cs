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
using CMPG_323_Project2.Repository;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using CMPG_323_Project2.Logic;

namespace CMPG_323_Project2.Controllers
{
    public class UserPhotoController : Controller
    {
        //private readonly CMPG_DBContext _DBContext;
        private readonly IGenericRepository<Photo> _photo;
        private readonly IGenericRepository<AspNetUser> _user;
        private readonly IGenericRepository<UserPhoto> _link;
        private readonly IFileManagerLogic _fileManagerLogic;
        // ;
        private readonly UserManager<AppUser> _UserManager;
        public UserPhotoController(IGenericRepository<Photo> photo,
            IGenericRepository<AspNetUser> user, 
            IGenericRepository<UserPhoto> link
            , UserManager<AppUser> UserManager,
            IFileManagerLogic fileManagerLogic)
        {
            _photo=photo;
            _link=link;
            _user=user;
            _UserManager=UserManager;
            _fileManagerLogic = fileManagerLogic;

        }
        public IActionResult Index()
        {


            var usid=_UserManager.GetUserId(HttpContext.User);
            List<AspNetUser> accountusers=_user.GetAll();
            List<UserPhoto> user_image_link=_link.Find("select * from(select *, row_number() over(partition by User_ID, Photo_ID, Recepient_User_ID  order by Photo_ID) as row_number from[dbo].UserPhoto) as rows where row_number = 1");
            List<Photo> images=_photo.GetAll();
            var userViewModelImages=from uil in user_image_link
                                      from u in accountusers
                                      from i in images
                                      where u.Id == usid
                                      && (uil.RecepientUserId==u.Id)
                                      where uil.PhotoId==i.PhotoId
                                      select new UserViewModelPhoto { userVm=u, photoVm=i };
            return View(userViewModelImages);

        }
        public IActionResult MyPhoto()
        {


            var usid=_UserManager.GetUserId(HttpContext.User);
            List<AspNetUser> accountusers=_user.GetAll();
            List<UserPhoto> user_image_link = _link.Find("select * from(select *, row_number() over(partition by User_ID, Photo_ID order by Photo_ID) as row_number from[dbo].UserPhoto) as rows where row_number = 1");
            List<Photo> images = _photo.GetAll(); 
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


            Photo photo=_photo.GetById(Id);
                //_DBContext.Photos.Where(p => p.PhotoId == Id).FirstOrDefault();
            AspNetUser aspUser=_user.GetById(_UserManager.GetUserId(HttpContext.User));
                //_DBContext.AspNetUsers.Where(p => p.Id == _UserManager.GetUserId(HttpContext.User)).FirstOrDefault();
            userViewModelPhoto.photoVm = photo;
            userViewModelPhoto.userVm = aspUser;
            return View(userViewModelPhoto);
        }
        [HttpPost]
        public IActionResult ShareTo(int PId, String Email)
        {
            //AspNetUser aspUser = _user.GetById(Email);

            AspNetUser aspUser = _user.GetAll().Where(p => p.Email == Email).FirstOrDefault();
            if (aspUser == null)
            {
                ViewBag.Message = "userNF";

                return View();

            }
            else
            {
                UserPhoto userPhoto = new UserPhoto();


                int auid = 0;
                try
                {
                    auid=_link.GetAll().Max(auId => auId.ShareId);
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
                AspNetUser currentUser=_user.GetById(_UserManager.GetUserId(HttpContext.User));
                userPhoto.ShareId = auid;

                userPhoto.RecepientUserId = aspUser.Id;


                userPhoto.UserId = currentUser.Id;
                userPhoto.PhotoId = PId;
                _link.Insert(userPhoto);



                return RedirectToAction("index");



            }
        }

        public IActionResult SharedWith()
        {//I recieved form other people


            var usid = _UserManager.GetUserId(HttpContext.User);
            List<AspNetUser> accountusers=_user.GetAll();
            List<UserPhoto> user_image_link=_link.GetAll();
            List<Photo> images=_photo.GetAll();
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
            public IActionResult PhotoUserAccess()
            {//I shared to other people


                var usid=_UserManager.GetUserId(HttpContext.User);
                List<AspNetUser> accountusers=_user.GetAll();
                List<UserPhoto> user_image_link=_link.GetAll();
                List<Photo> images=_photo.GetAll();
                var userViewModelImages = from uil in user_image_link
                                          from u in accountusers
                                          from sender in accountusers
                                          from i in images
                                          where sender.Id == uil.UserId
                                          && uil.UserId == usid
                                          && uil.RecepientUserId != uil.UserId
                                          where u.Id == usid
                                          where uil.PhotoId == i.PhotoId
                                          select new UserViewModelPhoto { userVm = sender, userPhotoVm =uil, photoVm = i };
                return View(userViewModelImages);
            }
        //[HttpGet("/UserPhoto/Delete/{user_sender}&{user_recieve}&{PId}")]
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            UserViewModelPhoto reuren = new UserViewModelPhoto();
               UserPhoto Likd=_link.GetById(Id);
            AspNetUser sender=_user.GetById(Likd.UserId);
            AspNetUser reciever=_user.GetById(Likd.RecepientUserId);
            Photo Ph=_photo.GetById(Likd.PhotoId);
            Likd.User=sender;
            Likd.RecepientUser=reciever;
            reuren.photoVm=Ph;
            reuren.userPhotoVm=Likd;
           
            return View(reuren);
        }
        [HttpPost]
        public IActionResult Delete(UserViewModelPhoto album)
        {
            _link.Delete(album.userPhotoVm.ShareId);
            return RedirectToAction("index");
        }

        public async Task<IActionResult> Download(int id)
        {
            var result = await _fileManagerLogic.GetData(id.ToString());
            return File(result, "application/octet-stream");
        }


    }
}
