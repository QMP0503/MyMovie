namespace MyMovies.ViewModels
{
    public class MovieActorVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Genre { get; set; }
        public decimal Rating { get; set; }

       // public List<Actor> Actors { get; set; }
    }
}
