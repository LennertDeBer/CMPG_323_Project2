using CMPG_323_Project2.Logic;
using CMPG_323_Project2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace CMPG_323_Project2.Controllers
{
    public class FileModelController : Controller
    {
        private readonly IFileManagerLogic _fileManagerLogic;
        public FileModelController(IFileManagerLogic fileManagerLogic)
        {
            _fileManagerLogic = fileManagerLogic;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult UploadFile()
        {
           
            return View();
        }
        [HttpPost]
        public  async Task<IActionResult> UploadFile(FileModel model)
        {
       
            if (model.MyFile != null)
            {
               await _fileManagerLogic.Upload(model,1);
            }
            return Redirect("/Home/index");
        }
    }
}
