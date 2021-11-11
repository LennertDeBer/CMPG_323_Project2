using CMPG_323_Project2.Areas.Identity.Data;
using CMPG_323_Project2.Data;
using CMPG_323_Project2.Logic;
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
    public class PhotoMetadataController : Controller
    {
        //private readonly CMPG_DBContext _DBContext;
        private readonly IGenericRepository<Photo> _photo;
        private readonly IGenericRepository<MetaDatum> _metaData;
        private readonly IGenericRepository<UserPhoto> _link;
        private readonly UserManager<AppUser> _UserManager;
        private readonly IFileManagerLogic _fileManagerLogic;
        public PhotoMetadataController(IGenericRepository<Photo> photo, IGenericRepository<MetaDatum> metaData, IGenericRepository<UserPhoto> link, UserManager<AppUser> UserManager, IFileManagerLogic fileManagerLogic)
        {
            _photo=photo;
            _metaData=metaData;
            _link=link;
            _UserManager = UserManager;
            _fileManagerLogic = fileManagerLogic;
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
        public async Task<IActionResult> Create(PhotoViewModelMetaData photoViewModelMeta)
        {
            

            int auid = 0;
            try
            {
                auid=_photo.GetAll().Max(auId => auId.PhotoId);
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
            await _fileManagerLogic.Upload(photoViewModelMeta.fileModelVm, auid);

            string Url = _fileManagerLogic.read(auid.ToString());
            photoViewModelMeta.photoVm.PhotoId = auid;
            photoViewModelMeta.photoVm.PhotoUrl = Url;
           _photo.Insert(photoViewModelMeta.photoVm);
           


            
             auid = 0;
            try
            {
                auid=_metaData.GetAll().Max(auId => auId.MetadataId);
            }
            catch (Exception e)
            {
                auid = 0;
            }


             auNo =0;
            int.TryParse(auid.ToString(), out auNo);
            if (auNo >= 0)
            {
                auNo++;
                auid = auNo;
            }

            photoViewModelMeta.metadataVm.MetadataId = auid;

            photoViewModelMeta.metadataVm.PhotoId=photoViewModelMeta.photoVm.PhotoId;
            _metaData.Insert(photoViewModelMeta.metadataVm);
           



            auid = 0;
            try
            {
                auid=_link.GetAll().Max(auId => auId.ShareId);
            }
            catch (Exception e)
            {
                auid = 0;
            }


            auNo = 0;
            int.TryParse(auid.ToString(), out auNo);
            if (auNo >= 0)
            {
                auNo++;
                auid = auNo;
            }

            photoViewModelMeta.userphotVm.ShareId = auid;
            photoViewModelMeta.userphotVm.PhotoId = photoViewModelMeta.photoVm.PhotoId;
            photoViewModelMeta.userphotVm.UserId=_UserManager.GetUserId(HttpContext.User); 
            photoViewModelMeta.userphotVm.RecepientUserId=_UserManager.GetUserId(HttpContext.User);
            _link.Insert(photoViewModelMeta.userphotVm);


            return Redirect("/UserPhoto");
        }
        private int photoToMeta(int Id)
        {
            MetaDatum md=_metaData.GetById(Id);
            return md.MetadataId;
        }

        //[HttpGet("/PhotoMetaData/Details/{id}&{urlds}")]
        /*, string urlds*/
        public IActionResult Details(int Id)
        {
            int v = photoToMeta(Id);
            PhotoViewModelMetaData pmVm = new PhotoViewModelMetaData();
            MetaDatum metaDatum=_metaData.GetById(v);
            Photo photo=_photo.GetById(Id);
            pmVm.metadataVm=metaDatum;
            pmVm.photoVm=photo;
            return View(pmVm);
          
        }
    }
}
