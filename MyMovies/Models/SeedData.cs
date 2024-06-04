using Humanizer.Localisation;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyMovies.Data;
using MyMovies.Models.ReadJsonModels;
using Newtonsoft.Json;
using NuGet.Versioning;
using System;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace MyMovies.Models;

public static class SeedData
{

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<ApplicationDbContext>>()))
        {

            //StreamReader r = new StreamReader("C:\\Users\\intern.pmquang1\\C#\\MyMovies\\MyMovies\\wwwroot\\movies-250.json");

            //string json = r.ReadToEnd();
            //MovieTestArray movieTest = JsonConvert.DeserializeObject<MovieTestArray>(json);
            //var movieArray = new List<Movie250>();
            //int i = 0;
            //IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-US");
            //foreach (var movie in movieTest.movies)
            //{
            //    int.TryParse(movie.BoxOffice, NumberStyles.Integer | NumberStyles.AllowThousands,
            //            provider, out int boxOffice);
            //    int.TryParse(movie.Year, out int year);
            //    int.TryParse(movie.imdbVotes, NumberStyles.Integer | NumberStyles.AllowThousands,
            //            provider, out int votes);
            //    int.TryParse(movie.Metascore, NumberStyles.Integer | NumberStyles.AllowThousands, provider, out int metascore);
            //    double.TryParse(movie.imdbRating, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands,
            //            provider, out double rating);
            //    DateOnly.TryParse(movie.Released, CultureInfo.InvariantCulture, out DateOnly released);
            //    movieArray.Add(
            //    new Movie250
            //    {
            //        Title = movie.Title,
            //        Year = year,
            //        Actors = movie.Actors,
            //        Rated = movie.Rated,
            //        Release = released,
            //        Runtime = movie.Runtime,
            //        Genre = movie.Genre,
            //        Director = movie.Director,
            //        Writer = movie.Writer,
            //        Plot = movie.Plot,
            //        Language = movie.Language,
            //        Country = movie.Country,
            //        Awards = movie.Awards,
            //        Poster = movie.Poster,
            //        Metascore = metascore,
            //        imdbRating = rating,
            //        imdbVotes = votes,
            //        BoxOffice = boxOffice,
            //    });
            //}
            ////var test = movieArray.OrderByDescending(x => x.Release);
            //if (context.Movies250.Any())
            //{
            //    return;   // DB has been seeded
            //}
            //context.Movies250.AddRange(movieArray);
            
            //var mov = context.Movies.ToList();
            //var dir = context.Directors.ToList();
            //var act = context.Actors.ToList();
            //if (mov.Any() || dir.Any() || act.Any()) return;

            var movie250 = context.Movies250.ToList();
            var directors = new List<Director>();
            var actors = new List<Actor>();
            var movies = new List<Movie>();

            int i = 1;

            foreach (var movie in movie250)
            {
                var directorArray = movie.Director.Split(", ");
                var actorArray = movie.Actors.Split(", ");
               

                var actorList = new List<Actor>(); //used inside foreach to link movie with actor (do actor seperate to avoid duplicates)
                var directorList = new List<Director>();
                
                foreach (var actor in actorArray)
                {
                    actorList.Add(new Actor
                    {
                        Name = actor
                    });
                }
                foreach(var director in directorArray)
                {
                    directorList.Add(new Director
                    {
                        Name = director
                    });
                }

                movies.Add(new Movie //adding movie250 to movie
                {
                    Title = movie.Title,
                    Year = movie.Year,
                    Rated = movie.Rated,
                    Release = movie.Release,
                    Runtime = movie.Runtime,
                    Genre = movie.Genre,
                    Writer = movie.Writer,
                    Plot = movie.Plot,
                    Language = movie.Language,
                    Country = movie.Country,
                    Awards = movie.Awards,
                    Poster = movie.Poster,
                    Metascore = movie.Metascore,
                    imdbRating = movie.imdbRating,
                    imdbVotes = movie.imdbVotes,
                    BoxOffice = movie.BoxOffice,
                    Actors = new List<Actor>(actorList),
                    Directors = new List<Director>(directorList)
                });
                
                foreach (var director in directorArray)
                {
                    if (directors.All(x => x.Name != director))
                    {
                        directors.Add(new Director
                        {
                            Name = director,
                            Movies = new List<Movie>(movies)
                        });
                    }
                }
                foreach (var actor in actorArray)
                {
                    if (actors.All(x => x.Name != actor))
                    {
                        actors.Add(new Actor
                        {
                            Name = actor,
                            Movies = new List<Movie>(movies)
                        });
                    }
                    else
                    {

                    }
                }
            }

            var test = movies.ToList();
            var testD = directors.ToList();
            var testA = actors.ToList();



            context.AddRange(actors);
            context.SaveChanges();




        }
    }
}