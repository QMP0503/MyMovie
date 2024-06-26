﻿namespace MyMovies.ViewModels
{
    public class ActorVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Movie>? Movies { get; set; }
        public List<int>? SelectedMovies { get; set; }
    }
}
