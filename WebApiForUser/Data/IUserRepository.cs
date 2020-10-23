using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace UserWebApi.Data
{
    public interface IUserRepository
    {
        Task<ActionResult<IEnumerable<User>>> GetUsers();
        Task<ActionResult<User>> GetUserByID(int userId);
        Task<ActionResult<User>> InsertUser(User user);
        Task<ActionResult<User>> DeleteUser(int userId);
        Task<ActionResult<User>> UpdateUser(int i, User user);
    }
}
