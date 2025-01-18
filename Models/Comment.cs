namespace PhotoCommunityWeb.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        public int PhotoId { get; set; }

        public int UserId { get; set; }

        public required string Text { get; set; }
    }
}
