using Moq;
using NUnit.Framework;
using PhotoCommunityWeb.Controllers;
using PhotoCommunityWeb.Models;
using PhotoCommunityWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace PhotoCommunityWeb.Tests
{
    [TestFixture]
    public class PhotoControllerTest
    {
        private Mock<IPhotoService> _mockPhotoService;
        private PhotoController _photoController;

        [SetUp]
        public void Setup()
        {
            _mockPhotoService = new Mock<IPhotoService>();
            _photoController = new PhotoController(_mockPhotoService.Object);
        }

        [Test]
        public void AddPhoto_CreatedAtAction_PhotoIsAdded()
        {
            var photo = new Photo { PhotoId = 1, Title = "Тестовая фотография" };

            var result = _photoController.AddPhoto(photo) as CreatedAtActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("GetPhoto", result.ActionName);
            Assert.AreEqual(photo, result.Value);
        }

        [Test]
        public void AddPhoto_BadRequest_PhotoIsInvalid()
        {
            var photo = new Photo { PhotoId = 1, Title = null };

            var result = _photoController.AddPhoto(photo);

            var badRequestResult = result as BadRequestObjectResult; 
            Assert.IsNotNull(badRequestResult); 
            Assert.AreEqual(400, badRequestResult.StatusCode); 
        }

        [Test]
        public void GetPhoto_ReturnsPhoto_WhenExists()
        {
            var photo = new Photo { PhotoId = 1, Title = "Тестовая фотография" };
            _mockPhotoService.Setup(s => s.GetPhoto(1)).Returns(photo);

            var result = _photoController.GetPhoto(1);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(photo, okResult.Value);
        }

        [Test]
        public void GetPhoto_NotFound_DoesNotExist()
        {
            _mockPhotoService.Setup(s => s.GetPhoto(1)).Returns((Photo)null);

            var result = _photoController.GetPhoto(1);

            Assert.IsInstanceOf<NotFoundResult>(result); 
        }

        [Test]
        public void UpdatePhoto_NoContent_PhotoIsUpdated()
        {
            var photo = new Photo { PhotoId = 1, Title = "Обновленная фотография" };
            _mockPhotoService.Setup(s => s.UpdatePhoto(photo)).Returns(true);

            var result = _photoController.UpdatePhoto(photo);

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void UpdatePhoto_NotFound_PhotoDoesNotExist()
        {
            var photo = new Photo { PhotoId = 1, Title = "Обновленная фотография" };
            _mockPhotoService.Setup(s => s.UpdatePhoto(photo)).Returns(false);

            var result = _photoController.UpdatePhoto(photo);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void DeletePhoto_NoContent_PhotoIsDeleted()
        {
            _mockPhotoService.Setup(s => s.DeletePhoto(1)).Returns(true);

            var result = _photoController.DeletePhoto(1);

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void DeletePhoto_NotFound_PhotoDoesNotExist()
        {
            _mockPhotoService.Setup(s => s.DeletePhoto(1)).Returns(false);

            var result = _photoController.DeletePhoto(1);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void SearchPhotos_Photos_Found()
        {
            var searchTerm = "Тест";
            var photos = new List<Photo>

            {
                new Photo { PhotoId = 1, Title = "Тестовая фотография" },
                new Photo { PhotoId = 2, Title = "Другие фотографии" }
            };

            _mockPhotoService.Setup(s => s.SearchPhotos(searchTerm)).Returns(photos);

            var result = _photoController.SearchPhotos(searchTerm);

            var okResult = result.Result as OkObjectResult; 
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOf<List<Photo>>(okResult.Value); 
            Assert.AreEqual(2, ((List<Photo>)okResult.Value).Count); 
        }

        [Test]
        public void SearchPhotos_EmptyList_NoPhotosFound()
        {
            var searchTerm = "Неизвестный";
            var photos = new List<Photo>();
            _mockPhotoService.Setup(s => s.SearchPhotos(searchTerm)).Returns(photos);

            // Act
            var result = _photoController.SearchPhotos(searchTerm);

            // Assert
            var okResult = result.Result as OkObjectResult; 
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOf<List<Photo>>(okResult.Value); 
            Assert.AreEqual(0, ((List<Photo>)okResult.Value).Count); 
        }

        [Test]
        public void GetPhotosByUserId_Photos_UserHasPhotos()
        {
            var userId = 1;
            var photos = new List<Photo>

            {
                new Photo { PhotoId = 1, Title = "Фотография 1", UserId = userId },
                new Photo { PhotoId = 2, Title = "Фотография 2", UserId = userId }
            };

            _mockPhotoService.Setup(s => s.GetPhotosByUserId(userId)).Returns(photos);

            var result = _photoController.GetPhotosByUserId(userId) as ActionResult<List<Photo>>;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(2, ((List<Photo>)okResult.Value).Count); 
        }

        [Test]
        public void GetPhotosByUserId_EmptyList_UserHasNoPhotos()
        {
            var userId = 1;
            _mockPhotoService.Setup(s => s.GetPhotosByUserId(userId)).Returns(new List<Photo>());

            var result = _photoController.GetPhotosByUserId(userId) as ActionResult<List<Photo>>;

            Assert.IsNotNull(result);
            var okResult = result.Result as OkObjectResult; 
            Assert.IsNotNull(okResult); 
            Assert.IsInstanceOf<List<Photo>>(okResult.Value); 
            Assert.AreEqual(0, ((List<Photo>)okResult.Value).Count);
        }
    }
}