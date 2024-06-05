using System.ComponentModel.DataAnnotations.Schema;

namespace MyMovies.Models
{
    public class MovieDirector
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
        [ForeignKey("Director")]
        public int DirectorId { get; set; }
        public Director Director { get; set; } = null!;

    }

}
