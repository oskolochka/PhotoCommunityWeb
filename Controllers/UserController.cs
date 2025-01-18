using Microsoft.AspNetCore.Mvc;
using PhotoCommunityWeb.Models;
using PhotoCommunityWeb.Services;

namespace PhotoCommunityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody] User user)
        {
            if (_userService.RegisterUser(user))
            {
                return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
            }
            return BadRequest("Ошибка регистрации пользователя");
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound(); 
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            if (_userService.UpdateUser(user))
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (_userService.DeleteUser(id))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}