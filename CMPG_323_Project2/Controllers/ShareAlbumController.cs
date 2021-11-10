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
using CMPG_323_Project2.Repository;

namespace CMPG_323_Project2.Controllers
{
    public class ShareAlbumController : Controller
    {
        private readonly IGenericRepository<Album> _album;
        private readonly IGenericRepository<AspNetUser> _user;
        private readonly IGenericRepository<ShareAlbum> _share;
        private readonly UserManager<AppUser> _UserManager;
        public ShareAlbumController(IGenericRepository<Album> album, IGenericRepository<ShareAlbum> share, IGenericRepository<AspNetUser> user, UserManager<AppUser> userManager)
        {
            _album = album;
            _share = share;
            _user = user;
            _UserManager = userManager;
        }
        public IActionResult Index()
        {
            var usid = _UserManager.GetUserId(HttpContext.User);
            List<AspNetUser> accountusers = _user.GetAll();

            List<ShareAlbum> albumuser = _share.Find("select * from( select *, row_number() over(partition by User_ID,Recipient_User_ID, Album_ID order by Share_Album_ID) as row_number from[dbo].[Share_Album] ) as rows where row_number = 1");
            //List<ShareAlbum> albumuser = _DBContext.ShareAlbums.ToList();
            List<Album> albums = _album.Find("select * from( select *, row_number() over(partition by Album_Name order by Album_ID) as row_number from[dbo].[Album] ) as rows where row_number = 1"); ;
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
        {//my albums
            var usid = _UserManager.GetUserId(HttpContext.User);
            List<AspNetUser> accountusers = _user.GetAll();

            List<ShareAlbum> albumuser = _share.Find("select * from( select *, row_number() over(partition by User_ID, Album_ID order by Share_Album_ID) as row_number from[dbo].[Share_Album] ) as rows where row_number = 1");
            //List<ShareAlbum> albumuser = _DBContext.ShareAlbums.ToList();
            List<Album> albums = _album.Find("select * from( select *, row_number() over(partition by Album_Name order by Album_ID) as row_number from[dbo].[Album] ) as rows where row_number = 1");
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
        {//shared to me
            var usid = _UserManager.GetUserId(HttpContext.User);
            List<AspNetUser> accountusers = _user.GetAll();

            List<ShareAlbum> albumuser = _share.Find("select * from( select *, row_number() over(partition by User_ID,Recipient_User_ID, Album_ID order by Share_Album_ID) as row_number from[dbo].[Share_Album] ) as rows where row_number = 1");
            //List<ShareAlbum> albumuser = _DBContext.ShareAlbums.ToList();
            List<Album> albums = _album.Find("select * from( select *, row_number() over(partition by Album_Name order by Album_ID) as row_number from[dbo].[Album] ) as rows where row_number = 1");
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
            Album exist = _album.Find("Select * From Album Where Album_Name = '" + userViewModelAlbum.albumVm.AlbumName + "'").FirstOrDefault();
            if(exist==null)
            { 
            int auid = 0;
            try
            {
                auid =_album.GetAll().Max(auId => auId.AlbumId);
            }
            catch (Exception e)
            {
                auid = 1;
            }



            int auNo;
            int.TryParse(auid.ToString(), out auNo);
            if (auNo > 1)
            {
                auNo++;
                auid = auNo;
            }

            userViewModelAlbum.albumVm.AlbumId = auid;
            _album.Insert(userViewModelAlbum.albumVm);


            ShareAlbum share = new ShareAlbum();

            auid = 0;
            try
            {
                auid = _share.GetAll().Max(auId => auId.ShareAlbumId);
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
            _share.Insert(share);


            return RedirectToAction("index");
            }
            else 
            {
                ViewBag.Message = "Album name is already taken";
                return View();
            }
        }



        [HttpGet]
        public IActionResult ShareToUser(int Id)
        {
            UserViewModelAlbum userViewModelAlbum = new UserViewModelAlbum();
            Album album = _album.GetAll().Where(p => p.AlbumId == Id).FirstOrDefault();
            AspNetUser aspUser = _user.GetAll().Where(p => p.Id == _UserManager.GetUserId(HttpContext.User)).FirstOrDefault();
            userViewModelAlbum.albumVm = album;
            userViewModelAlbum.userVm = aspUser;
            return View(userViewModelAlbum);
        }
        [HttpPost]
        public IActionResult ShareToUser(int AId, String Email)
        {
            AspNetUser aspUser = _user.GetAll().Where(p => p.Email == Email).FirstOrDefault();
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
                    auid = _share.GetAll().Max(auId => auId.ShareAlbumId);
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
                AspNetUser currentUser = _user.GetAll().Where(p => p.Id == _UserManager.GetUserId(HttpContext.User)).FirstOrDefault();
                shareAlbum.ShareAlbumId = auid;
                shareAlbum.RecipientUserId = aspUser.Id;


                shareAlbum.UserId = currentUser.Id;
                shareAlbum.AlbumId = AId;
                shareAlbum.AccessGranted = true;

                _share.Insert(shareAlbum);


                return RedirectToAction("index");



            }
        }
    }

}

