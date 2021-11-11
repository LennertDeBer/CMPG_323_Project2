using CMPG_323_Project2.Data;
using CMPG_323_Project2.Logic;
using CMPG_323_Project2.Models;
using CMPG_323_Project2.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMPG_323_Project2.Controllers
{
    public class PhotoController : Controller
    {

        //private readonly CMPG_DBContext _DBContext;
        private readonly IGenericRepository<Photo> _photo;
        private readonly IFileManagerLogic _fileManagerLogic;


        public PhotoController(IGenericRepository<Photo> photo, IFileManagerLogic fileManagerLogic)
        {
            _photo=photo;
            _fileManagerLogic = fileManagerLogic;
        }
        public IActionResult Index()
        {

            List<Photo> photos=_photo.GetAll();
            return View(photos);
        }

     
       
        [HttpGet]
        public IActionResult Create(int Id)
        {
            Photo photo = new Photo();
            return View(photo);
        
        }










        /* add to database*/
        [HttpPost]
        public IActionResult Create(Photo photo)
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

            photo.PhotoId = auid;
            _photo.Insert(photo);


            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Photo photo=_photo.GetById(Id);
            return View(photo);
        }
        [HttpPost]
        public IActionResult Delete(Photo photo)
        {
            _photo.Delete(photo.PhotoId);
          _fileManagerLogic.Delete(photo.PhotoId.ToString());
            //_DBContext.Attach(photo);
            //_DBContext.Entry(photo).State = EntityState.Deleted;
            //_DBContext.SaveChanges();

            return Redirect("/UserPhoto");
        }
        public async Task<IActionResult> SingleFile(IFormFile file)
        {
            FileModel model = new FileModel();
            model.MyFile = file;

            FileModelController fmc = new FileModelController(_fileManagerLogic);
                await fmc.UploadFile(model);
            return Ok();
        }
        public IActionResult Details(int Id)
        {
            Photo metaDatum=_photo.GetById(Id);
            return View(metaDatum);

        }
    }
}
