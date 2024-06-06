using Microsoft.AspNetCore.Mvc;

namespace MyMovies.ViewModels
{
    public class MovieVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Rated { get; set; }
        public DateOnly Release { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        [BindProperty]
        public List<Director>? Directors { get; set; }
        public string Writer { get; set; }
        [BindProperty]
        public List<Actor>? Actors { get; set; }
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

        //used in edit/create
        public List<int>? SelectedActors { get; set; }
        public List<int>? SelectedDirectors { get; set; }
    }
}
