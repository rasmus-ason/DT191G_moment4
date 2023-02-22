using System.ComponentModel.DataAnnotations;

namespace DT191G_moment4.Models{

    public class Rating {

        public int RatingId {get; set;}

        [Required]
        [Range(1, 10, ErrorMessage = "The song rating must be between 1 and 10.")]
        public int SongRating {get; set;}

        [Required]
        //Foreign key of table Song
        public int SongId {get; set;}

       
    }

}