using System.ComponentModel.DataAnnotations.Schema;

namespace MyMovies.Models
{
    public class MovieActor
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Actor")]
        public int ActorId { get; set; }
        public Actor Actor { get; set; } = null!;
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;   
    }
}
