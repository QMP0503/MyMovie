using System.ComponentModel.DataAnnotations.Schema;

namespace MyMovies.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

       // [Display(Name = "Release Date")]
       // [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? Genre { get; set; }
       // [Column(TypeName = "decimal(18, 2)")]
        public decimal Rating { get; set; }
    }
}
