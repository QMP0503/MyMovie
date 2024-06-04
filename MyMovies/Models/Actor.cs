﻿namespace MyMovies.Models
{
    public class Actor
    {
        public Actor()
        {
            Movies = new List<Movie>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
