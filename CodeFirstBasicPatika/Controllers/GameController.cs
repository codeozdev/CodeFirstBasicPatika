using CodeFirstBasicPatika.Context;
using CodeFirstBasicPatika.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstBasicPatika.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController(PatikaFirstDbContext context) : ControllerBase
    {
        private readonly PatikaFirstDbContext _context = context;
        
        [HttpGet]
        public async Task<IActionResult> GetGames()
        {
            var games = await _context.Games.ToListAsync();
            return Ok(games);
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddGame(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetGame), new { id = game.Id }, game);
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateGame(int id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }
            _context.Entry(game).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
