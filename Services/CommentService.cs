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

        public bool EditComment(Comment updatedComment)
        {
            var existingComment = _comments.FirstOrDefault(c => c.CommentId == updatedComment.CommentId);
            if (existingComment == null)
            {
                return false;
            }

            existingComment.CommentText = updatedComment.CommentText;
            return true;
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

        public Comment GetComment(int commentId)
        {
            return _comments.FirstOrDefault(c => c.CommentId == commentId);
        }

        public List<Comment> GetCommentsByPhotoId(int photoId)
        {
            return _comments.Where(c => c.PhotoId == photoId).ToList();
        }
    }
}
