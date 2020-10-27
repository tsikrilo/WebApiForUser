using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApiForUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTitlesController : ControllerBase
    {
        private readonly UserContext _context;

        // TODO no reason for anything other than GET methods
        public UserTitlesController(UserContext context)
        {
            _context = context;
        }

        // GET: api/UserTitles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTitle>>> GetUserTitle()
        {
            return await _context.UserTitle.ToListAsync();
        }

        // GET: api/UserTitles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTitle>> GetUserTitle(int id)
        {
            var userTitle = await _context.UserTitle.FindAsync(id);

            if (userTitle == null)
            {
                return NotFound();
            }

            return userTitle;
        }

        // PUT: api/UserTitles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserTitle(int id, UserTitle userTitle)
        {
            if (id != userTitle.Id)
            {
                return BadRequest();
            }

            _context.Entry(userTitle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTitleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserTitles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserTitle>> PostUserTitle(UserTitle userTitle)
        {
            _context.UserTitle.Add(userTitle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserTitle", new { id = userTitle.Id }, userTitle);
        }

        // DELETE: api/UserTitles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserTitle>> DeleteUserTitle(int id)
        {
            var userTitle = await _context.UserTitle.FindAsync(id);
            if (userTitle == null)
            {
                return NotFound();
            }

            _context.UserTitle.Remove(userTitle);
            await _context.SaveChangesAsync();

            return userTitle;
        }

        private bool UserTitleExists(int id)
        {
            return _context.UserTitle.Any(e => e.Id == id);
        }
    }
}
