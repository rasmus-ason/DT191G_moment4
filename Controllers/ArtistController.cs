using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DT191G_moment4.Data;
using DT191G_moment4.Models;

namespace DT191G_moment4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly ArtistContext _context;

        public ArtistController(ArtistContext context)
        {
            _context = context;
        }

        // GET: api/Artist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtists()
        {
          if (_context.Artists == null)
          {
              return NotFound();
          }

            return await _context.Artists.ToListAsync();
        }

        // GET: api/Artist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetArtist(int id)
        {
          if (_context.Artists == null)
          {
              return NotFound();
          }
            var artist = await _context.Artists.FindAsync(id);

            if (artist == null)
            {
                var message = new { message = "Artist with id " + id + " doesnt exist"};
                return NotFound(message);
            }

            return artist;
        }

        // PUT: api/Artist/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, Artist artist)
        {
            if (id != artist.ArtistId)
            {
                var message = new { message = "Artist with id " + id + " doesnt exist"};
                return BadRequest(message);
            }

            _context.Entry(artist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
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

        // POST: api/Artist
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Artist>> PostArtist(Artist artist)
        {
          if (_context.Artists == null)
          {
              return Problem("Entity set 'ArtistContext.Artists'  is null.");
          }

          //Check if artist name exist in db
          var doesArtistExist = await _context.Artists.FirstOrDefaultAsync(m => m.ArtistName == artist.ArtistName);

          //Return if artist exist
          if(doesArtistExist != null){

            var message = new { text = "Artist finns redan i databasen" };
            return new JsonResult(message);

          }




            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtist", new { id = artist.ArtistId }, artist);
        }

        // DELETE: api/Artist/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            if (_context.Artists == null)
            {

                return NotFound();
            }
            var artist = await _context.Artists.FindAsync(id);
            
            if (artist == null)
            {
                var message = new { message = "Artist with id " + id + " doesnt exist"};
                return NotFound(message);
            }

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtistExists(int id)
        {
            return (_context.Artists?.Any(e => e.ArtistId == id)).GetValueOrDefault();
        }
    }
}
