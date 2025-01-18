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
        public void AddComment_True_CommentIsValid()
        {
            var comment = new Comment { CommentId = 1, PhotoId = 1, UserId = 1, CommentText = "Отличное фото!" };

            var result = _commentService.AddComment(comment);

            Assert.IsTrue(result); 
        }

        [Test]
        public void AddComment_False_CommentTextIsEmpty()
        {
            var comment = new Comment { CommentId = 1, PhotoId = 1, UserId = 1, CommentText = "" };

            var result = _commentService.AddComment(comment);

            Assert.IsFalse(result); 
        }

        [Test]
        public void GetCommentsByPhotoId_Comments_ForGivenPhotoId()
        {
            var comment1 = new Comment { CommentId = 1, PhotoId = 1, UserId = 1, CommentText = "Отличное фото!" };
            var comment2 = new Comment { CommentId = 2, PhotoId = 1, UserId = 2, CommentText = "Красивый закат!" };
            var comment3 = new Comment { CommentId = 3, PhotoId = 2, UserId = 1, CommentText = "Прекрасный вид!" };

            _commentService.AddComment(comment1);
            _commentService.AddComment(comment2);
            _commentService.AddComment(comment3);

            var comments = _commentService.GetCommentsByPhotoId(1);

            Assert.AreEqual(2, comments.Count);
            Assert.IsTrue(comments.Exists(c => c.CommentId == 1));
            Assert.IsTrue(comments.Exists(c => c.CommentId == 2));
        }

        [Test]
        public void GetComment_Comment_CommentExists()
        {
            var comment = new Comment { CommentId = 1, PhotoId = 1, UserId = 1, CommentText = "Отличное фото!" };
            _commentService.AddComment(comment);

            var retrievedComment = _commentService.GetComment(1);

            Assert.IsNotNull(retrievedComment);
            Assert.AreEqual("Отличное фото!", retrievedComment.CommentText);
        }

        [Test]
        public void GetComment_Null_CommentDoesNotExist()
        {
            var retrievedComment = _commentService.GetComment(1);

            Assert.IsNull(retrievedComment); 
        }

        [Test]
        public void DeleteComment_True_CommentExists()
        {
            var comment = new Comment { CommentId = 1, PhotoId = 1, UserId = 1, CommentText = "Отличное фото!" };
            _commentService.AddComment(comment);

            var result = _commentService.DeleteComment(1);

            Assert.IsTrue(result);
            var retrievedComment = _commentService.GetComment(1);
            Assert.IsNull(retrievedComment); 
        }

        [Test]
        public void DeleteComment_ReturnsFalse_WhenCommentDoesNotExist()
        {
            var result = _commentService.DeleteComment(1);

            Assert.IsFalse(result); 
        }
    }
}