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

            List<ShareAlbum> albumuser = _DBContext.ShareAlbums.FromSqlRaw("select * from( select *, row_number() over(partition by User_ID,Recipient_User_ID, Album_ID order by Share_Album_ID) as row_number from[dbo].[Share_Album] ) as rows where row_number = 1").ToList();
            //List<ShareAlbum> albumuser = _DBContext.ShareAlbums.ToList();
            List<Album> albums = _DBContext.Albums.ToList();
            var userViewModelImages = from au in albumuser
                                      from u in accountusers
                                      from a in albums
                                      where u.Id == usid
                                      &&(au.UserId == u.Id
                                     && au.RecipientUserId == u.Id)
                                     ^ (au.UserId != usid
                                     && au.RecipientUserId == usid)
                                      
                                      where a.AlbumId == au.AlbumId
                                 
             
                                    select new UserViewModelAlbum { userVm = u, albumVm = a }
                                    ;
            //ViewBag.Awaiting = _DBContext.ShareAlbums.Where(p => p.AccessGranted == false && p.RecipientUserId == usid).Count();
            return View(userViewModelImages);
        }
        public IActionResult MyAlbum()
        {
            var usid = _UserManager.GetUserId(HttpContext.User);
            List<AspNetUser> accountusers = _DBContext.AspNetUsers.ToList();

            List<ShareAlbum> albumuser = _DBContext.ShareAlbums.FromSqlRaw("select * from( select *, row_number() over(partition by User_ID,Recipient_User_ID, Album_ID order by Share_Album_ID) as row_number from[dbo].[Share_Album] ) as rows where row_number = 1").ToList();
            //List<ShareAlbum> albumuser = _DBContext.ShareAlbums.ToList();
            List<Album> albums = _DBContext.Albums.ToList();
            var userViewModelImages = from au in albumuser
                                      from u in accountusers
                                      from a in albums
                                      where u.Id == usid
                                      && (au.UserId == u.Id
                                     && au.RecipientUserId == u.Id)
                                     

                                      where a.AlbumId == au.AlbumId


                                      select new UserViewModelAlbum { userVm = u, albumVm = a }
                                    ;
            //ViewBag.Awaiting = _DBContext.ShareAlbums.Where(p => p.AccessGranted == false && p.RecipientUserId == usid).Count();
            return View(userViewModelImages);

        }
        public IActionResult AlbumShared()
        {
            var usid = _UserManager.GetUserId(HttpContext.User);
            List<AspNetUser> accountusers = _DBContext.AspNetUsers.ToList();

            List<ShareAlbum> albumuser = _DBContext.ShareAlbums.FromSqlRaw("select * from( select *, row_number() over(partition by User_ID,Recipient_User_ID, Album_ID order by Share_Album_ID) as row_number from[dbo].[Share_Album] ) as rows where row_number = 1").ToList();
            //List<ShareAlbum> albumuser = _DBContext.ShareAlbums.ToList();
            List<Album> albums = _DBContext.Albums.ToList();
            var userViewModelImages = from au in albumuser
                                      from u in accountusers
                                      from a in albums
                                      where u.Id == usid
                                      && (au.UserId != usid
                                     && au.RecipientUserId == usid)

                                      where a.AlbumId == au.AlbumId


                                      select new UserViewModelAlbum { userVm = u, albumVm = a }
                                    ;
            //ViewBag.Awaiting = _DBContext.ShareAlbums.Where(p => p.AccessGranted == false && p.RecipientUserId == usid).Count();
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



        [HttpGet]
        public IActionResult ShareToUser(int Id)
        {
            UserViewModelAlbum userViewModelAlbum = new UserViewModelAlbum();
            Album album = _DBContext.Albums.Where(p => p.AlbumId == Id).FirstOrDefault();
            AspNetUser aspUser = _DBContext.AspNetUsers.Where(p => p.Id == _UserManager.GetUserId(HttpContext.User)).FirstOrDefault();
            userViewModelAlbum.albumVm = album;
            userViewModelAlbum.userVm = aspUser;
            return View(userViewModelAlbum);
        }
        [HttpPost]
        public IActionResult ShareToUser(int AId, String Email)
        {
            AspNetUser aspUser = _DBContext.AspNetUsers.Where(p => p.Email == Email).FirstOrDefault();
            if (aspUser == null)
            {
                ViewBag.Message = "userNF";

                return View();

            }
            else
            {
                ShareAlbum shareAlbum = new ShareAlbum();


                int auid = 0;
                try
                {
                    auid = _DBContext.ShareAlbums.Max(auId => auId.ShareAlbumId);
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
                shareAlbum.ShareAlbumId = auid;
                shareAlbum.RecipientUserId = aspUser.Id;


                shareAlbum.UserId = currentUser.Id;
                shareAlbum.AlbumId = AId;
                shareAlbum.AccessGranted = true;
        
                _DBContext.Attach(shareAlbum);
                _DBContext.Entry(shareAlbum).State = EntityState.Added;
                _DBContext.SaveChanges();


                return RedirectToAction("index");



            }
        }
    }

}

