using System.ComponentModel.DataAnnotations;

namespace PhotoCommunityWeb.Models
{
    public class Photo
    {
        public int PhotoId { get; set; } 

        public int UserId { get; set; } 

        [Required(ErrorMessage = "Заголовок обязателен")]
        public string Title { get; set; }

        public string? Description { get; set; } 

        public string? Tags { get; set; }

        [Required(ErrorMessage = "Путь к файлу обязателен")]
        public string FilePath { get; set; }

        public virtual User User { get; set; }
    }
}
