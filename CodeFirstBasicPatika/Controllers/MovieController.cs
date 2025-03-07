using CodeFirstBasicPatika.Context;
using CodeFirstBasicPatika.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstBasicPatika.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController(PatikaFirstDbContext context) : ControllerBase
    {
        
        private readonly PatikaFirstDbContext _context = context;
        
        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _context.Movies.ToListAsync();
            return Ok(movies);
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
