using System.ComponentModel.DataAnnotations.Schema;

namespace MyMovies.Models
{
    /*
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

       [Display(Name = "Release Date")]
       [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? Genre { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Rating { get; set; }

        public List<Actor>? Actors { get; set;} //many-to-many

    }
    */
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public int Runtime { get; set; }
        public string Rated { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; } //make list when possible
        public string Writer { get; set; } //make list when possible
        public string Actors { get; set; } //make list when possible
        public string PLot { get; set; }
        public string Awards { get; set; }
        public decimal imdbRating { get; set; }

    }
}
