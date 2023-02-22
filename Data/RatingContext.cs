using DT191G_moment4.Models;
using Microsoft.EntityFrameworkCore;

namespace DT191G_moment4.Data {

    public class RatingContext : DbContext {

        public RatingContext(DbContextOptions<RatingContext> options) : base(options){

        }

        public DbSet<Rating> Ratings {get; set;}

    }
}