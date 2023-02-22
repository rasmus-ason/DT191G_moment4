using DT191G_moment4.Models;
using Microsoft.EntityFrameworkCore;

namespace DT191G_moment4.Data {

    public class ArtistContext : DbContext {

        public ArtistContext(DbContextOptions<ArtistContext> options) : base(options){

        }

        public DbSet<Artist> Artists {get; set;}

    }
}