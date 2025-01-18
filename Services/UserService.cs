using PhotoCommunityWeb.Models;

namespace PhotoCommunityWeb.Services
{
    public class UserService
    {
        private readonly List<User> _users = new List<User>();

        public bool RegisterUser(User user)
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

        public bool Login(string login, string password)
        {
            return _users.Any(u => u.Login == login && u.Password == password);
        }
    }
}
