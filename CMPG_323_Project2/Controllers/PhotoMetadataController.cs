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
        private int validateId(int Id)
        {
            int tmp = 0;
            bool vl = false;
            List<int> v = _fileManagerLogic.loop();
            foreach (int id in v)
            {
                if(tmp<=id)
                {
                    tmp = id;
                }
                if(id==Id)
                {
                    vl = true;
                }
            }
            if(vl)
            {
                return ++tmp;
            }
            else
            {
                return Id;
            }
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
            auid = validateId(auid);
            await _fileManagerLogic.Upload(photoViewModelMeta.fileModelVm, auid);
            string tmp = photoViewModelMeta.fileModelVm.MyFile.FileName;
            string extention = tmp.Substring(tmp.IndexOf('.')); 

            string Url = _fileManagerLogic.read(auid.ToString()+extention);
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
            List<MetaDatum> md=_metaData.Find("SELECT *  FROM MetaData WHERE Photo_ID = " + Id + " ");
            return md[0].MetadataId;
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

        [HttpPost]
        public IActionResult Search(string colNeed, string searchNeed)
        {
            var usid = _UserManager.GetUserId(HttpContext.User);
            List<MetaDatum> metadatas = _metaData.Find("SELECT *  FROM MetaData WHERE " + colNeed + " = '" + searchNeed + "'");
            List<Photo> photoList = _photo.GetAll();
            List<UserPhoto> user_image_link = _link.Find("select * from(select *, row_number() over(partition by User_ID, Photo_ID, Recepient_User_ID  order by Photo_ID) as row_number from[dbo].UserPhoto) as rows where row_number = 1");
            var container = from uil in user_image_link
                            from m in metadatas
                            from i in photoList
                            where uil.RecepientUserId == usid
                            where uil.PhotoId == i.PhotoId
                            where m.PhotoId == i.PhotoId
                            select new PhotoViewModelMetaData { metadataVm = m, photoVm = i };



            //ViewBag.Message = searchNeed;

            //List<MetaDatum> returnMeta = (List<MetaDatum>)(from m in metadatas
            //                             where colNeed == searchNeed
            //                             select new MetaDatum { MetadataId=m.MetadataId,Tags=m.Tags,CapturedBy=m.CapturedBy, CapturedDate =m.CapturedDate
            //                             , Geolocation = m.Geolocation, PhotoId =m.PhotoId});
            return View(container);

        }
    }
}
