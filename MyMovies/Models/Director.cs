namespace MyMovies.Models
{
    public class Director
    {
        public Director()
        {
            Movies = new List<Movie>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
