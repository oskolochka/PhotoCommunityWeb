using PhotoCommunityWeb.Models;

namespace PhotoCommunityWeb.Services
{
    public class UserService : IUserService
    {

        private readonly List<User> _users = new List<User>();

        public virtual bool RegisterUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.LastName) ||
                string.IsNullOrWhiteSpace(user.FirstName) ||
                string.IsNullOrWhiteSpace(user.Login) ||
                string.IsNullOrWhiteSpace(user.Password))
            {
                return false;
            }

            if (_users.Any(u => u.Login == user.Login))
            {
                return false;
            }

            if (user.Password.Length < 8)
            {
                return false;
            }

            _users.Add(user);
            return true; 
        }

        public virtual User Login(string login, string password)
        {
            return _users.FirstOrDefault(u => u.Login == login && u.Password == password);
        }

        public virtual User GetUser(int userId)
        {
            return _users.FirstOrDefault(u => u.UserId == userId);
        }

        public virtual bool UpdateUser(User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.UserId == user.UserId);
            if (existingUser == null || existingUser.Login != user.Login)
            {
                return false; 
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.MiddleName = user.MiddleName;
            existingUser.Cameras = user.Cameras;
            existingUser.Lenses = user.Lenses;

            return true; 
        }

        public virtual bool ChangePassword(int userId, string newPassword)
        {

            if (newPassword.Length < 8)
            {
                return false;
            }

            var user = GetUser(userId);
            if (user == null)
            {
                return false; 
            }

            user.Password = newPassword;
            return true; 
        }

        public virtual bool DeleteUser(int userId)
        {
            var user = _users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                return false; 
            }

            _users.Remove(user);
            return true; 
        }
    }
}
