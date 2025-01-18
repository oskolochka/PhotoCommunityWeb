using NUnit.Framework;
using PhotoCommunityWeb.Models;
using PhotoCommunityWeb.Services;

namespace PhotoCommunityWeb.Tests.Services
{
    [TestFixture]
    public class CommentServiceTests
    {
        private CommentService _commentService;

        [SetUp]
        public void Setup()
        {
            _commentService = new CommentService();
        }

        [Test]
        public void AddComment_ReturnTrue_ValidData()
        {
            var comment = new Comment
            {
                CommentId = 1,
                PhotoId = 1,
                UserId = 1,
                CommentText = "Great!"
            };

            var result = _commentService.AddComment(comment);

            Assert.That(result, Is.True);
        }

        [Test]
        public void AddComment_ReturnFalse_MissingContent()
        {
            var comment = new Comment
            {
                CommentId = 1,
                PhotoId = 1,
                UserId = 1,
                CommentText = ""
            };

            var result = _commentService.AddComment(comment);

            Assert.That(result, Is.False);
        }

        [Test]
        public void EditComment_ReturnTrue_CommentExists()
        {
            var comment = new Comment
            {
                CommentId = 1,
                PhotoId = 1,
                UserId = 1,
                CommentText = "Great!"
            };
            _commentService.AddComment(comment);

            var updatedComment = new Comment
            {
                CommentId = 1,
                PhotoId = 1,
                UserId = 1,
                CommentText = "Great!"
            };
            var result = _commentService.EditComment(updatedComment);

            Assert.That(result, Is.True);
            Assert.That(_commentService.GetComment(1).CommentText, Is.EqualTo("Great!"));
        }

        [Test]
        public void EditComment_ReturnFalse_CommentDoesNotExist()
        {
            var updatedComment = new Comment
            {
                CommentId = 1,
                PhotoId = 1,
                UserId = 1,
                CommentText = "Great!"
            };

            var result = _commentService.EditComment(updatedComment);

            Assert.That(result, Is.False);
        }

        [Test]
        public void DeleteComment_ReturnTrue_CommentExists()
        {
            var comment = new Comment
            {
                CommentId = 1,
                PhotoId = 1,
                UserId = 1,
                CommentText = "Great!"
            };
            _commentService.AddComment(comment);

            var result = _commentService.DeleteComment(1);

            Assert.That(result, Is.True);
            Assert.That(_commentService.GetComment(1), Is.Null);
        }

        [Test]
        public void DeleteComment_ReturnFalse_CommentDoesNotExist()
        {
            var result = _commentService.DeleteComment(1);

            Assert.That(result, Is.False);
        }

        [Test]
        public void GetComment_ReturnComment_CommentExists()
        {
            var comment = new Comment
            {
                CommentId = 1,
                PhotoId = 1,
                UserId = 1,
                CommentText = "Great!"
            };
            _commentService.AddComment(comment);

            var result = _commentService.GetComment(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.CommentText, Is.EqualTo("Great!"));
        }

        [Test]
        public void GetComment_ReturnNull_CommentDoesNotExist()
        {
            var result = _commentService.GetComment(1);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetCommentsByPhotoId_ReturnComments_ForGivenPhotoId()
        {
            var comment1 = new Comment
            {
                CommentId = 1,
                PhotoId = 1,
                UserId = 1,
                CommentText = "Great!"
            };
            var comment2 = new Comment
            {
                CommentId = 2,
                PhotoId = 1,
                UserId = 2,
                CommentText = "Great!"
            };
            var comment3 = new Comment
            {
                CommentId = 3,
                PhotoId = 2,
                UserId = 1,
                CommentText = "Great!"
            };

            _commentService.AddComment(comment1);
            _commentService.AddComment(comment2);
            _commentService.AddComment(comment3);

            var result = _commentService.GetCommentsByPhotoId(1);

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(c => c.CommentId == 1), Is.True);
            Assert.That(result.Any(c => c.CommentId == 2), Is.True);
        }
    }
}