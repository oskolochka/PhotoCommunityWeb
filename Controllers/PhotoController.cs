using Microsoft.AspNetCore.Mvc;
using PhotoCommunityWeb.Models;
using PhotoCommunityWeb.Services;

namespace PhotoCommunityWeb.Controllers
{
    [ApiController]
    [Route("api/photos")]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }


        [HttpPost]
        public IActionResult AddPhoto([FromBody] Photo photo)
        {
            if (photo == null || string.IsNullOrEmpty(photo.Title))
            {
                return BadRequest("Ошибка добавления фото");
            }

            _photoService.AddPhoto(photo);
            return CreatedAtAction(nameof(GetPhoto), new { id = photo.PhotoId }, photo);
        }

        // GET: api/photos/{id}
        [HttpGet("{id}")]
        public IActionResult GetPhoto(int id)
        {
            var photo = _photoService.GetPhoto(id);
            if (photo == null)
            {
                return NotFound();
            }
            return Ok(photo);
        }

        // GET: api/photos/user/{userId}
        [HttpGet("user/{userId}")]
        public ActionResult<List<Photo>> GetPhotosByUserId(int userId)
        {
            var photos = _photoService.GetPhotosByUserId(userId);
            return Ok(photos);
        }

        // PUT: api/photos
        [HttpPut]
        public IActionResult UpdatePhoto([FromBody] Photo photo)
        {
            if (!_photoService.UpdatePhoto(photo))
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/photos/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletePhoto(int id)
        {
            if (!_photoService.DeletePhoto(id))
            {
                return NotFound();
            }
            return NoContent();
        }

        // GET: api/photos/search
        [HttpGet("search")]
        public ActionResult<List<Photo>> SearchPhotos([FromQuery] string searchTerm)
        {
            var photos = _photoService.SearchPhotos(searchTerm);

            return Ok(photos);
        }
    }
}