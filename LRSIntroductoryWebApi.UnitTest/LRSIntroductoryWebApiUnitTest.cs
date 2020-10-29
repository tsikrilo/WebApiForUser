using LRSIntroductoryWebApi.Controllers;
using LRSIntroductoryWebApi.DTO;
using LRSIntroductoryWebApi.Models;
using LRSIntroductoryWebApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace LRSIntroductoryWebApi.UnitTest
{
    [TestClass]
    public class LRSIntroductoryWebApiUnitTest
    {
        private Mock<IUserService> userServiceMocked;
        private Mock<ILogger> loggerMocked;
        private UsersController _usersController;

        private User user = new User()
        {
            Id = 1,
            Name = "Name1",
            Surname = "Surname1",
            Birthdate = new DateTime(1998, 01, 02),
            EmailAddress = "kjdhsdjf",
            UserTypeId = 1,
            UserTitleId = 1,
            IsActive = true
        };

        private User userInserted = new User()
        {
            Name = "Name1",
            Surname = "Surname1",
            Birthdate = new DateTime(1998, 01, 02),
            EmailAddress = "test@test.gr",
            UserTypeId = 1,
            UserTitleId = 1,
            IsActive = true
        };

        private UserDTO SetupDto(string _name, string _surname, DateTime _birthdate,
            string _emailAddress, int _userTypeId, int _userTitleId, bool _isActive)
        {
            var dto = new UserDTO
            {
                Name = _name,
                Surname = _surname,
                Birthdate = _birthdate,
                EmailAddress = _emailAddress,
                UserTypeId = _userTypeId,
                UserTitleId = _userTitleId,
                IsActive = _isActive
            };
            return dto;
        }

        [TestInitialize]
        public void Setup()
        {
            userServiceMocked = new Mock<IUserService>(MockBehavior.Strict);
            loggerMocked = new Mock<ILogger>(MockBehavior.Default);
        }

        /// <summary>
        /// Class: <see cref="_usersController"/>
        /// Method: <see cref="_usersController.PostUser(user)"/>
        /// Test: For input user, method returns OkResult
        /// </summary>
        [TestMethod]
        [TestCategory("Success")]
        public async Task PostUser_InsertSuccessfully_OkObjectResult()
        {
            Setup();
            _usersController = new UsersController(userServiceMocked.Object, loggerMocked.Object);
            userServiceMocked.Setup(x => x.CreateUser(user)).ReturnsAsync(user);

            var response = await _usersController.PostUser(user);
            Assert.IsInstanceOfType(response.Result, typeof(OkObjectResult));
        }

        /// <summary>
        /// Class: <see cref="_usersController"/>
        /// Method: <see cref="_usersController.PostUser(user)"/>
        /// Test: For input null, method returns BadRequest
        /// </summary>
        [TestMethod]
        [TestCategory("Fail")]
        public async Task PostUser_InsertNotSuccessfully_BadRequest()
        {
            Setup();
            User nullUser = null;
            _usersController = new UsersController(userServiceMocked.Object, loggerMocked.Object);
            userServiceMocked.Setup(x => x.CreateUser(null)).ReturnsAsync(nullUser);
            var response = await _usersController.PostUser(null);

            Assert.IsInstanceOfType(response.Result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Class: <see cref="_usersController"/>
        /// Method: <see cref="_usersController.DeleteUser(int)"/>
        /// Test: For input id = 1, method returns user with Id = 1 and IsActive = false
        /// </summary>
        [TestMethod]
        [TestCategory("Success")]
        public async Task DeleteUser_Successfully()
        {
            Setup();
            User user = new User()
            {
                Id = 1,
                Name = "Name1",
                Surname = "Surname1",
                Birthdate = new DateTime(1998, 01, 02),
                EmailAddress = "test@test.gr",
                UserTypeId = 1,
                UserTitleId = 1,
                IsActive = false
            };
            _usersController = new UsersController(userServiceMocked.Object, loggerMocked.Object);
            userServiceMocked.Setup(x => x.DisableUser(1)).ReturnsAsync(user);

            var response = await _usersController.DeleteUser(1);

            Assert.IsInstanceOfType(response.Result, typeof(OkObjectResult));
        }

        /// <summary>
        /// Class: <see cref="_usersController"/>
        /// Method: <see cref="_usersController.DeleteUser(int)"/>
        /// Test: For input id <= 0, method returns BadRequest
        /// </summary>
        [TestMethod]
        [TestCategory("Fail")]
        public async Task DeleteUser_NotSuccessfully_BadRequest()
        {
            Setup();
            User nullUser = null;
            _usersController = new UsersController(userServiceMocked.Object, loggerMocked.Object);
            userServiceMocked.Setup(x => x.DisableUser(-2)).ReturnsAsync(nullUser);

            var response = await _usersController.DeleteUser(-2);
            Assert.IsInstanceOfType(response.Result, typeof(BadRequestResult));
        }
    }
}
