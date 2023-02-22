using System.ComponentModel.DataAnnotations;

namespace DT191G_moment4.Models{

    public class Artist {

        public int ArtistId {get; set;}

        [Required]
        public string? ArtistName {get; set;}

        
    }

}