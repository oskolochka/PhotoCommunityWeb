using PhotoCommunityWeb.Models;

namespace PhotoCommunityWeb.Services
{
    public interface IUserService
    {
        bool RegisterUser(User user);
        User GetUser(int id);
        bool UpdateUser(User user);
        bool ChangePassword(int userId, string newPassword);
        bool DeleteUser(int userId);
        User Login(string login, string password);
    }
}
