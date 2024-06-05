namespace MyMovies.Models
{
    public class Director
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<MovieDirector>? MovieDirectors { get; set; }
    }
}
