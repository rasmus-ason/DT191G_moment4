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
    public class SongController : ControllerBase
    {
        private readonly SongContext _context;
        private readonly ArtistContext _artistContext;
         private readonly RatingContext _ratingContext;

        public SongController(SongContext context, ArtistContext artistContext, RatingContext ratingContext )
        {
            _context = context;
            _artistContext = artistContext;
            _ratingContext = ratingContext;
        }

        // GET: api/Song
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
          if (_context.Songs == null)
          {
              return NotFound();
          }
            return await _context.Songs.ToListAsync();
        }

        // GET: api/Song/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(int id)
        {
          if (_context.Songs == null)
          {
            return NotFound();
          }
            var song = await _context.Songs.FindAsync(id);

            if (song == null)
            {
                var usermessage = new { message = "Song with id " + id + " doesnt exist"};
                return NotFound(usermessage);
            }

            return song;
        }

        // GET: api/Song/5
        [HttpGet("songsbyartist/{artistId}")]
        public async Task<ActionResult<List<Song>>> GetSongsByArtist(int artistId)
        {
            var songs = await _context.Songs.Where(song => song.ArtistId == artistId).ToListAsync();


            if (songs == null || songs.Count == 0)
            {
                var usermessage = new { message = "Artist with artist-id " + artistId + " doesnt have any songs in database"};
                return NotFound();
            }

            return songs;
        }
        


        // PUT: api/Song/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, Song song)
        {
            if (id != song.SongId)
            {
                var usermessage = new { message = "Song with id " + id + " doesnt exist"};
                return BadRequest();
            }

            _context.Entry(song).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
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

        // POST: api/Song
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(Song song)
        {
           if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

        //Check if artistId exist
        var artistExist = await _artistContext.Artists.FirstOrDefaultAsync(m => m.ArtistId == song.ArtistId);


        //Return if artistId does not exist
        if(artistExist == null) {

            var message = new { text = "ArtistId finns ej" };
            return new JsonResult(message);
            
        }




            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSong", new { id = song.SongId }, song);
        }

        // DELETE: api/Song/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            if (_context.Songs == null)
            {
                
                return NotFound();
            }

            //Delete the song
            var song = await _context.Songs.FindAsync(id);

            if (song == null)
            {
                var usermessage = new { message = "Song with id " + id + " doesnt exist"};
                return NotFound(usermessage);
            }

            //Delete its rating
            var ratingsToDelete = await _ratingContext.Ratings.Where(rating => rating.SongId == id).ToListAsync();

            if (ratingsToDelete != null)
            {
                //Remove several documents
                _ratingContext.Ratings.RemoveRange(ratingsToDelete);     
                await _ratingContext.SaveChangesAsync();
            }

            //Save changes
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SongExists(int id)
        {
            return (_context.Songs?.Any(e => e.SongId == id)).GetValueOrDefault();
        }
    }
}
