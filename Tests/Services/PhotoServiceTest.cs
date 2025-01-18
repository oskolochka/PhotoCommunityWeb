using NUnit.Framework;
using PhotoCommunityWeb.Models;
using PhotoCommunityWeb.Services;

namespace PhotoCommunityWeb.Tests.Services
{
    [TestFixture]
    public class PhotoServiceTests
    {
        private PhotoService _photoService;

        [SetUp]
        public void Setup()
        {
            _photoService = new PhotoService();
        }

        [Test]
        public void AddPhoto_Photo_AddsPhotoSuccessfully()
        {
            var photo = new Photo { PhotoId = 1, UserId = 1, Title = "Закат", FilePath = "путь/к/фото.jpg" };

            _photoService.AddPhoto(photo);

            var retrievedPhoto = _photoService.GetPhoto(1);
            Assert.IsNotNull(retrievedPhoto);
            Assert.AreEqual("Закат", retrievedPhoto.Title);
        }

        [Test]
        public void GetPhotosByUserId_Photos_UserHasPhotos()
        {
            var photo1 = new Photo { PhotoId = 1, UserId = 1, Title = "Закат", FilePath = "путь/к/фото1.jpg" };
            var photo2 = new Photo { PhotoId = 2, UserId = 1, Title = "Пляж", FilePath = "путь/к/фото2.jpg" };
            var photo3 = new Photo { PhotoId = 3, UserId = 2, Title = "Гора", FilePath = "путь/к/фото3.jpg" };

            _photoService.AddPhoto(photo1);
            _photoService.AddPhoto(photo2);
            _photoService.AddPhoto(photo3);

            var photos = _photoService.GetPhotosByUserId(1);

            Assert.AreEqual(2, photos.Count);
        }

        [Test]
        public void GetPhoto_Photo_PhotoExists()
        {
            var photo = new Photo { PhotoId = 1, UserId = 1, Title = "Закат", FilePath = "путь/к/фото.jpg" };
            _photoService.AddPhoto(photo);

            var retrievedPhoto = _photoService.GetPhoto(1);

            Assert.IsNotNull(retrievedPhoto);
            Assert.AreEqual("Закат", retrievedPhoto.Title);
        }

        [Test]
        public void GetPhoto_Null_PhotoDoesNotExist()
        {
            var retrievedPhoto = _photoService.GetPhoto(1);

            Assert.IsNull(retrievedPhoto);
        }

        [Test]
        public void UpdatePhoto_True_PhotoIsUpdated()
        {
            var photo = new Photo { PhotoId = 1, UserId = 1, Title = "Закат", FilePath = "путь/к/фото.jpg" };
            _photoService.AddPhoto(photo);

            var updatedPhoto = new Photo { PhotoId = 1, UserId = 1, Title = "Другой Закат", FilePath = "путь/к/другому_фото.jpg" };
            var result = _photoService.UpdatePhoto(updatedPhoto);

            Assert.IsTrue(result);
            var retrievedPhoto = _photoService.GetPhoto(1);
            Assert.AreEqual("Другой Закат", retrievedPhoto.Title);
        }

        [Test]
        public void UpdatePhoto_False_PhotoDoesNotExist()
        {
            var photo = new Photo { PhotoId = 1, UserId = 1, Title = "Закат", FilePath = "путь/к/фото.jpg" };

            var result = _photoService.UpdatePhoto(photo);

            Assert.IsFalse(result); // Фотография не найдена
        }

        [Test]
        public void DeletePhoto_True_PhotoExists()
        {
            var photo = new Photo { PhotoId = 1, UserId = 1, Title = "Закат", FilePath = "путь/к/фото.jpg" };
            _photoService.AddPhoto(photo);

            var result = _photoService.DeletePhoto(1);

            Assert.IsTrue(result);
            var retrievedPhoto = _photoService.GetPhoto(1);
            Assert.IsNull(retrievedPhoto);
        }

        [Test]
        public void DeletePhoto_False_PhotoDoesNotExist()
        {
            var result = _photoService.DeletePhoto(1);

            Assert.IsFalse(result); 
        }

        [Test]
        public void SearchPhotos_Photos_SearchTermMatches()
        {
            var photo1 = new Photo { PhotoId = 1, UserId = 1, Title = "Закат", Description = "Красивый закат", Tags = "закат" };
            var photo2 = new Photo { PhotoId = 2, UserId = 1, Title = "Пляж", Description = "Солнечный пляж", Tags = "пляж" };
            var photo3 = new Photo { PhotoId = 3, UserId = 2, Title = "Гора", Description = "Высокая гора", Tags = "гора" };

            _photoService.AddPhoto(photo1);
            _photoService.AddPhoto(photo2);
            _photoService.AddPhoto(photo3);

            var searchResults = _photoService.SearchPhotos("закат");

            Assert.AreEqual(1, searchResults.Count);
            Assert.AreEqual("Закат", searchResults[0].Title);
        }

        [Test]
        public void SearchPhotos_EmptyList_NoMatchesFound()
        {
            var photo1 = new Photo { PhotoId = 1, UserId = 1, Title = "Закат", Description = "Красивый закат", Tags = "закат" };
            var photo2 = new Photo { PhotoId = 2, UserId = 1, Title = "Пляж", Description = "Солнечный пляж", Tags = "пляж" };

            _photoService.AddPhoto(photo1);
            _photoService.AddPhoto(photo2);

            var searchResults = _photoService.SearchPhotos("гора");

            Assert.AreEqual(0, searchResults.Count); 
        }

        [Test]
        public void SearchPhotos_Photos_SearchTermMatchesTags()
        {
            var photo1 = new Photo { PhotoId = 1, UserId = 1, Title = "Закат", Description = "Красивый закат", Tags = "закат" };
            var photo2 = new Photo { PhotoId = 2, UserId = 1, Title = "Пляж", Description = "Солнечный пляж", Tags = "пляж" };
            var photo3 = new Photo { PhotoId = 3, UserId = 2, Title = "Гора", Description = "Высокая гора", Tags = "гора" };

            _photoService.AddPhoto(photo1);
            _photoService.AddPhoto(photo2);
            _photoService.AddPhoto(photo3);

            var searchResults = _photoService.SearchPhotos("гора");

            Assert.AreEqual(1, searchResults.Count); 
            Assert.AreEqual("Гора", searchResults[0].Title);
        }
    }
}
