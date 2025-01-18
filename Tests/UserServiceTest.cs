using NUnit.Framework;
using PhotoCommunityWeb.Models;
using PhotoCommunityWeb.Services;


namespace PhotoCommunityWeb.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _userService = new UserService();
        }

        [Test]
        public void RegisterUser_ReturnTrue_WhenValidData()
        {
            var user = new User
            {
                LastName = "Иванов",
                FirstName = "Иван",
                Login = "ivanov",
                Password = "password123"
            };

            var result = _userService.RegisterUser(user);

            Assert.IsTrue(result);
        }

        [Test]
        public void RegisterUser_ReturnFalse_LoginExists()
        {
            var user1 = new User { LastName = "Иванов", FirstName = "Иван", Login = "ivanov", Password = "password123" };
            var user2 = new User { LastName = "Петров", FirstName = "Петр", Login = "ivanov", Password = "password456" };

            _userService.RegisterUser(user1);
            var result = _userService.RegisterUser(user2);

            Assert.IsFalse(result);
        }

        [Test]
        public void RegisterUser_ReturnFalse_PasswordTooShort()
        {
            var user = new User { LastName = "Иванов", FirstName = "Иван", Login = "ivanov", Password = "short" };

            var result = _userService.RegisterUser(user);

            Assert.IsFalse(result);
        }

        [Test]
        public void RegisterUser_ReturnFalse_MissingRequiredFields()
        {
            var user = new User { LastName = "", FirstName = "Иван", Login = "ivanov", Password = "password123" };

            var result = _userService.RegisterUser(user);

            Assert.IsFalse(result);
        }

        [Test]
        public void Login_ReturnTrue_ValidCredentials()
        {
            var user = new User { LastName = "Иванов", FirstName = "Иван", Login = "ivanov", Password = "password123" };
            _userService.RegisterUser(user);

            var result = _userService.Login("ivanov", "password123");

            Assert.IsTrue(result);
        }

        [Test]
        public void Login_ReturnFalse_InvalidCredentials()
        {
            var user = new User { LastName = "Иванов", FirstName = "Иван", Login = "ivanov", Password = "password123" };
            _userService.RegisterUser(user);

            var result = _userService.Login("ivanov", "wrongpassword");

            Assert.IsFalse(result);
        }

        [Test]
        public void Login_ReturnFalse_UserDoesNotExist()
        {
            var result = _userService.Login("nonexistent", "password123");

            Assert.IsFalse(result);
        }
    }
}
