namespace MyMovies.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public List<Movie>? Movies { get; set; } //many-to-many


    }
}
