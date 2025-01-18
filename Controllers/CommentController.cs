using Microsoft.AspNetCore.Mvc;
using PhotoCommunityWeb.Models;
using PhotoCommunityWeb.Services;

namespace PhotoCommunityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public IActionResult AddComment([FromBody] Comment comment)
        {
            if (comment == null || string.IsNullOrEmpty(comment.CommentText))
            {
                return BadRequest("Comment is invalid.");
            }

            _commentService.AddComment(comment);
            return CreatedAtAction(nameof(GetComment), new { id = comment.CommentId }, comment);
        }

        [HttpGet("photo/{photoId}")]
        public ActionResult<List<Comment>> GetCommentsByPhotoId(int photoId)
        {
            var comments = _commentService.GetCommentsByPhotoId(photoId);
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public IActionResult GetComment(int id)
        {
            var comment = _commentService.GetComment(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpPut]
        public IActionResult UpdateComment([FromBody] Comment comment)
        {
            if (comment == null || string.IsNullOrEmpty(comment.CommentText))
            {
                return BadRequest("Comment is invalid.");
            }

            _commentService.UpdateComment(comment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            _commentService.DeleteComment(id);
            return NoContent();
        }
    }
}
