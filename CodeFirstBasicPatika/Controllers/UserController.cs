using CodeFirstBasicPatika.Context;
using CodeFirstBasicPatika.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstBasicPatika.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(PatikaFirstDbContext context) : ControllerBase
    {
        private readonly PatikaFirstDbContext _context = context;


        [HttpGet]
        public async Task<IActionResult> GetUsersWithPosts()
        {
            // Users tablosundan tüm kayıtları al
            // Include ile ilgili olan Posts bilgilerini de çek
            var users = await _context.Users
                .Include(u => u.Posts) // EF Core'un ilişkili veriyi yüklemesini sağlar
                .ToListAsync();

            // Veriyi JSON olarak döndür
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}