using PhotoCommunityWeb.Models;

namespace PhotoCommunityWeb.Services
{
    public class PhotoService
    {
        private readonly List<Photo> _photos = new List<Photo>();

        public bool UploadPhoto(Photo photo)
        {
            if (string.IsNullOrWhiteSpace(photo.Title) || string.IsNullOrWhiteSpace(photo.FilePath))
            {
                return false;
            }

            _photos.Add(photo);
            return true;
        }

        public bool EditPhoto(Photo updatedPhoto)
        {
            var existingPhoto = _photos.FirstOrDefault(p => p.PhotoId == updatedPhoto.PhotoId);
            if (existingPhoto == null)
            {
                return false;
            }

            existingPhoto.Title = updatedPhoto.Title;
            existingPhoto.FilePath = updatedPhoto.FilePath;
            existingPhoto.Description = updatedPhoto.Description;
            existingPhoto.Tags = updatedPhoto.Tags;
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

        public Photo GetPhoto(int photoId)
        {
            return _photos.FirstOrDefault(p => p.PhotoId == photoId);
        }

        public List<Photo> GetAllPhotos()
        {
            return _photos;
        }
    }
}
