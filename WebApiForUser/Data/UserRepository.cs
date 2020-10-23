using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;

namespace UserWebApi.Data
{
    public class UserRepository : IUserRepository
    {
        private UserContext _context;
        private readonly ILogger _logger;

        public UserRepository(UserContext context, ILogger<UserRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(UserRepository));
            _logger = logger;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.User.ToListAsync();
        }

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<ActionResult<User>> GetUserByID(int userId)
        {
            var user = _context.User.Where(c => c.Id == userId).AsNoTracking().FirstOrDefaultAsync();

            if (user == null)
            {
                _logger.LogInformation("User with id: ${id} does not exist!");
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("User with id: ${id} does not exist!"),
                    ReasonPhrase = "Retrieve data Exception"
                });
            }
            return await user;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public async Task<ActionResult<User>> InsertUser(User user)
        {
            try
            {
                _context.User.Add(user);
                _context.SaveChanges();
            }
            catch (HttpResponseException)
            {
                _logger.LogInformation("Error trying to create user");
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Error trying to create user"),
                    ReasonPhrase = "Critical Exception"
                });
            }
            return await _context.User.FindAsync(user.Id);
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = _context.User.Find(id);
            if (user == null)
            {
                _logger.LogInformation("User with id: ${id} does not exist!");
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Error trying to retrieve user data with id: ${id}")
                });
            }

            try
            {
                user.IsActive = false;
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (HttpResponseException)
            {
                _logger.LogInformation("Error trying to update user's data");
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Error trying to set user with id: ${id} as inactive")
                });
            }

            return await _context.User.FindAsync(id);
        }

        /// <summary>
        /// Updates the user's data with the specified id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            User userModified = _context.User.SingleOrDefault(x => x.Id == id);

            userModified.Name = user.Name;
            userModified.Surname = user.Surname;
            userModified.Birthdate = user.Birthdate;
            userModified.EmailAddress = user.EmailAddress;
            userModified.UserTitleId = user.UserTitleId;
            userModified.UserTypeId = user.UserTypeId;
            userModified.IsActive = user.IsActive;
            _context.Update(userModified);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    _logger.LogInformation("User with id: ${id} does not exist!");
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Error trying to update user data with id: ${id}")
                    });
                }
                else
                {
                    _logger.LogInformation("Error updating user data with id: ${id}");
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Error updating user data with id: ${id}")
                    });
                }
            }

            return await _context.User.FindAsync(user.Id);
        }

        /// <summary>
        /// Checks if the users with the specified id exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }

    }
}
