using PhotoCommunityWeb.Models;

namespace PhotoCommunityWeb.Services
{
    public class CommentService
    {
        private readonly List<Comment> _comments = new List<Comment>();

        public bool AddComment(Comment comment)
        {
            if (string.IsNullOrWhiteSpace(comment.CommentText))
            {
                return false;
            }

            _comments.Add(comment); 
            return true; 
        }

        public List<Comment> GetCommentsByPhotoId(int photoId)
        {
            return _comments
                .Where(c => c.PhotoId == photoId)
                .ToList(); 
        }

        public Comment GetComment(int commentId)
        {
            return _comments.FirstOrDefault(c => c.CommentId == commentId);
        }

        public bool DeleteComment(int commentId)
        {
            var comment = _comments.FirstOrDefault(c => c.CommentId == commentId);
            if (comment == null)
            {
                return false;
            }

            _comments.Remove(comment); 
            return true; 
        }
    }
}
