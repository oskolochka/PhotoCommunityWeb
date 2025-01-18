using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PhotoCommunityWeb.Controllers;
using PhotoCommunityWeb.Models;
using PhotoCommunityWeb.Services;

namespace PhotoCommunityWeb.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private UserController _userController;
        private Mock<UserService> _mockUserService;

        [SetUp]
        public void Setup()
        {
            _mockUserService = new Mock<UserService>(); 
            _userController = new UserController(_mockUserService.Object);
        }

        [Test]
        public void RegisterUser_CreatedResult_UserIsValid()
        {
            var user = new User { UserId = 1, FirstName = "Иван", LastName = "Иванов", Login = "ivanov", Password = "password123" };

            _mockUserService.Setup(s => s.RegisterUser(user)).Returns(true);

            var result = _userController.RegisterUser(user);

            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
            Assert.AreEqual(user, createdResult.Value);
        }

        [Test]
        public void RegisterUser_BadRequest_UserIsInvalid()
        {
            var user = new User { UserId = 1, FirstName = "Иван", LastName = "Иванов", Login = "ivanov", Password = "password123" };

            _mockUserService.Setup(s => s.RegisterUser(user)).Returns(false);

            var result = _userController.RegisterUser(user);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void GetUser_OkResult_UserExists()
        {
            var user = new User { UserId = 1, FirstName = "Иван", LastName = "Иванов", Login = "ivanov", Password = "password123" };
            _mockUserService.Setup(s => s.GetUser(1)).Returns(user);

            var result = _userController.GetUser(1);

            var okResult = result as OkObjectResult; 
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var returnedUser = okResult.Value as User; 
            Assert.IsNotNull(returnedUser);
            Assert.AreEqual(user.UserId, returnedUser.UserId);
            Assert.AreEqual(user.FirstName, returnedUser.FirstName);
            Assert.AreEqual(user.LastName, returnedUser.LastName);
            Assert.AreEqual(user.Login, returnedUser.Login);
        }

        [Test]
        public void GetUser_NotFound_UserDoesNotExist()
        {
            _mockUserService.Setup(s => s.GetUser(1)).Returns((User)null);

            var result = _userController.GetUser(1);

            Assert.IsInstanceOf<NotFoundResult>(result); 
        }

        [Test]
        public void UpdateUser_NoContent_UserIsUpdated()
        {
            var user = new User { UserId = 1, FirstName = "Иван", LastName = "Иванов", Login = "ivanov", Password = "password123" };

            _mockUserService.Setup(s => s.UpdateUser(user)).Returns(true);

            var result = _userController.UpdateUser(1, user);

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void UpdateUser_NotFound_UserDoesNotExist()
        {
            var user = new User { UserId = 1, FirstName = "Иван", LastName = "Иванов", Login = "ivanov", Password = "password123" };

            _mockUserService.Setup(s => s.UpdateUser(user)).Returns(false);

            var result = _userController.UpdateUser(1, user);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void DeleteUser_NoContent_UserIsDeleted()
        {
            _mockUserService.Setup(s => s.DeleteUser(1)).Returns(true);

            var result = _userController.DeleteUser(1);

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void DeleteUser_NotFound_UserDoesNotExist()
        {
            _mockUserService.Setup(s => s.DeleteUser(1)).Returns(false);

            var result = _userController.DeleteUser(1);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
