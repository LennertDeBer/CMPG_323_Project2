using CMPG_323_Project2.Controllers;
using CMPG_323_Project2.Models;
using CMPG_323_Project2.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject323
{
    public class AlbumControllerTest
    {
        private List<Album> albumList = new List<Album>()
        {
            new Album
            {
                AlbumId = 1,
                AlbumName = "Album1"
            },
            new Album
            {
                AlbumId = 2,
                AlbumName = "Album2"
            }
        };

        [Fact]
        public void Index()
        {
            var mockRepo1 = new Mock<IGenericRepository<Album>>();

            mockRepo1.Setup(repo => repo.GetAll())
                .Returns(GetAlbums());

            var controller = new AlbumController(mockRepo1.Object);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Album>>(viewResult.ViewData.Model);

            Assert.Equal(2, model.Count);
        }

        public List<Album> GetAlbums()
        {
            return albumList;
        }

        [Fact]
        public void Create()
        {
            Album albumTest = new Album();
            albumTest.AlbumId = 3;
            albumTest.AlbumName = "Album3";

            var mockRepo1 = new Mock<IGenericRepository<Album>>();

            mockRepo1.Setup(repo => repo.GetAll())
                .Returns(GetAlbums());

            var controller = new AlbumController(mockRepo1.Object);

            var result = controller.Create(albumTest);

            AddAlbum(albumTest);
            var all = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(all);
            var model = Assert.IsAssignableFrom<List<Album>>(viewResult.ViewData.Model);

            Assert.Equal(3, model.Count);

        }

        public void AddAlbum(Album album)
        {
            albumList.Add(album);
        }
    }
}
