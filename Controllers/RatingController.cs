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
    public class RatingController : ControllerBase
    {
        private readonly RatingContext _context;
        private readonly SongContext _songContext;



        public RatingController(RatingContext context, SongContext songContext)
        {
            _context = context;
            _songContext = songContext;
        }

        // GET: api/Rating
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRatings()
        {
          if (_context.Ratings == null)
          {
              return NotFound();
          }
            return await _context.Ratings.ToListAsync();
        }

        // GET: api/Rating/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rating>> GetRating(int id)
        {
          if (_context.Ratings == null)
          {
              return NotFound();
          }
            var rating = await _context.Ratings.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            return rating;
        }

        // GET: api/Song/5
        [HttpGet("ratingsbysong/{songId}")]
        public async Task<ActionResult<List<Rating>>> GetRatingBySong(int songId)
        {
            var ratingsBySong = await _context.Ratings.Where(song => song.SongId == songId).ToListAsync();


            if (ratingsBySong == null || ratingsBySong.Count == 0)
            {
                return NotFound();
            }

            return ratingsBySong;
        }

        // GET: api/Song/5
        [HttpGet("averagerating/{songId}")]
        public async Task<ActionResult<double>> GetAverageRatingBySong(int songId)
        {
            //Get data from songId
            var ratingsBySong = await _context.Ratings.Where(song => song.SongId == songId).ToListAsync();

            //Return if null
            if (ratingsBySong == null || ratingsBySong.Count == 0)
            {
                return NotFound();
            }

            //Store title 
            var song = await _songContext.Songs.FindAsync(songId);
            string? songTitle = song.Title;

            //Calculuate averge value
            var averageRating = ratingsBySong.Average(rating => rating.SongRating);

            //Return json-message
            var message = new { text = "Snittvärdet för " + songTitle + " är " + averageRating };
            return new JsonResult(message);

        }

       

        // PUT: api/Rating/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRating(int id, Rating rating)
        {
            if (id != rating.RatingId)
            {
                return BadRequest();
            }

            _context.Entry(rating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
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

        // POST: api/Rating
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rating>> PostRating(Rating rating)
        {
          if (_context.Ratings == null)
          {
              return Problem("Entity set 'RatingContext.Ratings'  is null.");
          }

            //Check if SongId exist
            var songIdExist = await _songContext.Songs.FirstOrDefaultAsync(m => m.SongId == rating.SongId);

            //Return if SongId doesnt exist
            if(songIdExist == null) {

                var message = new { text = "Låtens id finns inte i databasen" };
                return new JsonResult(message);
                
            }

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRating", new { id = rating.RatingId }, rating);
        }

        // DELETE: api/Rating/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            if (_context.Ratings == null)
            {
                return NotFound();
            }
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RatingExists(int id)
        {
            return (_context.Ratings?.Any(e => e.RatingId == id)).GetValueOrDefault();
        }
    }
}
