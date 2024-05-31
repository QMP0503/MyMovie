namespace MyMovies.Models
{
    public class MovieTest
    {
        public string Title { get; set; } 
        public int Year { get; set; }
        public string Rated { get; set; }

        [DataType(DataType.Date)]
        public DateTime Release { get; set; }

    }
}
