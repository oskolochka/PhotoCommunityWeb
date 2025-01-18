using PhotoCommunityWeb.Models;

namespace PhotoCommunityWeb.Services
{
    public interface ICommentService
    {
        void AddComment(Comment comment);
        List<Comment> GetCommentsByPhotoId(int photoId);
        Comment GetComment(int commentId);
        void UpdateComment(Comment comment);
        void DeleteComment(int commentId);
    }
}
