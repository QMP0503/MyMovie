using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyMovies.Data;
using MyMovies.Models.ReadJsonModels;
using Newtonsoft.Json;
using NuGet.Versioning;
using System;
using System.Globalization;
using System.Linq;
using System.Text.Json;

namespace MyMovies.Models;

public static class SeedData
{

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<ApplicationDbContext>>()))
        {

            //if (context.Movies.Any())
            //    {
            //        return;   // DB has been seeded
            //    }
            //context.Movies.AddRange(
            //    new Movie
            //    {
            //        Title = "When Harry Met Sally",
            //        ReleaseDate = DateTime.Parse("1989-2-12"),
            //        Genre = "Romantic Comedy",
            //        Rating = 8,

            //    },
            //    new Movie
            //    {
            //        Title = "Ghostbusters ",
            //        ReleaseDate = DateTime.Parse("1984-3-13"),
            //        Genre = "Comedy",
            //        Rating = 6
            //    },
            //    new Movie
            //    {
            //        Title = "Ghostbusters 2",
            //        ReleaseDate = DateTime.Parse("1986-2-23"),
            //        Genre = "Comedy",
            //        Rating = 2
            //    },
            //    new Movie
            //    {
            //        Title = "Rio Bravo",
            //        ReleaseDate = DateTime.Parse("1959-4-15"),
            //        Genre = "Western",
            //        Rating = 10
            //    }
            //);
            //if (context.Actors.Any())
            //{
            //    return;   // DB has been seeded
            //}
            //context.Actors.AddRange(
            //    new Actor
            //    {
            //        FirstName = "Jason",
            //        LastName = "Cruise",
            //        Age = 30,
            //    },
            //    new Actor
            //    {
            //        FirstName = "Tom",
            //        LastName = "Cruise",
            //        Age = 40,

            //    },
            //    new Actor
            //    {
            //        FirstName = "Jack",
            //        LastName = "Born",
            //        Age = 20,
            //    },
            //    new Actor
            //    {
            //        FirstName = "Tom",
            //        LastName = "Smith",
            //        Age = 50,

            //    },
            //    new Actor
            //    {
            //        FirstName = "James",
            //        LastName = "Ryan",
            //        Age = 30,
            //    },
            //    new Actor
            //    {
            //        FirstName = "Sarah",
            //        LastName = "Bonny",
            //        Age = 22,
            //    }
            //    );

            StreamReader r = new StreamReader("C:\\Users\\intern.pmquang1\\C#\\MyMovies\\MyMovies\\wwwroot\\movies-250.json");

            string json = r.ReadToEnd();
            MovieTestArray movieTest = JsonConvert.DeserializeObject<MovieTestArray>(json);
            var movieArray = new List<Movie250>();
            int i = 0;
            IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-US");
            foreach (var movie in movieTest.movies)
            {
                int.TryParse(movie.BoxOffice, NumberStyles.Integer | NumberStyles.AllowThousands,
                        provider, out int boxOffice);
                int.TryParse(movie.Year, out int year);
                int.TryParse(movie.imdbVotes, NumberStyles.Integer | NumberStyles.AllowThousands,
                        provider, out int votes);
                int.TryParse(movie.Metascore, NumberStyles.Integer | NumberStyles.AllowThousands, provider, out int metascore);
                double.TryParse(movie.imdbRating, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands,
                        provider, out double rating);
                DateOnly.TryParse(movie.Released, CultureInfo.InvariantCulture,  out DateOnly released);
                movieArray.Add(
                new Movie250
                {
                    Title = movie.Title,
                    Year = year,
                    Actors = movie.Actors,
                    Rated = movie.Rated,
                    Release = released,
                    Runtime = movie.Runtime,
                    Genre = movie.Genre,
                    Director = movie.Director,
                    Writer = movie.Writer,
                    Plot = movie.Plot,
                    Language = movie.Language,
                    Country = movie.Country,
                    Awards = movie.Awards,
                    Poster = movie.Poster,
                    Metascore = metascore,
                    imdbRating = rating,
                    imdbVotes = votes,
                    BoxOffice = boxOffice,
                });
            }
            //var test = movieArray.OrderByDescending(x => x.Release);
            if (context.Movies250.Any())
            {
                return;   // DB has been seeded
            }
            context.Movies250.AddRange(movieArray);
            context.SaveChanges();


        }
    }
}