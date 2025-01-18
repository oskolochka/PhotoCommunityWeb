namespace PhotoCommunityWeb.Models
{
    public class Photo
    {
        public int PhotoId { get; set; }

        public int UserId { get; set; }

        public required string Title { get; set; }

        public string? Description { get; set; }

        public string? Tags { get; set; }

        public required string FilePath { get; set; }
    }
}
