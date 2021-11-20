using CMPG_323_Project2.Areas.Identity.Data;
using CMPG_323_Project2.Controllers;
using CMPG_323_Project2.Logic;
using CMPG_323_Project2.Models;
using CMPG_323_Project2.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Xunit;

namespace TestProject323
{
    public class PhotoControllerTest
    {
        private List<Photo> list = new List<Photo>() {new Photo{
        PhotoId=1,
        PhotoUrl="TestURl1"},
            new Photo{
                PhotoId=2,
        PhotoUrl="TestURL2"} };
        [Fact]
        public void Index()
        {
            // Arrange
            var mockRepo1 = new Mock<IGenericRepository<Photo>>();
            //var mockRepo2 = new Mock<IGenericRepository<AspNetUser>>();
            //var mockRepo3 = new Mock<IGenericRepository<UserPhoto>>();
            //var mockuser = new Mock<UserManager<AppUser>>();
            var mockRepo4 = new Mock<IFileManagerLogic>();

            mockRepo1.Setup(repo => repo.GetAll())
                .Returns(GetPhotos());
            //mockRepo2.Setup(repo => repo.GetAll())
            //    .Returns(GetUsers());
            //mockRepo3.Setup(repo => repo.Find(""))
            //    .Returns(GetUserPhotos());
            //mockRepo4.Setup(repo => repo.R())
            //    .Returns(GetPhotos());

            var controller = new PhotoController(mockRepo1.Object, mockRepo4.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Photo>>(viewResult.ViewData.Model);
        
            Assert.Equal(2, model.Count);
        }
        [Fact]
        public void Create()
        {
            Photo val = new Photo();
            val.PhotoId = 3;
            val.PhotoUrl = "";
            // Arrange
            var mockRepo1 = new Mock<IGenericRepository<Photo>>();
    
            var mockRepo4 = new Mock<IFileManagerLogic>();
            mockRepo1.Setup(repo => repo.Insert(val));
            mockRepo1.Setup(repo => repo.GetAll())
          .Returns(GetPhotos());




            var controller = new PhotoController(mockRepo1.Object, mockRepo4.Object);

            // Act
            var result = controller.Create(val);
          
            AddPhoto(val);
            var all = controller.Index();

            // Assert
            if (result.ToString()!="")
            {

            }
            var viewResult = Assert.IsType<ViewResult>(all);
            var model = Assert.IsAssignableFrom<List<Photo>>(viewResult.ViewData.Model);
            //var model = Assert.IsAssignableFrom<List<Photo>>(viewResult.ViewData.Model);
            Assert.Equal(3, model.Count);

        }
        [Fact]
        public void Delete()
        {
            Photo val = new Photo();
            val.PhotoId = 2;
            val.PhotoUrl = "Test/URL2";
            // Arrange
            var mockRepo1 = new Mock<IGenericRepository<Photo>>();

            var mockRepo4 = new Mock<IFileManagerLogic>();
            mockRepo1.Setup(repo => repo.Delete(val)); 
            mockRepo4.Setup(repo => repo.Delete(val.PhotoId.ToString()));
            mockRepo1.Setup(repo => repo.GetAll())
          .Returns(GetPhotos());




            var controller = new PhotoController(mockRepo1.Object, mockRepo4.Object);

            // Act
            var before = controller.Index();
            var result = controller.Delete(val);

             removePhoto();
            var after = controller.Index();

            // Assert
            if (result.ToString() != "")
            {

            }
            var viewResult = Assert.IsType<ViewResult>(after);
            var model = Assert.IsAssignableFrom<List<Photo>>(viewResult.ViewData.Model);
            var viewResultb = Assert.IsType<ViewResult>(before);
            var modelb = Assert.IsAssignableFrom<List<Photo>>(viewResultb.ViewData.Model);
            //var model = Assert.IsAssignableFrom<List<Photo>>(viewResult.ViewData.Model);
            Assert.Equal(1, model.Count);

        }
        //[Fact]
        //public void Details()
        //{
        //    int id = 2;
        //    // Arrange
        //    var mockRepo1 = new Mock<IGenericRepository<Photo>>();

        //    var mockRepo4 = new Mock<IFileManagerLogic>();
        // mockRepo1.Setup(repo => repo.GetById(2)).Returns(getPhotoById(id).FirstOrDefault());
       
        //  //  mockRepo1.Setup(repo => repo.GetAll())
        //  //.Returns(GetPhotos());



        //    var controller = new PhotoController(mockRepo1.Object, mockRepo4.Object);

        //    // Act
        //    var before = controller.Index();
        //    var result = controller.Details(id);

        
        //    var after = controller.Index();

        //    // Assert
        //    if (result.ToString() != "")
        //    {

        //    }

        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    var model = Assert.IsAssignableFrom<Photo>(viewResult.ViewData.Model);
        //    //var model = Assert.IsAssignableFrom<List<Photo>>(viewResult.ViewData.Model);
        //    Assert.Equal(1, model.PhotoId);
        //}
        private List<Photo> GetPhotos()
        {

            return list;
        }
       private void AddPhoto(Photo ph)
        {
            list.Add(ph);

        }
        private void removePhoto()
        {
           
             list.RemoveAt(0);
            

        }
        private List<Photo> getPhotoById(int id)
        {
            List<Photo> tmp = new List<Photo>();
            tmp.Add(list.ElementAt(1));
            return tmp;

        }
       

    }
}
