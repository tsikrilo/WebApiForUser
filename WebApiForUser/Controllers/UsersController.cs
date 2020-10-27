using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserWebApi.Data;
using WebApi.Models;

// TODO correct namespace everywhere, also cleanup imports
namespace UserWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            // TODO controllers should return http response codes like 200, 400, 500

            return await _userRepository.GetUsers();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int userId)
        {
            return await _userRepository.GetUserByID(userId);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> PutUser(int id, User user)
        {
            // TODO no point in having both the path param id and user id
            // TODO we use DTOs (Automapper)
            // TODO handle exceptions in controller
            // TODO log exceptions in controller
            try
            {
                return await _userRepository.UpdateUser(id, user);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500); // Internal Server Error
            }
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            return await _userRepository.InsertUser(user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            return await _userRepository.DeleteUser(id);
        }
    }
}
