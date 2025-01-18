using Moq;
using NUnit.Framework;
using PhotoCommunityWeb.Controllers;
using PhotoCommunityWeb.Models;
using PhotoCommunityWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace PhotoCommunityWeb.Tests
{
    [TestFixture]
    public class CommentControllerTest
    {
        private Mock<ICommentService> _mockCommentService;
        private CommentController _commentController;

        [SetUp]
        public void Setup()
        {
            _mockCommentService = new Mock<ICommentService>();
            _commentController = new CommentController(_mockCommentService.Object);
        }

        [Test]
        public void AddComment_BadRequest_InvalidComment()
        {
            var comment = new Comment { CommentId = 1, CommentText = null }; 

            var result = _commentController.AddComment(comment);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [Test]
        public void AddComment_Comment_Valid()
        {
            var comment = new Comment { CommentId = 1, CommentText = "Тестовый комментарий" };

            var result = _commentController.AddComment(comment) as CreatedAtActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("GetComment", result.ActionName);
            Assert.AreEqual(comment, result.Value);
        }

        [Test]
        public void GetCommentsByPhotoId_Comments_Found()
        {
            var photoId = 1;
            var comments = new List<Comment>
            {
                new Comment { CommentId = 1, CommentText = "Комментарий 1", PhotoId = photoId },
                new Comment { CommentId = 2, CommentText = "Комментарий 2", PhotoId = photoId }
            };
            _mockCommentService.Setup(s => s.GetCommentsByPhotoId(photoId)).Returns(comments);

            var result = _commentController.GetCommentsByPhotoId(photoId);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOf<List<Comment>>(okResult.Value);
            Assert.AreEqual(2, ((List<Comment>)okResult.Value).Count);
        }

        [Test]
        public void GetComment_Comment_Exists()
        {
            var comment = new Comment { CommentId = 1, CommentText = "Тестовый комментарий" };
            _mockCommentService.Setup(s => s.GetComment(1)).Returns(comment);

            var result = _commentController.GetComment(1);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(comment, okResult.Value);
        }

        [Test]
        public void GetComment_NotFound_DoesNotExist()
        {
            _mockCommentService.Setup(s => s.GetComment(1)).Returns((Comment)null);

            var result = _commentController.GetComment(1);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void UpdateComment_NoContent_SuccessfullyUpdated()
        {
            var comment = new Comment { CommentId = 1, CommentText = "Обновленный комментарий" };
            _mockCommentService.Setup(s => s.UpdateComment(comment));

            var result = _commentController.UpdateComment(comment);

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void DeleteComment_NoContent_SuccessfullyDeleted()
        {
            var commentId = 1;
            _mockCommentService.Setup(s => s.DeleteComment(commentId));

            var result = _commentController.DeleteComment(commentId);

            Assert.IsInstanceOf<NoContentResult>(result);
        }
    }
}
