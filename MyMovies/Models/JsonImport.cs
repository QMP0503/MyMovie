using System.IO;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace MyMovies.Models
{
    public class JsonImport
    {
        public void Import(MovieDbContext context)
        {
            var json = File.ReadAllText("movies-250.json");
            var movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            context.Movies.AddRange(movies);
            context.SaveChanges();
        }

    }
}
