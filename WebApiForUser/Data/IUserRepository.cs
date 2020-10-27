using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

// TODO correct namespace
namespace UserWebApi.Data
{
    public interface IUserRepository
    {
        // TODO xml comments should be in the interfaces
        Task<ActionResult<IEnumerable<User>>> GetUsers();
        Task<ActionResult<User>> GetUserByID(int userId);
        Task<ActionResult<User>> InsertUser(User user);
        Task<ActionResult<User>> DeleteUser(int userId);
        Task<ActionResult<User>> UpdateUser(int i, User user);
    }
}
