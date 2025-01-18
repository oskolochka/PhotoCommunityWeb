using PhotoCommunityWeb.Models;

namespace PhotoCommunityWeb.Services
{
    public interface IPhotoService
    {
        void AddPhoto(Photo photo);
        List<Photo> GetPhotosByUserId(int userId);
        Photo GetPhoto(int photoId);
        bool UpdatePhoto(Photo photo);
        bool DeletePhoto(int photoId);
        List<Photo> SearchPhotos(string searchTerm);
    }
}
