using DT191G_moment4.Models;
using Microsoft.EntityFrameworkCore;

namespace DT191G_moment4.Data {

    public class SongContext : DbContext {

        public SongContext(DbContextOptions<SongContext> options) : base(options){



        }

        public DbSet<Song> Songs {get; set;}

    }
}