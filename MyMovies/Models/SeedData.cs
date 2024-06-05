using Humanizer.Localisation;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using MyMovies.Data;
using MyMovies.Models.ReadJsonModels;
using Newtonsoft.Json;
using NuGet.Versioning;
using System;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Numerics;
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
            //context.SaveChanges();

            //var moviedb = context.Movies.ToList();
            //var actordb = context.Actors.ToList();
            //var directordb = context.Directors.ToList();

            //if (moviedb.Any() || directordb.Any() || actordb.Any()) return; //checking if database already seeded



            //var movies = new List<Movie>();
            //var actors = new List<Actor>();
            //var directors = new List<Director>();

            //foreach (var movie in movie250)
            //{
            //    var directorArray = movie.Director.Split(", ");
            //    var actorArray = movie.Actors.Split(", ");


            //    movies.Add(new Movie //adding movie250 to movie
            //    {
            //        Title = movie.Title,
            //        Year = movie.Year,
            //        Rated = movie.Rated,
            //        Release = movie.Release,
            //        Runtime = movie.Runtime,
            //        Genre = movie.Genre,
            //        Writer = movie.Writer,
            //        Plot = movie.Plot,
            //        Language = movie.Language,
            //        Country = movie.Country,
            //        Awards = movie.Awards,
            //        Poster = movie.Poster,
            //        Metascore = movie.Metascore,
            //        imdbRating = movie.imdbRating,
            //        imdbVotes = movie.imdbVotes,
            //        BoxOffice = movie.BoxOffice,

            //    });

            //    foreach (var director in directorArray)
            //    {
            //        var existingDirector = directors.FirstOrDefault(x => x.Name == director);
            //        if (existingDirector == null)
            //        {
            //            directors.Add(new Director
            //            {
            //                Name = director,

            //            });
            //        }

            //    }
            //    foreach (var actor in actorArray)
            //    {
            //        var existingActor = actors.FirstOrDefault(x => x.Name == actor);
            //        if (existingActor == null)
            //        {
            //            actors.Add(new Actor
            //            {
            //                Name = actor,

            //            });
            //        }
            //    }

            //}
            //var testA = actors.ToList();
            //var testD = directors.ToList();
            //var testM = movies.ToList();

            //context.Movies.AddRange(movies);
            //context.Actors.AddRange(actors);
            //context.Directors.AddRange(directors);
            //context.SaveChanges();

            //var moviedb = context.Movies.ToList();
            //var actordb = context.Actors.ToList();
            //var directordb = context.Directors.ToList();

            ////if (moviedb.Any() || directordb.Any() || actordb.Any()) return; //checking if database already seeded

            //var movie250 = context.Movies250.ToList();

            //var MA = context.MovieActors.ToList();
            //var MD = context.MovieDirectors.ToList();

            //if(MA.Any() || MD.Any()) return; //checking if database already seeded

            //var ListMA = new List<MovieActor>();
            //var ListMD = new List<MovieDirector>();

            //foreach (var movie in movie250)
            //{
            //    var directorArray = movie.Director.Split(", ");
            //    var actorArray = movie.Actors.Split(", ");


            //    var actorList = new List<Actor>(); //used inside foreach to link movie with actor (do actor seperate to avoid duplicates)
            //    var directorList = new List<Director>();
            //    var movieList = new List<Movie>();

            //    var movieTitle = movie.Title; //for thinking

            //    var movieID = moviedb.FirstOrDefault(x => x.Title == movieTitle);

            //    foreach (var actor in actorArray) //linking movie and actor
            //    {
            //        var actorID = actordb.FirstOrDefault(x => x.Name == actor);
            //        if (actorID != null && movieID != null)
            //        {
            //            ListMA.Add(new MovieActor
            //            {
            //                MovieId = movieID.Id,
            //                ActorId = actorID.Id
            //            });
            //        }



            //    }
            //    foreach (var director in directorArray)
            //    {
            //        var directorID = directordb.FirstOrDefault(x => x.Name == director);
            //        if (directorID != null && movieID != null)
            //        {
            //            ListMD.Add(new MovieDirector
            //            {
            //                MovieId = movieID.Id,
            //                DirectorId = directorID.Id
            //            });
            //        }


            //    }

            //}

            //var check1 = ListMA.ToList();
            //var check2 = ListMD.ToList();

            //context.MovieActors.AddRange(ListMA);
            //context.MovieDirectors.AddRange(ListMD);
            //context.SaveChanges();

            return;


        }
    }
}