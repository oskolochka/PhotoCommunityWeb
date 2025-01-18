using System.ComponentModel.DataAnnotations;

namespace PhotoCommunityWeb.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Имя обязательно")]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Логин обязателен")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Пароль обязателен")]
        [MinLength(8, ErrorMessage = "Пароль должен содержать не менее 8 символов")]
        public string Password { get; set; } = string.Empty;

        public string? Cameras { get; set; }

        public string? Lenses { get; set; }
    }
}
