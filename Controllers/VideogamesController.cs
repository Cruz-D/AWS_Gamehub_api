using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gamehub_API.Models;
using gamehub_API.DbContext.NewFolder;

namespace gamehub_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideogamesController : ControllerBase
    {
        private readonly LocalDbContext _context;

        public VideogamesController(LocalDbContext context)
        {
            _context = context;
        }

        // GET: api/Videogames
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Videogame>>> GetVideogames()
        {
            return await _context.Videogames.ToListAsync();
        }

        // GET: api/Videogames/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Videogame>> GetVideogame(int id)
        {
            var videogame = await _context.Videogames.FindAsync(id);

            if (videogame == null)
            {
                return NotFound();
            }

            return videogame;
        }

        // PUT: api/Videogames/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVideogame(int id, Videogame videogame)
        {
            if (id != videogame.Id)
            {
                return BadRequest();
            }

            _context.Entry(videogame).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideogameExists(id))
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

        // POST: api/Videogames
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Videogame>> PostVideogame(Videogame videogame)
        {
            _context.Videogames.Add(videogame);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVideogame", new { id = videogame.Id }, videogame);
        }

        // DELETE: api/Videogames/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideogame(int id)
        {
            var videogame = await _context.Videogames.FindAsync(id);
            if (videogame == null)
            {
                return NotFound();
            }

            _context.Videogames.Remove(videogame);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VideogameExists(int id)
        {
            return _context.Videogames.Any(e => e.Id == id);
        }
    }
}
