using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserWebApi.Controllers;
using UserWebApi.Data;
using WebApi.Models;

namespace UserWebApi.UnitTests
{
    [TestClass]
    public class UnitTests
    {
        private UsersController _usersController;
        private Mock<IUserRepository> _userRepositoryMocked;
        private List<User> _listOfUsers;
        private UserContext _context;

        private readonly User _userInserted = new User()
        {
            Id = 1,
            Name = "Name3",
            Surname = "Surname3",
            Birthdate = new DateTime(2002, 02, 02),
            EmailAddress = "1",
            UserTypeId = 3,
            UserTitleId = 3,
            IsActive = false
        };

        private readonly User _userDeleted = new User()
        {
            Id = 1,
            Name = "Name1",
            Surname = "Surname1",
            Birthdate = new DateTime(1998, 01, 02),
            EmailAddress = "kjdhsdjf",
            UserTypeId = 1,
            UserTitleId = 1,
            IsActive = false
        };

        /// <summary>
        /// Sets up.
        /// </summary>
        // TODO there is a TestInitialize annotation for this. you should NOT call it directly
        private void SetUp()
        {
            // TODO no point in having this region

            #region Setup

            _userRepositoryMocked = new Mock<IUserRepository>(MockBehavior.Strict);
            var options = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            // TODO we mock everything else than the tested "unit" in unit tests
            _context = new UserContext(options);

            _usersController = new UsersController(_userRepositoryMocked.Object);

            _listOfUsers = GetUsers();

            // TODO we do NOT manipulate the context in the unit tests
            _context.AddRange(_listOfUsers);
            _context.SaveChanges();

            #endregion
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <returns></returns>
        private List<User> GetUsers()
        {
            User user1 = new User()
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

            User user2 = new User()
            {
                Id = 2,
                Name = "Name2",
                Surname = "Surname2",
                Birthdate = new DateTime(1998, 02, 02),
                EmailAddress = "kjdhsdjf",
                UserTypeId = 2,
                UserTitleId = 2,
                IsActive = true
            };

            User user3 = new User()
            {
                Id = 3,
                Name = "Name3",
                Surname = "Surname3",
                Birthdate = new DateTime(2002, 02, 02),
                EmailAddress = "1",
                UserTypeId = 3,
                UserTitleId = 3,
                IsActive = false
            };

            List<User> listOfUsers = new List<User>()
            {
                user1, user2, user3
            };

            return listOfUsers;
        }

        /// <summary>
        /// Posts the user insert successfully.
        /// </summary>
        [TestMethod]
        public void PostUser_InsertSuccessfully()
        {
            SetUp();

            _userRepositoryMocked.Setup(c => c.InsertUser(_userInserted)).ReturnsAsync(new User()
            {
                Id = 1,
                Name = "Name1",
                Surname = "Surname1",
                Birthdate = new DateTime(1998, 01, 02),
                EmailAddress = "kjdhsdjf",
                UserTypeId = 1,
                UserTitleId = 1,
                IsActive = true
            });

            Task<ActionResult<User>> response = _usersController.PostUser(_userInserted);

            Assert.AreEqual(1, response.Result.Value.Id);

            _userRepositoryMocked.VerifyAll();

            _context.Dispose();
        }

        /// <summary>
        /// Puts the user modify user successfully.
        /// </summary>
        [TestMethod]
        public void PutUser_ModifyUserSuccessfully()
        {
            SetUp();

            _userRepositoryMocked.Setup(c => c.UpdateUser(1, _listOfUsers[2])).ReturnsAsync(_userInserted);

            Task<ActionResult<User>> response = _usersController.PutUser(1, _listOfUsers[2]);

            Assert.AreEqual(_userInserted, response.Result.Value);

            _userRepositoryMocked.VerifyAll();

            _context.Dispose();
        }

        /// <summary>
        /// Deletes the user successfully.
        /// </summary>
        [TestMethod]
        public void DeleteUser_Successfully()
        {
            SetUp();

            _userRepositoryMocked.Setup(c => c.DeleteUser(1)).ReturnsAsync(_userDeleted);

            Task<ActionResult<User>> response = _usersController.DeleteUser(1);

            Assert.AreEqual(false, response.Result.Value.IsActive);
            _userRepositoryMocked.VerifyAll();
            _context.Dispose();
        }


        /// <summary>
        /// Gets the user get successfully.
        /// </summary>
        [TestMethod]
        public void GetUser_GetSuccessfully()
        {
            SetUp();

            _userRepositoryMocked.Setup(c => c.GetUserByID(1)).ReturnsAsync(_listOfUsers[0]);

            Task<ActionResult<User>> response = _usersController.GetUser(1);

            Assert.AreEqual("Name1", response.Result.Value.Name);

            _userRepositoryMocked.VerifyAll();

            _context.Dispose();
        }

        [TestMethod]
        public void UserControllerConstructorTest()
        {
            try
            {
                // TODO we do not unit test constructors
                var users = new UsersController(null);
                Assert.Fail();
            }
            catch (ArgumentNullException exc)
            {
                Assert.AreEqual("userRepository", exc.ParamName);
            }
        }
    }
}
