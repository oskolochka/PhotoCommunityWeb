using PhotoCommunityWeb.Models;

namespace PhotoCommunityWeb.Services
{
    public class PhotoService
    {
        private readonly List<Photo> _photos = new List<Photo>();

        public void AddPhoto(Photo photo)
        {
            _photos.Add(photo);
        }

        public List<Photo> GetPhotosByUserId(int userId)
        {
            return _photos
                .Where(p => p.UserId == userId)
                .Take(10) 
                .ToList();
        }

        public Photo GetPhoto(int photoId)
        {
            return _photos.FirstOrDefault(p => p.PhotoId == photoId); 
        }

        public bool UpdatePhoto(Photo photo)
        {
            var existingPhoto = _photos.FirstOrDefault(p => p.PhotoId == photo.PhotoId);
            if (existingPhoto == null)
            {
                return false; 
            }

            existingPhoto.Title = photo.Title; 
            existingPhoto.Description = photo.Description; 
            existingPhoto.Tags = photo.Tags; 
            existingPhoto.FilePath = photo.FilePath; 

            return true;
        }

        public bool DeletePhoto(int photoId)
        {
            var photo = _photos.FirstOrDefault(p => p.PhotoId == photoId);
            if (photo == null)
            {
                return false; 
            }

            _photos.Remove(photo); 
            return true; 
        }

        public List<Photo> SearchPhotos(string searchTerm)
        {
            return _photos
                .Where(p => p.Title.Contains(searchTerm) ||
                             p.Description.Contains(searchTerm) ||
                             (p.Tags != null && p.Tags.Contains(searchTerm)))
                .ToList(); 
        }
    }
}
