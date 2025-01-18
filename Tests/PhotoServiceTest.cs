using NUnit.Framework;
using PhotoCommunityWeb.Models;
using PhotoCommunityWeb.Services;

namespace PhotoCommunityWeb.Tests
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
        public void UploadPhoto_ReturnTrue_ValidData()
        {
            var photo = new Photo
            {
                PhotoId = 1,
                UserId = 1,
                Title = "Test Photo",
                FilePath = "http://example.com/photo.jpg"
            };

            var result = _photoService.UploadPhoto(photo);

            Assert.That(result, Is.True);
        }

        [Test]
        public void UploadPhoto_ReturnFalse_MissingTitle()
        {
            var photo = new Photo
            {
                PhotoId = 1,
                UserId = 1,
                Title = "",
                FilePath = "http://example.com/photo.jpg"
            };

            var result = _photoService.UploadPhoto(photo);

            Assert.That(result, Is.False);
        }

        [Test]
        public void UploadPhoto_ReturnFalse_MissingFilePath()
        {
            var photo = new Photo
            {
                PhotoId = 1,
                UserId = 1,
                Title = "Test Photo",
                FilePath = ""
            };

            var result = _photoService.UploadPhoto(photo);

            Assert.That(result, Is.False);
        }

        [Test]
        public void EditPhoto_ReturnTrue_PhotoExists()
        {
            var photo = new Photo
            {
                PhotoId = 1,
                UserId = 1,
                Title = "Title",
                FilePath = "http://example.com/photo.jpg"
            };
            _photoService.UploadPhoto(photo);

            var updatedPhoto = new Photo
            {
                PhotoId = 1,
                UserId = 1,
                Title = "New Title",
                FilePath = "http://example.com/photo.jpg"
            };
            var result = _photoService.EditPhoto(updatedPhoto);

            Assert.That(result, Is.True);
            Assert.That(_photoService.GetPhoto(1).Title, Is.EqualTo("New Title"));
        }

        [Test]
        public void EditPhoto_ReturnFalse_PhotoDoesNotExist()
        {
            var updatedPhoto = new Photo
            {
                PhotoId = 1,
                UserId = 1,
                Title = "Title",
                FilePath = "http://example.com/photo.jpg"
            };

            var result = _photoService.EditPhoto(updatedPhoto);

            Assert.That(result, Is.False);
        }

        [Test]
        public void DeletePhoto_ReturnTrue_PhotoExists()
        {
            var photo = new Photo
            {
                PhotoId = 1,
                UserId = 1,
                Title = "Test Photo",
                FilePath = "http://example.com/photo.jpg"
            };
            _photoService.UploadPhoto(photo);

            var result = _photoService.DeletePhoto(1);

            Assert.That(result, Is.True);
            Assert.That(_photoService.GetPhoto(1), Is.Null);
        }

        [Test]
        public void DeletePhoto_ReturnFalse_PhotoDoesNotExist()
        {
            var result = _photoService.DeletePhoto(1);

            Assert.That(result, Is.False);
        }

        [Test]
        public void GetPhoto_ReturnPhoto_PhotoExists()
        {
            var photo = new Photo
            {
                PhotoId = 1,
                UserId = 1,
                Title = "Test Photo",
                FilePath = "http://example.com/photo.jpg"
            };
            _photoService.UploadPhoto(photo);

            var result = _photoService.GetPhoto(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo("Test Photo"));
        }

        [Test]
        public void GetAllPhotos_ReturnAllPhotos()
        {
            var photo1 = new Photo
            {
                PhotoId = 1,
                UserId = 1,
                Title = "Photo 1",
                FilePath = "http://example.com/photo1.jpg"
            };

            var photo2 = new Photo
            {
                PhotoId = 2,
                UserId = 1,
                Title = "Photo 2",
                FilePath = "http://example.com/photo2.jpg"
            };
            _photoService.UploadPhoto(photo1);
            _photoService.UploadPhoto(photo2);

            var result = _photoService.GetAllPhotos();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(p => p.PhotoId == photo1.PhotoId), Is.True);
            Assert.That(result.Any(p => p.PhotoId == photo2.PhotoId), Is.True);
        }
    }
}
