using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DT191G_moment4.Models{

    public class Song {

        public int SongId {get; set;}

        [Required]
        public string? Title {get; set;}

        [Required]
        public int LengthInSeconds {get; set;}

        [Required]
        public string? Genre {get; set;}

        //Foreign key of table Artist
        [Required]
        public int? ArtistId {get; set;}
    }

}