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
    public class PhotoMetadataController : Controller
    {
        private readonly CMPG_DBContext _DBContext;
        private readonly UserManager<AppUser> _UserManager;
        public PhotoMetadataController(CMPG_DBContext DBContext, UserManager<AppUser> UserManager)
        {
            _DBContext = DBContext;
            _UserManager = UserManager;
        }

        public IActionResult Index()
        {

            return RedirectToAction("index", "Photo");
        }
        [HttpGet]
        public IActionResult Create(int Id)
        {
            PhotoViewModelMetaData photoViewModelMetaData = new PhotoViewModelMetaData();
            return View(photoViewModelMetaData);

        }

        [HttpPost]
        public IActionResult Create(PhotoViewModelMetaData photoViewModelMeta)
        {
            

            int auid = 0;
            try
            {
                auid = _DBContext.Photos.Max(auId => auId.PhotoId);
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

            photoViewModelMeta.photoVm.PhotoId = auid;
            _DBContext.Attach(photoViewModelMeta.photoVm);
            _DBContext.Entry(photoViewModelMeta.photoVm).State = EntityState.Added;
            _DBContext.SaveChanges();


            
             auid = 0;
            try
            {
                auid = _DBContext.MetaData.Max(auId => auId.MetadataId);
            }
            catch (Exception e)
            {
                auid = 1;
            }


             auNo =0;
            int.TryParse(auid.ToString(), out auNo);
            if (auNo > 0)
            {
                auNo++;
                auid = auNo;
            }

            photoViewModelMeta.metadataVm.MetadataId = auid;

            photoViewModelMeta.metadataVm.PhotoId=photoViewModelMeta.photoVm.PhotoId;
            _DBContext.Attach(photoViewModelMeta.metadataVm);
            _DBContext.Entry(photoViewModelMeta.metadataVm).State = EntityState.Added;
            _DBContext.SaveChanges();



            auid = 0;
            try
            {
                auid = _DBContext.UserPhotos.Max(auId => auId.ShareId);
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

            photoViewModelMeta.userphotVm.ShareId = auid;
            photoViewModelMeta.userphotVm.PhotoId = photoViewModelMeta.photoVm.PhotoId;
            photoViewModelMeta.userphotVm.UserId = _UserManager.GetUserId(HttpContext.User); 
            photoViewModelMeta.userphotVm.RecepientUserId = _UserManager.GetUserId(HttpContext.User);
            _DBContext.Attach(photoViewModelMeta.userphotVm);
            _DBContext.Entry(photoViewModelMeta.userphotVm).State = EntityState.Added;
            _DBContext.SaveChanges();


            return Redirect("/UserPhoto");
        }
        private int photoToMeta(int Id)
        {
            MetaDatum md = _DBContext.MetaData.Where(p => p.PhotoId == Id).FirstOrDefault();
            return md.MetadataId;
        }

        //[HttpGet("/PhotoMetaData/Details/{id}&{urlds}")]
        /*, string urlds*/
        public IActionResult Details(int Id)
        {
            int v = photoToMeta(Id);
            PhotoViewModelMetaData pmVm = new PhotoViewModelMetaData();
            MetaDatum metaDatum = _DBContext.MetaData.Where(p => p.MetadataId == v).FirstOrDefault();
            Photo photo = _DBContext.Photos.Where(p => p.PhotoId == Id).FirstOrDefault();
            pmVm.metadataVm = metaDatum;
            pmVm.photoVm = photo;
            return View(pmVm);
          
        }
    }
}
