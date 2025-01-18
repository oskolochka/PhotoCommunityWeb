using NUnit.Framework;
using PhotoCommunityWeb.Models;
using PhotoCommunityWeb.Services;

namespace PhotoCommunityWeb.Tests.Services
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
        public void RegisterUser_True_UserIsValid()
        {
            var user = new User { UserId = 1, FirstName = "Иванов", LastName = "Иван", Login = "ivanov", Password = "password123" };

            var result = _userService.RegisterUser(user);

            Assert.That(result, Is.True);
        }

        [Test]
        public void RegisterUser_False_UserHasEmptyFields()
        {
            var user = new User { UserId = 1, FirstName = "", LastName = "Иван", Login = "ivanov", Password = "password123" };

            var result = _userService.RegisterUser(user);

            Assert.That(result, Is.False);
        }

        [Test]
        public void RegisterUser_False_LoginIsNotUnique()
        {
            var user1 = new User { UserId = 1, FirstName = "Иванов", LastName = "Иван", Login = "ivanov", Password = "password123" };
            var user2 = new User { UserId = 2, FirstName = "Петров", LastName = "Петр", Login = "ivanov", Password = "password456" };

            _userService.RegisterUser(user1);
            var result = _userService.RegisterUser(user2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void RegisterUser_False_PasswordIsTooShort()
        {
            var user = new User { UserId = 1, FirstName = "Иванов", LastName = "Иван", Login = "ivanov", Password = "short" };

            var result = _userService.RegisterUser(user);

            Assert.That(result, Is.False);
        }

        [Test]
        public void Login_User_CredentialsAreValid()
        {
            var user = new User { UserId = 1, FirstName = "Иванов", LastName = "Иван", Login = "ivanov", Password = "password123" };
            _userService.RegisterUser(user);

            var result = _userService.Login("ivanov", "password123");

            Assert.IsNotNull(result);
            Assert.AreEqual("ivanov", result.Login);
        }

        [Test]
        public void Login_Null_CredentialsAreInvalid()
        {
            var user = new User { UserId = 1, FirstName = "Иванов", LastName = "Иван", Login = "ivanov", Password = "password123" };
            _userService.RegisterUser(user);

            var result = _userService.Login("ivanov", "wrongpassword");

            Assert.IsNull(result);
        }

        [Test]
        public void GetUser_User_UserExists()
        {
            var user = new User { UserId = 1, FirstName = "Иванов", LastName = "Иван", Login = "ivanov", Password = "password123" };
            _userService.RegisterUser(user);

            var result = _userService.GetUser(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.UserId);
        }

        [Test]
        public void GetUser_Null_UserDoesNotExist()
        {
            var result = _userService.GetUser(1);

            Assert.IsNull(result);
        }

        [Test]
        public void UpdateUser_True_UserIsUpdated()
        {
            var user = new User { UserId = 1, FirstName = "Иванов", LastName = "Иван", Login = "ivanov", Password = "password123" };
            _userService.RegisterUser(user);

            var updatedUser = new User { UserId = 1, FirstName = "Иванов", LastName = "Иван", Login = "ivanov", Password = "newpassword123" };
            var result = _userService.UpdateUser(updatedUser);

            Assert.That(result, Is.True);
            var retrievedUser = _userService.GetUser(1);
            Assert.That(retrievedUser.FirstName, Is.EqualTo("Иванов"));
        }

        [Test]
        public void UpdateUser_False_UserDoesNotExist()
        {
            var user = new User { UserId = 1, FirstName = "Иванов", LastName = "Иван", Login = "ivanov", Password = "password123" };

            var result = _userService.UpdateUser(user);

            Assert.That(result, Is.False);
        }

        [Test]
        public void ChangePassword_True_PasswordIsChanged()
        {
            var user = new User { UserId = 1, FirstName = "Иванов", LastName = "Иван", Login = "ivanov", Password = "password123" };
            _userService.RegisterUser(user);

            var result = _userService.ChangePassword(1, "newpassword123");

            Assert.That(result, Is.True);
            var updatedUser = _userService.GetUser(1);
            Assert.That(updatedUser.Password, Is.EqualTo("newpassword123"));
        }

        [Test]
        public void ChangePassword_False_PasswordIsTooShort()
        {
            var user = new User { UserId = 1, FirstName = "Иванов", LastName = "Иван", Login = "ivanov", Password = "password123" };
            _userService.RegisterUser(user);

            var result = _userService.ChangePassword(1, "short");

            Assert.That(result, Is.False);
        }

        [Test]
        public void ChangePassword_False_UserDoesNotExist()
        {
            var result = _userService.ChangePassword(1, "newpassword123");

            Assert.That(result, Is.False);
        }

        [Test]
        public void DeleteUser_True_UserExists()
        {
            var user = new User { UserId = 1, FirstName = "Иванов", LastName = "Иван", Login = "ivanov", Password = "password123" };
            _userService.RegisterUser(user);

            var result = _userService.DeleteUser(1);

            Assert.IsTrue(result);
            var retrievedUser = _userService.GetUser(1);
            Assert.IsNull(retrievedUser);
        }

        [Test]
        public void DeleteUser_False_UserDoesNotExist()
        {
            var result = _userService.DeleteUser(1);

            Assert.That(result, Is.False);
        }
    }
}
