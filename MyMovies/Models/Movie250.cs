﻿namespace MyMovies.Models
{
    public class Movie250
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Rated { get; set; }
        public DateOnly Release { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Awards { get; set; }
        public string Poster { get; set; }
        public int Metascore { get; set; }
        public double imdbRating { get; set; }
        public int imdbVotes { get; set; }
        [Display(Name = "Box Office")]
        public int BoxOffice { get; set; }
    }
}
